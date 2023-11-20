namespace ComicChecklist.Domain.Dtos
{
    public record ChecklistDto(int Id, string Name, IssueDto[] Issues);

    public record ChecklistDTO(int id, string Name, List<double> Progress);
}
