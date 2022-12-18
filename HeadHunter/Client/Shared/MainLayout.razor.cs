using HeadHunter.Client.Services;
using Microsoft.AspNetCore.Components;

namespace HeadHunter.Client.Shared;

public partial class MainLayout
{
    [Inject] public StateContainer StateContainer { get; set; }
    public object Username { get; set; }

    protected override void OnInitialized()
    {
        Username = GetUsername();
    }
    private object GetUsername()
    {
        if (StateContainer.User is null)
            return "Login";
        else 
            return StateContainer.User.Acct.GameName;
    }
}