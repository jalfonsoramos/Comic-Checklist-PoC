namespace ComicChecklist.Presentation.UI.Models
{
    public class SubscriptionSummaryModel
    {
        public int ChecklistId { get; set; }
        public string ChecklistName { get; set; }
        public int TotalIssues { get; set; }
        public int CompletedIssues { get; set; }
        public double PercentageCompleted { get; set; }

        public string Progress => $"{CompletedIssues} out of {TotalIssues} issues ({PercentageCompleted:0.##}%)";
    }
}
