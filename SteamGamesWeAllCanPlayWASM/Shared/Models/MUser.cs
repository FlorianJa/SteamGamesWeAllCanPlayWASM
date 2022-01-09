using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SteamGamesWeAllCanPlayWASM.Shared.Models
{
    public class MUser
    {
        public int Id { get; set; }
        public string SteamId { get; set; }
        public DateTime CreateDate { get; set; }
                public string Name { get; set; }
        public string AvatarFullURL { get; set; }


        [JsonIgnore]
        public string SteamProfileUrl => "https://steamcommunity.com/profiles/" + SteamId;


        public MUser()
        {
            CreateDate = DateTime.Now;
        }

        public static MUser FromUser(MUser user)
        {
            return new MUser()
            {
                Id = user.Id,
                SteamId = user.SteamId,
                CreateDate = user.CreateDate
            };
        }
    }
}
