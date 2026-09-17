using ComicChecklist.Application.Interfaces.Repositories;
using ComicChecklist.Domain.Enums;
using ComicChecklist.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ComicChecklist.Infra.Data.Repositories
{
    public class UserIssuesRepository : IUserIssuesRepository
    {
        private readonly ComicChecklistDbContext _dbContext;

        public UserIssuesRepository(ComicChecklistDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateUserIssueStatusAsync(int userId, int issueId, IssueStatus issueStatus)
        {
            var userIssueStatus = new UserIssueStatus
            {
                UserId = userId,
                IssueId = issueId,
                Status = issueStatus
            };

            await _dbContext.UserIssuesStatuses.AddAsync(userIssueStatus);
        }

        public async Task<bool> IssueStatusExistAsync(int userId, int issueId)
        {
            var userIssueStatus = await _dbContext.UserIssuesStatuses.SingleOrDefaultAsync(x => x.UserId == userId && x.IssueId == issueId);
            return userIssueStatus != null;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUserIssueStatusAsync(int userId, int issueId, IssueStatus issueStatus)
        {
            var userIssueStatus = await _dbContext.UserIssuesStatuses.SingleAsync(x => x.UserId == userId && x.IssueId == issueId);
            userIssueStatus.Status = issueStatus;
        }
    }
}
