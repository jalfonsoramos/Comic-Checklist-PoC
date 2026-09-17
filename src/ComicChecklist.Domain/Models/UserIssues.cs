using ComicChecklist.Domain.Enums;

namespace ComicChecklist.Domain.Models
{
    public class UserIssueStatus
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int IssueId { get; set; }

        public Issue Issue { get; set; }

        public IssueStatus Status { get; set; }
    }
}
