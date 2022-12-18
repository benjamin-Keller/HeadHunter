using HeadHunter.Client.Services;
using HeadHunter.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace HeadHunter.Client.Pages;

public partial class Index
{
    [Inject] private StateContainer StateContainer { get; set; }
    [Inject] private HttpClient _httpClient { get; set; }
    public EventsModel EventData { get; set; } = new();
    protected override async Task OnInitializedAsync()
    {
        EventData = await _httpClient.GetFromJsonAsync<EventsModel>("api/events");
    }
}