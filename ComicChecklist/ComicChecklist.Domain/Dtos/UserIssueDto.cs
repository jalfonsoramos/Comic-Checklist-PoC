using ComicChecklist.Domain.Enums;

namespace ComicChecklist.Domain.Dtos
{
    public record UserIssueDto(int IssueId, string IssueTitle, IssueStatus IssueStatus);
}
