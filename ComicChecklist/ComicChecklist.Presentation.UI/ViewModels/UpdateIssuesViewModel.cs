using ComicChecklist.Presentation.UI.Models;
using ComicChecklist.Presentation.UI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ComicChecklist.Presentation.UI.ViewModels
{
    [QueryProperty("ChecklistId", "ChecklistId")]
    public partial class UpdateIssuesViewModel : BaseViewModel
    {
        [ObservableProperty]
        int checklistId;

        [ObservableProperty]
        string checklistName;

        public ObservableCollection<UserIssueModel> Issues { get; } = new ObservableCollection<UserIssueModel>();
      
        private readonly IChecklistApiService _checklistApiService;

        public UpdateIssuesViewModel(IChecklistApiService checklistApiService)
        {
            Title = "Update user issues";

            _checklistApiService = checklistApiService;

            LoadSubscriptionCommand.Execute(this);
        }

        [RelayCommand]
        async Task LoadSubscription()
        {
            SubscriptionFullModel subscription = await _checklistApiService.GetSubscriptionAsync(ChecklistId);

            if (subscription == null)
            {
                return;
            }

            ChecklistName = subscription.ChecklistName;

            if (Issues.Count > 0)
            {
                Issues.Clear();
            }

            foreach (var issue in subscription.Issues)
            {
                Issues.Add(issue);
            }
        }

        [RelayCommand]
        async Task UpdateIssueAsync(UserIssueModel issue)
        {
            await Task.CompletedTask;
        }
    }
}
