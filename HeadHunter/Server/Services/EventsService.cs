using HeadHunter.Shared;

namespace HeadHunter.Server.Services;

public class EventsService
{
    private readonly HttpClient _httpClient;

    public EventsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<EventsModel> GetEventsAsync() => await _httpClient.GetFromJsonAsync<EventsModel>("https://valorant-api.com/v1/events");
}
