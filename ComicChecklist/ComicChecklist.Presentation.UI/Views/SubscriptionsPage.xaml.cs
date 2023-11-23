using ComicChecklist.Presentation.UI.ViewModels;

namespace ComicChecklist.Presentation.UI.Views;

public partial class SubscriptionsPage : ContentPage
{
    public SubscriptionsPage(SubscriptionsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is SubscriptionsViewModel viewModel)
        {
            viewModel.GetSubscriptionsCommand.Execute(null);
        }
    }    
}