using ComicChecklist.Application.Interfaces.Repositories;
using ComicChecklist.Domain.Dtos;
using MediatR;

namespace ComicChecklist.Application.UseCases.Queries
{
    public class GetSubscriptionsHandler : IRequestHandler<GetSubscriptionsQuery, ChecklistDto[]>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserChecklistRepository _userChecklistRepository;

        public GetSubscriptionsHandler(IUserRepository userRepository, IUserChecklistRepository userChecklistRepository)
        {            
            _userRepository = userRepository;
            _userChecklistRepository = userChecklistRepository;
        }

        public async Task<ChecklistDto[]> Handle(GetSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            var userId = await _userRepository.GetUserIdByNameAsync(request.UserName);

            var userChecklists = await _userChecklistRepository.GetSubscriptionsByUserIdAsync(userId);

            var checklists = userChecklists.Select(x => new ChecklistDto(x.Checklist.Id,
                                                                            x.Checklist.Name,
                                                                            Enumerable.Empty<IssueDto>().ToArray()));
            return checklists.ToArray();
        }
    }
}
