using ComicChecklist.Application.Interfaces.Repositories;
using ComicChecklist.Domain.Dtos;
using MediatR;

namespace ComicChecklist.Application.UseCases.Commands
{
    public class UpdateUserIssueHandler : IRequestHandler<UpdateUserIssueCommand, UseCaseResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IChecklistRepository _checklistRepository;
        private readonly IUserChecklistRepository _userChecklistRepository;
        private readonly IUserIssuesRepository _userIssuesRepository;

        public UpdateUserIssueHandler(IUserRepository userRepository,
                                      IUserIssuesRepository userIssuesRepository,
                                      IUserChecklistRepository userChecklistRepository,
                                      IChecklistRepository checklistRepository)
        {
            _userRepository = userRepository;
            _userIssuesRepository = userIssuesRepository;
            _userChecklistRepository = userChecklistRepository;
            _checklistRepository = checklistRepository;
        }

        public async Task<UseCaseResult> Handle(UpdateUserIssueCommand request, CancellationToken cancellationToken)
        {
            var userId = await _userRepository.GetUserIdByNameAsync(request.UserName);

            var checklist = await _checklistRepository.GetByIdAsync(request.ChecklistId);

            if (checklist == null)
            {
                return UseCaseResult.CreateFailResult("Checklist not found.");
            }

            if (!await _userChecklistRepository.IsSubscribed(userId, request.ChecklistId))
            {
                return UseCaseResult.CreateFailResult("User is not subscribed to checklist.");
            }

            if (request.UpdateIssues == null || request.UpdateIssues.Length == 0)
            {
                return UseCaseResult.CreateFailResult("Update issue list is empty.");
            }

            if (request.UpdateIssues.Select(x => x.IssueId).Distinct().Count() != request.UpdateIssues.Length)
            {
                return UseCaseResult.CreateFailResult("Duplicated elements found in update issue list.");
            }

            foreach (var updateIssue in request.UpdateIssues)
            {
                if (!checklist.Issues.Any(x => x.Id == updateIssue.IssueId))
                {
                    return UseCaseResult.CreateFailResult("Issue does not belong to checklist.");
                }

                if (!await _userIssuesRepository.IssueStatusExistAsync(userId, updateIssue.IssueId))
                {
                    await _userIssuesRepository.CreateUserIssueStatusAsync(userId, updateIssue.IssueId, updateIssue.IssueStatus);
                }
                else
                {
                    await _userIssuesRepository.UpdateUserIssueStatusAsync(userId, updateIssue.IssueId, updateIssue.IssueStatus);
                }
            }

            await _userIssuesRepository.SaveChangesAsync();

            return UseCaseResult.CreateSuccessResult();
        }
    }
}