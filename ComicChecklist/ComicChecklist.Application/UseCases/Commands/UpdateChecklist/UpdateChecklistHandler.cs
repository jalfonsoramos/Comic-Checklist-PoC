using ComicChecklist.Application.Interfaces.Repositories;
using ComicChecklist.Domain.Dtos;
using MediatR;

namespace ComicChecklist.Application.UseCases.Commands
{
    public class UpdateChecklistHandler : IRequestHandler<UpdateChecklistCommand, UseCaseResult<ChecklistDto>>
    {
        private readonly IChecklistRepository _checklistRepository;

        public UpdateChecklistHandler(IChecklistRepository checklistRepository)
        {
            _checklistRepository = checklistRepository;
        }

        public async Task<UseCaseResult<ChecklistDto>> Handle(UpdateChecklistCommand request, CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(request.Checklist.Name))
            {
                return UseCaseResult<ChecklistDto>.CreateFailResult("Checklist name is null or empty.");
            }

            var currentChecklist = await _checklistRepository.GetByIdAsync(request.Checklist.Id);

            if (currentChecklist == null)
            {
                return UseCaseResult<ChecklistDto>.CreateFailResult("Checklist not found.");
            }

            if (request.Checklist.Issues.Length == currentChecklist.Issues.Count)
            {
                if (request.Checklist.Issues.All(x => currentChecklist.Issues.Any(y => y.Id == x.Id)))
                {
                    currentChecklist.Name = request.Checklist.Name;

                    if(request.Checklist.Issues.Any(x=>string.IsNullOrEmpty(x.Title)))
                    {
                        return UseCaseResult<ChecklistDto>.CreateFailResult("Issue title null or empty found.");
                    }

                    for (var i = 0; i < request.Checklist.Issues.Length; i++)
                    {
                        var issueId = request.Checklist.Issues[i].Id;
                        var issue = currentChecklist.Issues.SingleOrDefault(x => x.Id == issueId);

                        if (issue != null)
                        {
                            issue.Order = i;
                        }
                    }
                }
                else
                {
                    return UseCaseResult<ChecklistDto>.CreateFailResult("Issue list is invalid: IDs does not match.");
                }
            }
            else
            {
                return UseCaseResult<ChecklistDto>.CreateFailResult("Issue list is invalid: Issues count mismatch.");
            }

            await _checklistRepository.SaveChangesAsync();

            var updatedChecklist = new ChecklistDto(currentChecklist.Id,
                                           currentChecklist.Name,
                                           currentChecklist.Issues.OrderBy(x => x.Order).Select(x => new IssueDto(x.Id, x.Title)).ToArray());

            return UseCaseResult<ChecklistDto>.CreateSuccessResult(updatedChecklist);
        }
    }
}
