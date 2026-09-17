using ComicChecklist.Presentation.UI.Enums;

namespace ComicChecklist.Presentation.UI.Models
{
    public record UserIssueUpdateModel(int IssueId, IssuesStatusOptions IssueStatus);
}
