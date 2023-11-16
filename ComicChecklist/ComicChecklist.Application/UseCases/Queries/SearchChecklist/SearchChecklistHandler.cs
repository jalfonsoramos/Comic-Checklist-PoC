using ComicChecklist.Application.Interfaces.Repositories;
using ComicChecklist.Domain.Dtos;
using MediatR;

namespace ComicChecklist.Application.UseCases.Queries
{
    public class SearchChecklistHandler : IRequestHandler<SearchChecklistQuery, ChecklistDto[]>
    {
        private readonly IChecklistRepository _checklistRepository;

        public SearchChecklistHandler(IChecklistRepository checklistRepository)
        {
            _checklistRepository = checklistRepository;
        }

        public async Task<ChecklistDto[]> Handle(SearchChecklistQuery request, CancellationToken cancellationToken)
        {
            if (request.Index < 0)
            {
                throw new ArgumentException($"The parameter {nameof(Index)} is < 0.");
            }

            var checklists = await _checklistRepository.Search(request.Name, request.Index * 10, 10);

            var checklistDtos = checklists.Select(checklist => new ChecklistDto(checklist.Id,
                                                                                    checklist.Name,
                                                                                    checklist.Issues.OrderBy(x => x.Order).Select(x => new IssueDto(x.Id, x.Title)).ToArray()));
            return checklistDtos.ToArray();
        }
    }
}
