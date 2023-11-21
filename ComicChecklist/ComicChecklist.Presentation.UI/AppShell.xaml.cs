using ComicChecklist.Presentation.UI.Views;

namespace ComicChecklist.Presentation.UI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ChecklistsPage), typeof(ChecklistsPage));
        }
    }
}