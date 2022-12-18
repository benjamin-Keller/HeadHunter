using HeadHunter.Client.Services;
using HeadHunter.Shared.Auth;
using Microsoft.AspNetCore.Components;

namespace HeadHunter.Client.Pages.Auth;

public partial class RiotLogin
{
    [Inject] public StateContainer StateContainer { get; set; }
    public RiotUser User { get; set; } = new();

    public async Task OnValidLoginSubmit()
    {
        await StateContainer.GetUserInfo(User);
    }
}