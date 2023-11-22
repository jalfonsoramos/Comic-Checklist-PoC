using ComicChecklist.Presentation.UI.Models;
using ComicChecklist.Presentation.UI.ViewModels;

namespace ComicChecklist.Presentation.UI.Views;

public partial class ChecklistsPage : ContentPage
{
    public ChecklistsPage(ChecklistsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}