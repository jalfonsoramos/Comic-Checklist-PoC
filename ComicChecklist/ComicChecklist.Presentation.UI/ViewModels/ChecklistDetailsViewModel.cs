using ComicChecklist.Presentation.UI.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ComicChecklist.Presentation.UI.ViewModels
{
    [QueryProperty("Checklist", "Checklist")]
    public partial class ChecklistDetailsViewModel : BaseViewModel
    {
        public ChecklistDetailsViewModel()
        {
            Title = "Checklist Details";
        }

        [ObservableProperty]
        ChecklistModel checklist;
    }
}
