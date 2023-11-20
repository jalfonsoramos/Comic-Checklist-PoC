namespace ComicChecklist.Domain.Dtos
{
    public record SubscriptionFullDto(int ChecklistId, string ChecklistName, UserIssueDto[] issues);
}
