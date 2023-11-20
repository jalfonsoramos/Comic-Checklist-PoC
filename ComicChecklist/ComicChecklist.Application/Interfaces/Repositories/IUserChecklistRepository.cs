using ComicChecklist.Domain.Dtos;
using ComicChecklist.Domain.Models;

namespace ComicChecklist.Application.Interfaces.Repositories
{
    public interface IUserChecklistRepository : IRepository
    {
        Task SubscribeToChecklistAsync(int userId, int checklistId);

        Task<List<SubscriptionDto>> GetSubscriptionsByUserIdAsync(int userId);
    }
}