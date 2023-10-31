namespace ComicChecklist.Api.Models
{
    public record CreateChecklistDto(string Name, CreateIssueDto[] Issues);
    public record UpdateChecklistDto(string Name, UpdateIssueDto[] Issues);
    public record CreateIssueDto(string Title);
    public record UpdateIssueDto(int Id, string Title, int Order);
    public record ChecklistDto(int Id, string Name, IssueDto[] Issues);
    public record IssueDto(int Id, string Title);
}
