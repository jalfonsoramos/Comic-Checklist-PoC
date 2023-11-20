using ComicChecklist.Application.Interfaces.Repositories;
using ComicChecklist.Domain.Dtos;
using ComicChecklist.Domain.Enums;
using ComicChecklist.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ComicChecklist.Infra.Data.Repositories
{
    public class UserChecklistRepository : IUserChecklistRepository
    {
        private readonly ComicChecklistDbContext _dbContext;

        public UserChecklistRepository(ComicChecklistDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SubscribeToChecklistAsync(int userId, int checklistId)
        {
            var userChecklist = new UserChecklist
            {
                UserId = userId,
                ChecklistId = checklistId
            };

            await _dbContext.UserChecklists.AddAsync(userChecklist);
        }

        public async Task<List<SubscriptionDto>> GetSubscriptionsByUserIdAsync(int userId)
        {            
            var query = from userChecklist in _dbContext.UserChecklists
                        join checklist in _dbContext.Checklists on userChecklist.ChecklistId equals checklist.Id
                        join issue in _dbContext.Issues on checklist.Id equals issue.ChecklistId
                        join userIssueStatus in _dbContext.UserIssuesStatuses
                            on new { UserId = userChecklist.UserId, IssueId = issue.Id }
                                equals new { UserId = userIssueStatus.UserId, IssueId = userIssueStatus.IssueId } into utss
                        from uts in utss.DefaultIfEmpty()
                        where userChecklist.UserId == userId
                        group new { userChecklist, issue, uts } by new { ChecklistId = checklist.Id, ChecklistName = checklist.Name } into grouped
                        select new SubscriptionDto
                        (
                            grouped.Key.ChecklistId,
                            grouped.Key.ChecklistName,
                            grouped.Count(),
                            grouped.Count(g => g.uts.Status == IssueStatus.Completed),
                            (grouped.Count(g => g.uts.Status == IssueStatus.Completed) * 100.0) / grouped.Count()
                        );

            return await query.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
