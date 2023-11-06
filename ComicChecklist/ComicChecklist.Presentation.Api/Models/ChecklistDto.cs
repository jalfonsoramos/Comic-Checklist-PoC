namespace ComicChecklist.Presentation.Api.Models
{
    public record ChecklistDto(int Id, string Name, IssueDto[] Issues);
}
