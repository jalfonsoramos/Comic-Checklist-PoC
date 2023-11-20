using ComicChecklist.Application.Interfaces.Repositories;
using ComicChecklist.Domain.Dtos;
using MediatR;

namespace ComicChecklist.Application.UseCases.Commands
{
    public class SubscribeToChecklistHandler : IRequestHandler<SubscribeToChecklistCommand, UseCaseResult>
    {
        private readonly IChecklistRepository _checklistRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserChecklistRepository _userChecklistRepository;

        public SubscribeToChecklistHandler(IChecklistRepository checklistRepository, IUserRepository userRepository, IUserChecklistRepository userChecklistRepository)
        {
            _checklistRepository = checklistRepository;
            _userRepository = userRepository;
            _userChecklistRepository = userChecklistRepository;
        }

        public async Task<UseCaseResult> Handle(SubscribeToChecklistCommand request, CancellationToken cancellationToken)
        {
            var userId = await _userRepository.GetUserIdByNameAsync(request.UserName);

            var avaiableChecklists = await _checklistRepository.GetAvailableChecklistsAsync(userId);

            if (!avaiableChecklists.Any(x => x.Id == request.ChecklistId))
            {
                return UseCaseResult.CreateFailResult("The checklist is not available for subscription.");
            }

            await _userChecklistRepository.SubscribeToChecklistAsync(userId, request.ChecklistId);

            await _userChecklistRepository.SaveChangesAsync();

            return UseCaseResult.CreateSuccessResult();
        }
    }
}
