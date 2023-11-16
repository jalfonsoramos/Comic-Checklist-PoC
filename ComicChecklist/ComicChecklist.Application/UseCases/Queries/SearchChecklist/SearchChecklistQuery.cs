using ComicChecklist.Domain.Dtos;
using MediatR;

namespace ComicChecklist.Application.UseCases.Queries
{
    public record SearchChecklistQuery(string Name, int Index) : IRequest<ChecklistDto[]>;
}
