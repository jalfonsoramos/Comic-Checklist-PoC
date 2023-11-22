using ComicChecklist.Presentation.UI.Models;

namespace ComicChecklist.Presentation.UI.Services
{
    public interface IChecklistApiService
    {
        Task<List<ChecklistModel>> GetAvailableChecklists();
        Task<List<SubscriptionSummaryModel>> GetSubscriptionsAsync();
    }
}
