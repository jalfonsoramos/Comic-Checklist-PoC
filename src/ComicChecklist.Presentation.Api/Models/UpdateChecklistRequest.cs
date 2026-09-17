using ComicChecklist.Domain.Dtos;

namespace ComicChecklist.Presentation.Api.Models
{
    public record UpdateChecklistRequest(string Name, IssueDto[] Issues);
}
