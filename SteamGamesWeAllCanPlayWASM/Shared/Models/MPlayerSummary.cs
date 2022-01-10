using Steam.Models.SteamCommunity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamGamesWeAllCanPlayWASM.Shared.Models
{
    public class MPlayerSummary: PlayerSummaryModel
    {
        public MPlayerSummary()
        {

        }

        public MPlayerSummary(PlayerSummaryModel data)
        {
            this.AccountCreatedDate = data.AccountCreatedDate;
            this.AvatarFullUrl = data.AvatarFullUrl;
            this.AvatarUrl = data.AvatarUrl;
            this.AvatarMediumUrl = data.AvatarMediumUrl;
            this.CityCode = data.CityCode;
            this.CommentPermission = data.CommentPermission;
            this.CountryCode = data.CountryCode;
            this.LastLoggedOffDate = data.LastLoggedOffDate;
            this.Nickname = data.Nickname;
            this.PlayingGameId = data.PlayingGameId;
            this.PlayingGameName = data.PlayingGameName;
            this.PlayingGameServerIP = data.PlayingGameServerIP;
            this.PrimaryGroupId = data.PrimaryGroupId;
            this.ProfileState = data.ProfileState;
            this.ProfileUrl = data.ProfileUrl;
            this.ProfileVisibility = data.ProfileVisibility;
            this.RealName = data.RealName;
            this.StateCode = data.StateCode;
            this.SteamId = data.SteamId;
            this.UserStatus = data.UserStatus;
            this.LastUpdated = DateTime.UtcNow;
            this.Friends = null;
        }

        public int Id { get; set; }
        public DateTime LastUpdated { get; set; }

        public IList<MPlayerSummary> Friends { get; set; }

    }
}
