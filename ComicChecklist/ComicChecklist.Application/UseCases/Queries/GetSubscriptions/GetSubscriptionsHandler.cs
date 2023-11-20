using ComicChecklist.Application.Interfaces.Repositories;
using ComicChecklist.Domain.Dtos;
using MediatR;

namespace ComicChecklist.Application.UseCases.Queries
{
    public class GetSubscriptionsHandler : IRequestHandler<GetSubscriptionsQuery, SubscriptionDto[]>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserChecklistRepository _userChecklistRepository;

        public GetSubscriptionsHandler(IUserRepository userRepository, IUserChecklistRepository userChecklistRepository)
        {            
            _userRepository = userRepository;
            _userChecklistRepository = userChecklistRepository;
        }

        public async Task<SubscriptionDto[]> Handle(GetSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            var userId = await _userRepository.GetUserIdByNameAsync(request.UserName);

            var subscriptions = await _userChecklistRepository.GetSubscriptionsByUserIdAsync(userId);
           
            return subscriptions.ToArray();
        }
    }
}
