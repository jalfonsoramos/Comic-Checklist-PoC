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
        }

        [RelayCommand]
        async Task LoadSubscription()
        {
            try
            {
                var subscription = await _checklistApiService.GetSubscriptionAsync(ChecklistId);

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
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"Unable to get subscription {ex.Message}", "Ok");
            }
        }

        [RelayCommand]
        async Task UpdateIssueAsync()
        {
            try
            {
                // TODO: Update only if the issues with new status.

                var issuesToUpdate = Issues.Select(x => new UserIssueUpdateModel(x.IssueId, x.IssueStatus)).ToList();

                await _checklistApiService.UpdateUserIssue(ChecklistId, issuesToUpdate);

                LoadSubscriptionCommand.Execute(this);

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", $"Unable to update issue {ex.Message}", "Ok");
            }
        }
    }
}
