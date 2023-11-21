namespace ComicChecklist.Presentation.UI.Models
{
    public class ChecklistModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<IssueModel> Issue { get; set; }
    }
}