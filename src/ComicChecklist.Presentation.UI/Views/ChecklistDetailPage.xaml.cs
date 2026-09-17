using ComicChecklist.Presentation.UI.ViewModels;

namespace ComicChecklist.Presentation.UI.Views;

public partial class ChecklistDetailPage : ContentPage
{
	public ChecklistDetailPage(ChecklistDetailsViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}