using ComicChecklist.Data.Repositories;
using ComicChecklist.Presentation.Api.Models;
using MediatR;

namespace ComicChecklist.Presentation.Api.Application.Queries
{
    public class GetChecklistByIdHandler : IRequestHandler<GetChecklistByIdQuery, ChecklistDto>
    {
        private readonly IChecklistRepository _checklistRepository;

        public GetChecklistByIdHandler(IChecklistRepository checklistRepository)
        {
            _checklistRepository = checklistRepository;
        }

        public async Task<ChecklistDto> Handle(GetChecklistByIdQuery query, CancellationToken cancellationToken)
        {
            var checklist = await _checklistRepository.GetByIdAsync(query.checkListId);

            if (checklist == null)
            {
                return null;
            }

            var checklistDto = new ChecklistDto(checklist.Id,
                                                checklist.Name,
                                                checklist.Issues.OrderBy(x => x.Order).Select(x => new IssueDto(x.Id, x.Title)).ToArray());

            return checklistDto;
        }
    }
}