using ComicChecklist.Presentation.UI.Views;

namespace ComicChecklist.Presentation.UI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(SubscriptionsPage), typeof(SubscriptionsPage));
            Routing.RegisterRoute(nameof(ChecklistsPage), typeof(ChecklistsPage));
            Routing.RegisterRoute(nameof(ChecklistDetailPage), typeof(ChecklistDetailPage));
            Routing.RegisterRoute(nameof(UpdateIssuesPage), typeof(UpdateIssuesPage));
        }
    }
}