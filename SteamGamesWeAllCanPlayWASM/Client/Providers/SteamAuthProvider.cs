using Microsoft.AspNetCore.Components.Authorization;
using SteamGamesWeAllCanPlayWASM.Shared.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SteamGamesWeAllCanPlayWASM.Client.Providers
{
    public class SteamAuthProvider : AuthenticationStateProvider
    {
        private readonly HttpClient httpClient;

        public SteamAuthProvider(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public bool IsAuthenticated { get; private set; }
        public MUser User { get; private set; }

        public async Task UpdateUserAsync()
        {
            try
            {
                var response = await httpClient.GetAsync("api/user/me");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    User = await response.Content.ReadFromJsonAsync<MUser>();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private AuthenticationState authState;

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (authState != null)
            {
                return authState;
            }

            await UpdateUserAsync();
            ClaimsIdentity identity;
            if (User == null)
            {
                identity = new ClaimsIdentity();
            }
            else
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, User.Name),
                    new Claim(ClaimTypes.NameIdentifier, User.Id.ToString()),
                    new Claim("SteamId", User.SteamId),
                }, "Steam");
                IsAuthenticated = true;
            }

            var user = new ClaimsPrincipal(identity);

            authState = new AuthenticationState(user);
            return authState;
        }
    }
}
