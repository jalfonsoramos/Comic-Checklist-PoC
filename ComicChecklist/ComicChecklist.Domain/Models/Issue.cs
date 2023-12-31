﻿namespace ComicChecklist.Domain.Models
{
    public class Issue : Entity
    {
        public Issue()
        {
            UserIssueStatuses = new HashSet<UserIssueStatus>();
        }

        public int Order { get; set; }

        public string Title { get; set; }

        public int ChecklistId { get; set; }

        public Checklist Checklist { get; set; }

        public ICollection<UserIssueStatus> UserIssueStatuses { get; set; }
    }
}
