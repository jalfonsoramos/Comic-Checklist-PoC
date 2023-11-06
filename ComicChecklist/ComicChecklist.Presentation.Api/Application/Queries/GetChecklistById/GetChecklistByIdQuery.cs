using ComicChecklist.Presentation.Api.Models;
using MediatR;

namespace ComicChecklist.Presentation.Api.Application.Queries
{
    public record GetChecklistByIdQuery(int checkListId) : IRequest<ChecklistDto>;
}