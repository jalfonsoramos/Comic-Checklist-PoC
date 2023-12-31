﻿using ComicChecklist.Presentation.UI.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ComicChecklist.Presentation.UI.Services
{
    public class ChecklistApiService : IChecklistApiService
    {
        private readonly HttpClient _httpClient;

        // TODO: Get a token and store in secrets in the login page. This hardcodedtoken is for test user User1.
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
            _httpClient.SetAccessToken(token);

            var response = await _httpClient.GetAsync($"/checklists/{checklistId}/subscriptions");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var subscription = JsonConvert.DeserializeObject<SubscriptionFullModel>(content);

            return subscription;
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

        public async Task UpdateUserIssue(int checklistId, List<UserIssueUpdateModel> issuesToUpdate)
        {
            _httpClient.SetAccessToken(token);

            var payload = JsonConvert.SerializeObject(issuesToUpdate);

            var httpContent = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/checklists/{checklistId}/subscriptions", httpContent);

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
