using ComicChecklist.Domain.Dtos;
using ComicChecklist.Presentation.Api.Models;

namespace ComicChecklist.Presentation.Api.ExtensionMethods
{
    public static class RequestExtensions
    {
        public static ChecklistDto ToChecklistDto(this CreateChecklistRequest request)
        {
            return new ChecklistDto(0, request.Name, request.Issues.Select(issue => new IssueDto(0, issue)).ToArray());
        }

        public static ChecklistDto ToChecklistDto(this UpdateChecklistRequest request, int id)
        {
            return new ChecklistDto(id, request.Name, request.Issues.Select(issue => new IssueDto(issue.Id, issue.Title)).ToArray());
        }
    }
}