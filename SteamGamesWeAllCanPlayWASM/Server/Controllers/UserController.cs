using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SteamGamesWeAllCanPlayWASM.Data.Repositories;
using SteamGamesWeAllCanPlayWASM.Shared.Models;
using SteamWebAPI2.Interfaces;
using SteamWebAPI2.Utilities;
using System.Security.Claims;

namespace SteamGamesWeAllCanPlayWASM.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private readonly IUserRepository _userRepo;
        private readonly SteamWebInterfaceFactory _steamFactory;
        private readonly HttpClient _client;

        public UserController(IUserRepository userRepo, SteamWebInterfaceFactory steamFactory, HttpClient client)
        {
            _userRepo = userRepo;
            _steamFactory = steamFactory;
            _client = client;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMeUserAsync()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                return Ok(await _userRepo.GetByIdAsync(int.Parse(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value)));
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
        }


        [ResponseCache(NoStore = true, Duration = 0)]
        [HttpGet("~/signin"), HttpPost("~/signin")]
        public IActionResult SignIn()
        {
            string returnUrl = "/";
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = returnUrl,
                IsPersistent = true,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, "Steam");
        }

        [ResponseCache(NoStore = true, Duration = 0)]
        [HttpGet("~/signout"), HttpPost("~/signout")]
        public IActionResult LogOut()
        {
            string returnUrl = "/";
            return SignOut(new AuthenticationProperties
            {
                RedirectUri = returnUrl,
            }, CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [HttpGet("{steamId}/overview")]
        public async Task<IActionResult> GetUserOverview(string steamId)
        {
            var playerSummary = await _steamFactory.CreateSteamWebInterface<SteamUser>(_client).GetPlayerSummaryAsync(ulong.Parse(steamId));
            var friendListResponse = await _steamFactory.CreateSteamWebInterface<SteamUser>(_client).GetFriendsListAsync(ulong.Parse(steamId));
            var friendPlayerSummariesResponse = await _steamFactory.CreateSteamWebInterface<SteamUser>(_client).GetPlayerSummariesAsync(friendListResponse.Data.Select(p => p.SteamId).ToList());
            var friendCount = friendListResponse.Data.Count();
            var onlineFriendCount = friendPlayerSummariesResponse.Data.Count(s => s.UserStatus != Steam.Models.SteamCommunity.UserStatus.Offline);

            var gamesResponse = await _steamFactory.CreateSteamWebInterface<PlayerService>(_client).GetOwnedGamesAsync(ulong.Parse(steamId), includeFreeGames: true);
            var gameCount = gamesResponse.Data.GameCount;

            var overview = new MUserOverview()
            {
                FriendsOnline = onlineFriendCount,
                FriendsCount = friendCount,
                GameCount = gameCount,
                AvatarFullURL = playerSummary.Data.AvatarFullUrl
            };

            return Ok(overview);
        }

        [HttpGet("{steamId}/friendlist")]
        public async Task<IActionResult> GetUserFriendList(string steamId)
        {
            var friendListResponse = await _steamFactory.CreateSteamWebInterface<SteamUser>(_client).GetFriendsListAsync(ulong.Parse(steamId));
            var playerSummariesResponse = await _steamFactory.CreateSteamWebInterface<SteamUser>(_client).GetPlayerSummariesAsync(friendListResponse.Data.Select(p => p.SteamId).ToList());


            return Ok(playerSummariesResponse.Data);
        }

        [HttpGet("{steamId}/ownedgames")]
        public async Task<IActionResult> GetOwnedGames(string steamId)
        {
            var gamesResponse = await _steamFactory.CreateSteamWebInterface<PlayerService>(_client).GetOwnedGamesAsync(ulong.Parse(steamId), includeAppInfo: true, includeFreeGames: true);

            return Ok(gamesResponse.Data);
        }
    }
}
