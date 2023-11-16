using ComicChecklist.Domain.Dtos;
using MediatR;

namespace ComicChecklist.Application.UseCases.Queries
{
    public record GetAvailableChecklistsQuery(string UserName) : IRequest<ChecklistDto[]>;
}
