namespace ComicChecklist.Domain.Models
{
    public class UserChecklist
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int ChecklistId { get; set; }

        public Checklist Checklist { get; set; }
    }
}
