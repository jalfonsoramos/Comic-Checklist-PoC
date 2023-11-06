namespace ComicChecklist.Presentation.Api.Models
{
    public record CreateChecklistRequest(string Name, IssueModel[] Issues);
}
