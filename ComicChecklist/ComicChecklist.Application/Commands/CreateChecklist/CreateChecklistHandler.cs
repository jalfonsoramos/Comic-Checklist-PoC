using ComicChecklist.Domain.Dtos;
using ComicChecklist.Domain.Interfaces.Repositories;
using ComicChecklist.Domain.Models;
using MediatR;

namespace ComicChecklist.Application.Commands
{
    public class CreateChecklistHandler : IRequestHandler<CreateChecklistCommand, ChecklistDto>
    {
        private readonly IChecklistRepository _checklistRepository;

        public CreateChecklistHandler(IChecklistRepository checklistRepository)
        {
            _checklistRepository = checklistRepository;
        }

        public async Task<ChecklistDto> Handle(CreateChecklistCommand request, CancellationToken cancellationToken)
        {
            var checklist = new Checklist()
            {
                Name = request.Checklist.Name
            };

            if (request.Checklist.Issues != null && request.Checklist.Issues.Any())
            {
                var issues = request.Checklist.Issues.Select((item, index) => new Issue { Order = index, Title = item.Title });

                foreach (var issue in issues)
                {
                    checklist.Issues.Add(issue);
                }
            }

            _checklistRepository.Add(checklist);

            await _checklistRepository.SaveChangesAsync();

            var checklistDto = new ChecklistDto(checklist.Id,
                                                checklist.Name,
                                                checklist.Issues.Select(x => new IssueDto(x.Id, x.Title)).ToArray());

            return checklistDto;
        }
    }
}
