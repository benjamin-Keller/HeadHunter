using HeadHunter.Server.Services.Auth;
using HeadHunter.Shared.Auth;
using System.Text.RegularExpressions;

namespace HeadHunter.Server.Services
{
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

            var (entitlementResponse, entitlementToken) = await _authHandler.GetEntitlementAsync(accessToken, cookieCollection);

            return await _authHandler.GetUserInfo();
        }  
    }
}
