using System.Collections.ObjectModel;
using System.Windows.Input;
using ComicChecklist.Presentation.UI.Models;
using ComicChecklist.Presentation.UI.Services;
using ComicChecklist.Presentation.UI.Views;

namespace ComicChecklist.Presentation.UI.ViewModels
{
    public class SubscriptionsViewModel : BindableObject
    {
        private ObservableCollection<SubscriptionSummaryModel> _subscriptions;

        private ICommand _refreshCommand;
        private ICommand _viewChecklistsCommand;

        private readonly IChecklistApiService _checklistApiService;

        public SubscriptionsViewModel(IChecklistApiService checklistApiService)
        {
            _checklistApiService = checklistApiService;
        }

        public ObservableCollection<SubscriptionSummaryModel> Subscriptions
        {
            get => _subscriptions;
            set
            {
                _subscriptions = value;
                OnPropertyChanged();
            }
        }

        public ICommand RefreshCommand => _refreshCommand ??= new Command(async () => await LoadData());

        public ICommand ViewChecklistsCommand => _viewChecklistsCommand ??= new Command(async () => await ViewChecklists());

        public async Task LoadData()
        {
            var subscriptions = await _checklistApiService.GetSubscriptionsAsync();
            Subscriptions = new ObservableCollection<SubscriptionSummaryModel>(subscriptions);
        }

        async Task ViewChecklists()
        {
            await Shell.Current.GoToAsync(nameof(ChecklistsPage));
        }
    }
}
