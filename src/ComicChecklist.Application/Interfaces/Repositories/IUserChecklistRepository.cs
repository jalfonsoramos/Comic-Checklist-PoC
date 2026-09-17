using ComicChecklist.Domain.Dtos;

namespace ComicChecklist.Application.Interfaces.Repositories
{
    public interface IUserChecklistRepository : IRepository
    {
        Task SubscribeToChecklistAsync(int userId, int checklistId);

        Task<List<SubscriptionSummaryDto>> GetSubscriptionsByUserIdAsync(int userId);

        Task<SubscriptionFullDto> GetSubscriptionByUserIdAsync(int userId, int checklistId);

        Task<bool> IsSubscribed(int userId, int checklistId1);
    }
}