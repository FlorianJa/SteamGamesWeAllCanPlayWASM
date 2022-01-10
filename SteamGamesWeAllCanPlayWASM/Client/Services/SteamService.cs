using Steam.Models.SteamCommunity;
using SteamGamesWeAllCanPlayWASM.Shared.Models;
using System.Net;
using System.Net.Http.Json;

namespace SteamGamesWeAllCanPlayWASM.Client.Services
{
    public class SteamService
    {
        public readonly HttpClient _httpClient;

        public SteamService(HttpClient httpClient)
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

        public async Task<IEnumerable<PlayerSummaryModel>> GetFriendList(string steamId)
        {
            var response = await _httpClient.GetAsync($"api/user/{steamId}/friendlist");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var friendList = await response.Content.ReadFromJsonAsync<IEnumerable<PlayerSummaryModel>>();

                return friendList;
            }
            return null;
        }

        public async Task<IReadOnlyCollection<OwnedGameModel>> GetOwnedGamesAsync(string steamId)
        {
            var response = await _httpClient.GetAsync($"api/user/{steamId}/ownedgames");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var ownedGames = await response.Content.ReadFromJsonAsync<OwnedGamesResultModel>();

                return ownedGames.OwnedGames;
            }
            return null;
        }

        public async Task<IEnumerable<OwnedGameModel>> GetComparedLibrariesAsync(IEnumerable<string> steamIds)
        {
            var mergedLibraries = new List<OwnedGameModel>();
            var allGameLibraries = new List<IEnumerable<OwnedGameModel>>();

            foreach (var steamId in steamIds)
            {
                allGameLibraries.Add(await GetOwnedGamesAsync(steamId));

            }

            allGameLibraries = allGameLibraries.OrderByDescending(l => l.Count()).ToList();


            foreach (var library in allGameLibraries)
            {
                if (mergedLibraries.Count == 0)
                {
                    mergedLibraries.AddRange(library);
                }
                else
                {
                    mergedLibraries = mergedLibraries.Intersect(library, new OwnedGameModelComparer() ).ToList();
                }
            }

            return mergedLibraries;
        }
    }
}
