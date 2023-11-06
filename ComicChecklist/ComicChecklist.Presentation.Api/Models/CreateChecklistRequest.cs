namespace ComicChecklist.Presentation.Api.Models
{
    public record CreateChecklistRequest(string Name, IssueDto[] Issues);
}
