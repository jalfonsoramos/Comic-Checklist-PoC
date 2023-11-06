using ComicChecklist.Domain.Dtos;
using MediatR;

namespace ComicChecklist.Application.Commands
{
    public record CreateChecklistCommand(ChecklistDto Checklist) : IRequest<ChecklistDto>;
}
