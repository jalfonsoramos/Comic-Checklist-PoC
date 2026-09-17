using ComicChecklist.Domain.Enums;

namespace ComicChecklist.Domain.Dtos
{
    public record UserIssueUpdateDto(int IssueId, IssueStatus IssueStatus);
}
