using SteamGamesWeAllCanPlayWASM.Shared.Models;
using System.Net;
using System.Net.Http.Json;

namespace SteamGamesWeAllCanPlayWASM.Client.Services
{
    public class UserService
    {
        public readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<MUserOverview> GetOverviewAsync(string steamId)
        {
            var response = await _httpClient.GetAsync($"api/user/{steamId}/overview");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var overview = await response.Content.ReadFromJsonAsync<MUserOverview>();
            
                return overview;
            }
            return null;
        }
    }
}
