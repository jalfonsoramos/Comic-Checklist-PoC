﻿using ComicChecklist.Presentation.UI.Enums;
using ComicChecklist.Presentation.UI.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ComicChecklist.Presentation.UI.Services
{
    public class ChecklistApiService : IChecklistApiService
    {
        private readonly HttpClient _httpClient;

        string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IlVzZXIxIiwicm9sZSI6InJvbGVfdXNlciIsIm5iZiI6MTcwMDUyMjQwMiwiZXhwIjoxNzAwNTIzMDAyLCJpYXQiOjE3MDA1MjI0MDJ9.ohbLb9hGqFHSNxNo1y-wy4lLPNvCyT24BimwrInG4Oo";

        public ChecklistApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ChecklistModel>> GetAvailableChecklists()
        {
            _httpClient.SetAccessToken(token);

            var response = await _httpClient.GetAsync("/checklists");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var checklists = JsonConvert.DeserializeObject<List<ChecklistModel>>(content);

            return checklists;
        }

        public async Task<SubscriptionFullModel> GetSubscriptionAsync(int checklistId)
        {
            return await Task.FromResult(new SubscriptionFullModel(checklistId, "fake issue", new List<UserIssueModel>
            {
                new UserIssueModel
                {
                    IssueId =1,
                    IssueTitle = "issue1",
                    IssueStatus= IssuesStatusOptions.Completed
                },
            }));
        }

        public async Task<List<SubscriptionSummaryModel>> GetSubscriptionsAsync()
        {
            _httpClient.SetAccessToken(token);

            var response = await _httpClient.GetAsync("/checklists/subscriptions");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var subscriptions = JsonConvert.DeserializeObject<List<SubscriptionSummaryModel>>(content);

            return subscriptions;
        }

        public async Task SubscribeToChecklist(int checklistId)
        {
            _httpClient.SetAccessToken(token);

            var response = await _httpClient.PostAsync($"/checklists/{checklistId}/subscriptions", null);
            response.EnsureSuccessStatusCode();
        }
    }

    public static class Extensions
    {
        public static void SetAccessToken(this HttpClient httpClient, string token)
        {
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
