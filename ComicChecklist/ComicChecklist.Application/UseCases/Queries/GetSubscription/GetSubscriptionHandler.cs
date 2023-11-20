using ComicChecklist.Application.Interfaces.Repositories;
using ComicChecklist.Domain.Dtos;
using MediatR;

namespace ComicChecklist.Application.UseCases.Queries
{
    public class GetSubscriptionHandler : IRequestHandler<GetSubscriptionQuery, UseCaseResult<SubscriptionFullDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetSubscriptionHandler(IUserRepository userRepository, IUserChecklistRepository userChecklistRepository)
        {
            _userRepository = userRepository;
            _userChecklistRepository = userChecklistRepository;
        }

        private readonly IUserChecklistRepository _userChecklistRepository;

        public async Task<UseCaseResult<SubscriptionFullDto>> Handle(GetSubscriptionQuery request, CancellationToken cancellationToken)
        {
            var userId = await _userRepository.GetUserIdByNameAsync(request.UserName);

            var subscription = await _userChecklistRepository.GetSubscriptionByUserIdAsync(userId, request.ChecklistId);

            if (subscription == null)
            {
                return UseCaseResult<SubscriptionFullDto>.CreateFailResult("Subscription not found");
            }

            return UseCaseResult<SubscriptionFullDto>.CreateSuccessResult(subscription);
        }
    }
}
