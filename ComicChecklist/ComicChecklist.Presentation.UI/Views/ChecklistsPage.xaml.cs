using ComicChecklist.Presentation.UI.Models;
using ComicChecklist.Presentation.UI.ViewModels;

namespace ComicChecklist.Presentation.UI.Views;

public partial class ChecklistsPage : ContentPage
{
    private readonly ChecklistsViewModel _viewModel;

    public ChecklistsPage(ChecklistsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadData();
    }
}