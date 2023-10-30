namespace ComicChecklist.Domain.Models
{
    public class Checklist : Entity
    {
        public string Name { get; set; }

        public ICollection<Issue> Issues { get; set; }
    }
}
