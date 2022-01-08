﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
//using Steam.Models.SteamCommunity;
//using SteamWebAPI2.Interfaces;
//using SteamWebAPI2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
//using SteamGamesWeAllCanPlayWASM.Data.Repositories;
using SteamGamesWeAllCanPlayWASM.Shared.Models;
using SteamGamesWeAllCanPlayWASM.Data.Repositories;

namespace SteamGamesWeAllCanPlayWASM.Server.Helpers
{
    public class ValidationHelper
    {
        private const int SteamIdStartIndex = 37;

        public static async Task SignIn(CookieSignedInContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            string steamId = context.Principal.FindFirst(ClaimTypes.NameIdentifier).Value[SteamIdStartIndex..];

            var usersRepository = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
            //var imagesRepository = context.HttpContext.RequestServices.GetRequiredService<ImagesRepository>();

            var user = await usersRepository.GetBySteamIdAsync(steamId);

            if (user != null)
            {
                return;
            }

            //var steamFactory = context.HttpContext.RequestServices.GetRequiredService<SteamWebInterfaceFactory>();
            //var httpClientFactory = context.HttpContext.RequestServices.GetRequiredService<IHttpClientFactory>();
            //var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ValidationHelper>>();

            //var client = httpClientFactory.CreateClient();
            //client.Timeout = TimeSpan.FromSeconds(3);

            //PlayerSummaryModel playerSummary = null;

            //try
            //{
            //    var response = await steamFactory.CreateSteamWebInterface<SteamUser>(client).GetPlayerSummaryAsync(ulong.Parse(steamId));
            //    playerSummary = response.Data;
            //}
            //catch (Exception e)
            //{
            //    logger.LogError(e, "An exception occurated when downloading player summaries");
            //}

            user = new MUser()
            {
                SteamId = steamId,
            };

            //if (playerSummary != null)
            //{
            //    user.Name = playerSummary.Nickname;
            //}

            //if (playerSummary != null)
            //{
            //    try
            //    {
            //        byte[] avatarContent = await client.GetByteArrayAsync(playerSummary.AvatarFullUrl);
            //        MImage img = new MImage()
            //        {
            //            Name = "steam_avatar.jpg",
            //            Content = avatarContent,
            //            ContentType = "image/jpeg"
            //        };

            //        user.AvatarImageId = await imagesRepository.AddImageAsync(img);
            //    } catch (Exception e)
            //    {
            //        logger.LogError(e, $"An exception occurated when downloading player avatar {playerSummary.SteamId}");
            //    }                
            //}

             await usersRepository.AddAsync(user);
        }

        public static async Task Validate(CookieValidatePrincipalContext context)
        {
            string steamId = context.Principal.FindFirst(ClaimTypes.NameIdentifier).Value[SteamIdStartIndex..];

            var usersRepository = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();

            var user = await usersRepository.GetBySteamIdAsync(steamId);
            if (user != null)
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.SteamId),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                context.ReplacePrincipal(new ClaimsPrincipal(new ClaimsIdentity(claims, "Steam")));
            }
        }
    }
}
