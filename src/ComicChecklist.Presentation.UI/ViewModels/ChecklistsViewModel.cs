using System.Collections.ObjectModel;
using ComicChecklist.Presentation.UI.Models;
using ComicChecklist.Presentation.UI.Services;
using ComicChecklist.Presentation.UI.Views;
using CommunityToolkit.Mvvm.Input;

namespace ComicChecklist.Presentation.UI.ViewModels
{
    public partial class ChecklistsViewModel : BaseViewModel
    {
        public ObservableCollection<ChecklistModel> AvailableChecklists { get; } = new();

        public bool ShowIsEmpty => !(AvailableChecklists.Count == 0);

        private readonly IChecklistApiService _checklistApiService;

        public ChecklistsViewModel(IChecklistApiService checklistApiService)
        {
            Title = "Available checklists";

            _checklistApiService = checklistApiService;            
        }

        [RelayCommand]
        async Task GetAvailableChecklistsAsync()
        {
            try
            {
                var availableChecklists = await _checklistApiService.GetAvailableChecklists();

                if (AvailableChecklists.Count != 0)
                {
                    AvailableChecklists.Clear();
                }

                foreach (var availableChecklist in availableChecklists)
                {
                    AvailableChecklists.Add(availableChecklist);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"Unable to get available checklists {ex.Message}", "Ok");
            }
        }

        [RelayCommand]
        async Task ViewChecklistAsync(ChecklistModel availableChecklist)
        {
            if (availableChecklist is null) return;

            await Shell.Current.GoToAsync(nameof(ChecklistDetailPage), true, new Dictionary<string, object>() { { "Checklist", availableChecklist } });
        }

        [RelayCommand]
        async Task SubscribeAsync(ChecklistModel availableChecklist)
        {
            try
            {
                await _checklistApiService.SubscribeToChecklist(availableChecklist.Id);

                GetAvailableChecklistsCommand.Execute(this);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"Unable to subscribe to checklist {ex.Message}", "Ok");
            }
        }
    }
}
