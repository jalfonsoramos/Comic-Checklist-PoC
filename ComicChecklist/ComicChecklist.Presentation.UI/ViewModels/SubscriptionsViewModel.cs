using System.Collections.ObjectModel;
using ComicChecklist.Presentation.UI.Models;
using ComicChecklist.Presentation.UI.Services;
using ComicChecklist.Presentation.UI.Views;
using CommunityToolkit.Mvvm.Input;

namespace ComicChecklist.Presentation.UI.ViewModels
{
    public partial class SubscriptionsViewModel : BaseViewModel
    {
        public ObservableCollection<SubscriptionSummaryModel> Subscriptions { get; } = new();

        public bool ShowIsEmpty => !(Subscriptions.Count == 0);

        private readonly IChecklistApiService _checklistApiService;

        public SubscriptionsViewModel(IChecklistApiService checklistApiService)
        {
            Title = "My Subscriptions";

            _checklistApiService = checklistApiService;
        }

        public SubscriptionsViewModel()
        {

        }

        [RelayCommand]
        async Task GetSubscriptionsAsync()
        {
            try
            {
                var subscriptions = await _checklistApiService.GetSubscriptionsAsync();

                if (Subscriptions.Count != 0)
                {
                    Subscriptions.Clear();
                }

                foreach (var subscription in subscriptions)
                {
                    Subscriptions.Add(subscription);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"Unable to get subscriptions {ex.Message}", "Ok");
            }
        }

        [RelayCommand]
        async Task ViewChecklistsAsync()
        {
            await Shell.Current.GoToAsync(nameof(ChecklistsPage));
        }
    }
}
