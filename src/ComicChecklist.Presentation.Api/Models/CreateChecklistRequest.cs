using ComicChecklist.Domain.Dtos;

namespace ComicChecklist.Presentation.Api.Models
{
    public record CreateChecklistRequest(string Name, string[] Issues);
}
