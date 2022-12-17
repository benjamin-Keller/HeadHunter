using HeadHunter.Shared.Auth;
using Newtonsoft.Json;
using RiotCloudflareAuthFix;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;

namespace HeadHunter.Server.Services.Auth
{
    public class AuthHandler
    {
        private readonly AuthenticationJsonClient _client;
        private readonly HttpClient _httpClient;

        public AuthHandler(HttpClient httpClient)
        {
            _client = new AuthenticationJsonClient
            {
                SerializerOptions = new() { PropertyNameCaseInsensitive = true }
            };
            _client.DefaultRequestHeaders.Add("User-Agent", "RiotClient/62.0.1.4852117.4789131 rso-auth (Windows;11;;Professional, x64)");
            _client.DefaultRequestHeaders.Add("X-Curl-Source", "Api");
            _httpClient = httpClient;
        }

        public async Task<(string AuthRequest, IEnumerable<Cookie> CookiesCollection, string AccessToken)> HandleAuthAsync(RiotUser user)
        {
            var preAuth = new
            {
                client_id = "play-valorant-web-prod",
                redirect_uri = "https://playvalorant.com/opt_in",
                response_type = "token id_token",
                response_mode = "query",
                scope = "account openid",
                nonce = 1,
            };
            var authCookiesRequestResult = await _client.PostAsync<object>(new Uri("https://auth.riotgames.com/api/v1/authorization"), preAuth);

            var authCookies = ParseSetCookie(authCookiesRequestResult.Message.Headers);

            var auth = new
            {
                user.Username,
                user.Password,
                type = "auth",
                remember = "true"
            };
            var authRequestResult = await _client.PutAsync<object>(new Uri("https://auth.riotgames.com/api/v1/authorization"), auth, cookies: authCookies);
            var authResult = await authRequestResult.Message.Content.ReadAsStringAsync();
            return (authResult, authCookies, Regex.Match(authResult, @"access_token=(.+?)&scope=").Groups[1].Value);
        }

        private IEnumerable<Cookie> ParseSetCookie(HttpHeaders headers)
        {
            if (headers.TryGetValues("Set-Cookie", out var cookies))
            {
                return cookies.Select(cookie => cookie.Split('=', 2))
                    .Select(cookieParts => new Cookie(cookieParts[0], cookieParts[1]));
            }
            return Enumerable.Empty<Cookie>();
        }

        public async Task<(string, string)> GetEntitlementAsync(string accessToken, IEnumerable<Cookie> cookieCollection)
        {
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            var entitlementRequestResult = await _client.PostAsync<object>(new Uri("https://entitlements.auth.riotgames.com/api/token/v1"), new { }, cookies: cookieCollection);

            var entitlementResult = await entitlementRequestResult.Message.Content.ReadAsStringAsync();
            return (entitlementResult, Regex.Match(entitlementResult, @"access_token=(.+?)&scope=").Groups[1].Value);
        }

        public async Task<UserInfo> GetUserInfo()
        {
            var userInfoRequestResult = await _client.PostAsync<object>(new Uri("https://auth.riotgames.com/userinfo"), new { });

            var userInfoResult = await userInfoRequestResult.Message.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserInfo>(userInfoResult);
        }
    }
}
