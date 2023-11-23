using ComicChecklist.Presentation.UI.ViewModels;

namespace ComicChecklist.Presentation.UI.Views;

public partial class ChecklistsPage : ContentPage
{
    public ChecklistsPage(ChecklistsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ChecklistsViewModel viewModel)
        {
            viewModel.GetAvailableChecklistsCommand.Execute(this);
        }
    }
}