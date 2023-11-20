namespace ComicChecklist.Domain.Dtos
{
    public record SubscriptionDto(int ChecklistId, string ChecklistName, int TotalIssues, int CompletedIssues, double PercentageCompleted);
}
