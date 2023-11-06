namespace ComicChecklist.Domain.Dtos
{
    public record ChecklistDto(int Id, string Name, IssueDto[] Issues);
}
