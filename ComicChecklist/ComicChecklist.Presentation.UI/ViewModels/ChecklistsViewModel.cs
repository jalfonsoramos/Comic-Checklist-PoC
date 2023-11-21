using System.Collections.ObjectModel;
using ComicChecklist.Presentation.UI.Models;
using ComicChecklist.Presentation.UI.Services;

namespace ComicChecklist.Presentation.UI.ViewModels
{
    public class ChecklistsViewModel : BindableObject
    {
        private ObservableCollection<ChecklistModel> _availableChecklists;
        private readonly IChecklistApiService _checklistApiService;

        public ChecklistsViewModel(IChecklistApiService checklistApiService)
        {
            _checklistApiService = checklistApiService;
        }

        public ObservableCollection<ChecklistModel> AvailableChecklists
        {
            get => _availableChecklists;
            set
            {
                _availableChecklists = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadData()
        {
            var availableChecklists = await _checklistApiService.GetAvailableChecklists();
            AvailableChecklists = new ObservableCollection<ChecklistModel>(availableChecklists);
        }
    }
}
