using ComicChecklist.Application.Interfaces.Repositories;
using ComicChecklist.Domain.Dtos;
using MediatR;

namespace ComicChecklist.Application.UseCases.Queries
{
    public record GetAvailableChecklistsQuery(string userName) : IRequest<ChecklistDto[]>;

    public class GetAvailableChecklistsHandler : IRequestHandler<GetAvailableChecklistsQuery, ChecklistDto[]>
    {
        private readonly IChecklistRepository _checklistRepository;
        private readonly IUserRepository _userRepository;

        public GetAvailableChecklistsHandler(IChecklistRepository checklistRepository, IUserRepository userRepository)
        {
            _checklistRepository = checklistRepository;
            _userRepository = userRepository;
        }

        public async Task<ChecklistDto[]> Handle(GetAvailableChecklistsQuery request, CancellationToken cancellationToken)
        {
            var userId = await _userRepository.GetUserByName(request.userName);

            var checklists = await _checklistRepository.GetAvailableChecklists(userId);

            var checklistDtos = checklists.Select(checklist => new ChecklistDto(checklist.Id,
                                                                                checklist.Name,
                                                                                checklist.Issues.OrderBy(x => x.Order).Select(x => new IssueDto(x.Id, x.Title)).ToArray()));

            return checklistDtos.ToArray();
        }
    }
}
