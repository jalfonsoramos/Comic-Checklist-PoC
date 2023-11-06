using ComicChecklist.Domain.Dtos;
using MediatR;

namespace ComicChecklist.Application.Queries
{
    public record GetChecklistByIdQuery(int CheckListId) : IRequest<ChecklistDto>;
}