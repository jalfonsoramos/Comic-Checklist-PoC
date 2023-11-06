namespace ComicChecklist.Presentation.Api.Models
{
    public record ChecklistModel(int Id, string Name, IssueModel[] Issues);
}
