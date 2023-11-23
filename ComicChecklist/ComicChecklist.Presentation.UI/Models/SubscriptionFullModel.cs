namespace ComicChecklist.Presentation.UI.Models
{
    public record SubscriptionFullModel(int ChecklistId, string ChecklistName, List<UserIssueModel> Issues);
}
