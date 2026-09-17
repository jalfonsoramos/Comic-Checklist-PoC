using ComicChecklist.Domain.Dtos;
using MediatR;

namespace ComicChecklist.Application.UseCases.Commands
{
    public record SubscribeToChecklistCommand(string UserName, int ChecklistId) : IRequest<UseCaseResult>;
}
