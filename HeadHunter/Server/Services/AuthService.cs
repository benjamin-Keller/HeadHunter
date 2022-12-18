using HeadHunter.Server.Services.Auth;
using HeadHunter.Shared.Auth;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace HeadHunter.Server.Services;

public class AuthService
{
    private readonly AuthHandler _authHandler;

    public AuthService(AuthHandler authHandler)
    {
        _authHandler = authHandler;
    }

    public async Task<UserInfo> AuthenticateAsync(RiotUser user)
    {
        var (loginResponse, cookieCollection, accessToken) = await _authHandler.HandleAuthAsync(user);
        var entitlementToken = await _authHandler.GetEntitlementAsync(accessToken, cookieCollection);
        
        var userInfo = new UserInfo();
        userInfo = await _authHandler.GetUserInfo();
        userInfo.Entitlement = entitlementToken;

        return userInfo;
    }  
}
