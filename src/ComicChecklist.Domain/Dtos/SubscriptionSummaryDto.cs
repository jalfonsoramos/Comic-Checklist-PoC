namespace ComicChecklist.Domain.Dtos
{
    public record SubscriptionSummaryDto(int ChecklistId, string ChecklistName, int TotalIssues, int CompletedIssues, double PercentageCompleted);
}
