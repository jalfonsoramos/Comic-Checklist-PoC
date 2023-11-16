namespace ComicChecklist.Domain.Models
{
    public class Issue : Entity
    {
        public int Order { get; set; }

        public string Title { get; set; }

        public int ChecklistId { get; set; }

        public Checklist Checklist { get; set; }

        public IList<UserIssueStatus> UserIssueStatuses { get; set; }
    }
}
