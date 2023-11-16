using ComicChecklist.Domain.Dtos;
using MediatR;

namespace ComicChecklist.Application.UseCases.Commands
{
    public record CreateChecklistCommand(ChecklistDto Checklist) : IRequest<ChecklistDto>;
}
