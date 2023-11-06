namespace ComicChecklist.Presentation.Api.Models
{
    public record UpdateChecklistRequest(string Name, IssueModel[] Issues);
}
