using ComicChecklist.Presentation.UI.Enums;
using ComicChecklist.Presentation.UI.Models;

namespace ComicChecklist.Presentation.UI.Services
{
    public interface IChecklistApiService
    {
        Task<List<ChecklistModel>> GetAvailableChecklists();
        Task<SubscriptionFullModel> GetSubscriptionAsync(int checklistId);
        Task<List<SubscriptionSummaryModel>> GetSubscriptionsAsync();
        Task SubscribeToChecklist(int checklistId);
        Task UpdateUserIssue(int checklistId, List<UserIssueUpdateModel> issuesToUpdate);
    }
}
