using ComicChecklist.Presentation.UI.ViewModels;

namespace ComicChecklist.Presentation.UI.Views;

public partial class UpdateIssuesPage : ContentPage
{
    public UpdateIssuesPage(UpdateIssuesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is UpdateIssuesViewModel viewModel)
        {
            viewModel.LoadSubscriptionCommand.Execute(null);
        }
    }
}