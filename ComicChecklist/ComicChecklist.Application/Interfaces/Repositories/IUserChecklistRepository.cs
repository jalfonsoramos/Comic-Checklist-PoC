using ComicChecklist.Domain.Models;

namespace ComicChecklist.Application.Interfaces.Repositories
{
    public interface IUserChecklistRepository : IRepository
    {
        Task SubscribeToChecklistAsync(int userId, int checklistId);

        Task<List<UserChecklist>> GetSubscriptionsByUserIdAsync(int userId);
    }
}