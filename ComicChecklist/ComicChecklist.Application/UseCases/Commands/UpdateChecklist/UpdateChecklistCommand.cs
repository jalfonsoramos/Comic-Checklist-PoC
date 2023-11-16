using ComicChecklist.Domain.Dtos;
using MediatR;

namespace ComicChecklist.Application.UseCases.Commands
{
    public record UpdateChecklistCommand(ChecklistDto Checklist) : IRequest<ResultDto<ChecklistDto>>;
}
