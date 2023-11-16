using ComicChecklist.Domain.Dtos;
using MediatR;

namespace ComicChecklist.Application.UseCases.Queries
{
    public record GetChecklistByIdQuery(int CheckListId) : IRequest<ChecklistDto>;
}