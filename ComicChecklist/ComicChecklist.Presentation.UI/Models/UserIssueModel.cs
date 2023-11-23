using ComicChecklist.Presentation.UI.Enums;

namespace ComicChecklist.Presentation.UI.Models
{
    public class UserIssueModel
    {
        public int IssueId { get; set; }
        
        public string IssueTitle { get; set; }

        public IssuesStatusOptions IssueStatus { get; set; }
    }
}
