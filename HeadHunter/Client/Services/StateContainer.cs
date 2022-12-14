using HeadHunter.Shared.Auth;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace HeadHunter.Client.Services;

public class StateContainer
{
    private readonly HttpClient _httpClient;

    public StateContainer(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public UserInfo User { get; set; }
    public event Action OnStateChange;
    private void NotifyStateChanged() => OnStateChange?.Invoke();

    public async Task GetUserInfo(RiotUser riotUser)
    {
        var userRequest = await _httpClient.PostAsJsonAsync("api/auth", riotUser);
        if (userRequest.IsSuccessStatusCode)
            User = JsonConvert.DeserializeObject<UserInfo>(await userRequest.Content.ReadAsStringAsync());

        NotifyStateChanged();
    }

    public string GetUsername()
    {
        if (User is not null)
            return $"{User.Acct.GameName}#{User.Acct.TagLine}";
        else
            return "Login";
    }
}
