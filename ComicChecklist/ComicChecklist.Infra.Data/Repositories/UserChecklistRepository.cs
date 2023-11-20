using ComicChecklist.Application.Interfaces.Repositories;
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

        public async Task<List<UserChecklist>> GetSubscriptionsByUserIdAsync(int userId)
        {
            return await _dbContext.UserChecklists.Include(x=>x.Checklist)
                                                  .AsNoTracking()
                                                  .Where(x => x.UserId == userId)
                                                  .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
