namespace ComicChecklist.Domain.Models
{
    public class Checklist : Entity
    {
        public Checklist()
        {
            Issues = new HashSet<Issue>();
            UserChecklists = new HashSet<UserChecklist>();
        }

        public string Name { get; set; }

        public ICollection<Issue> Issues { get; set; }

        public ICollection<UserChecklist> UserChecklists { get; set; }
    }
}
