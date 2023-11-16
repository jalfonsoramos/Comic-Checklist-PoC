namespace ComicChecklist.Domain.Models
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public IList<UserChecklist> UserChecklists { get; set; }

        public IList<UserIssueStatus> UserIssueStatuses { get; set; }
    }
}
