namespace ComicChecklist.Domain.Models
{
    public class Checklist : Entity
    {
        public Checklist()
        {
            Issues = new HashSet<Issue>();
        }

        public string Name { get; set; }

        public ICollection<Issue> Issues { get; set; }
    }
}
