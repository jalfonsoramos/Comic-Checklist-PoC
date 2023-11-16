using ComicChecklist.Application.Interfaces.Repositories;
using ComicChecklist.Domain.Dtos;
using MediatR;

namespace ComicChecklist.Application.UseCases.Commands
{
    public class SubscribeToChecklistHandler : IRequestHandler<SubscribeToChecklistCommand, UseCaseResult>
    {
        private readonly IChecklistRepository _checklistRepository;
        private readonly IUserRepository _userRepository;

        public SubscribeToChecklistHandler(IChecklistRepository checklistRepository, IUserRepository userRepository)
        {
            _checklistRepository = checklistRepository;
            _userRepository = userRepository;
        }

        public async Task<UseCaseResult> Handle(SubscribeToChecklistCommand request, CancellationToken cancellationToken)
        {
            var userId = await _userRepository.GetUserIdByNameAsync(request.UserName);

            var avaiableChecklists = await _checklistRepository.GetAvailableChecklistsAsync(userId);

            if (!avaiableChecklists.Any(x => x.Id == request.ChecklistId))
            {
                return UseCaseResult.CreateFailResult("The checklist is not available for subscription.");
            }

            await _checklistRepository.SubscribeToChecklist(userId, request.ChecklistId);

            await _checklistRepository.SaveChangesAsync();

            return UseCaseResult.CreateSuccessResult();
        }
    }
}
