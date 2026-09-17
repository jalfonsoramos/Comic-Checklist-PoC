namespace ComicChecklist.Domain.Models
{
    public class User : Entity
    {
        public string UserName { get; set; }

        public IList<UserChecklist> UserChecklists { get; set; }

        public IList<UserIssueStatus> UserIssueStatuses { get; set; }
    }
}
