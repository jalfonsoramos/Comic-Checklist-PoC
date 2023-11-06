using ComicChecklist.Domain.Dtos;
using MediatR;

namespace ComicChecklist.Application.Queries
{
    public record SearchChecklistQuery(string Name, int Index) : IRequest<ChecklistDto[]>;
}
