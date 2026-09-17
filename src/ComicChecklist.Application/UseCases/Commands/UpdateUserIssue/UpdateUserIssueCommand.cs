using ComicChecklist.Domain.Dtos;
using MediatR;

namespace ComicChecklist.Application.UseCases.Commands
{
    public record UpdateUserIssueCommand(string UserName, int ChecklistId, UserIssueUpdateDto[] UpdateIssues) : IRequest<UseCaseResult>;
}