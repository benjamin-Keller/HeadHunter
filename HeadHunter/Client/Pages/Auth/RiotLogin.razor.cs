using HeadHunter.Client.Services;
using HeadHunter.Shared.Auth;
using Microsoft.AspNetCore.Components;

namespace HeadHunter.Client.Pages.Auth;

public partial class RiotLogin : IDisposable
{
    [Inject] private StateContainer StateContainer { get; set; }
    [Inject] private NavigationManager NavManager { get; set; }
    public RiotUser User { get; set; } = new();

    public async Task OnValidLoginSubmit()
    {
        await StateContainer.GetUserInfo(User);
        StateContainer.OnStateChange += StateHasChanged;
        NavManager.NavigateTo("/");
    }

    public void Dispose()
    {
        StateContainer.OnStateChange -= StateHasChanged;
    }
}