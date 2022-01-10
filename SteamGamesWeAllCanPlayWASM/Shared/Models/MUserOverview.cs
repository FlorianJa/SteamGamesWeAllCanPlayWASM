using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamGamesWeAllCanPlayWASM.Shared.Models
{
    public class MUserOverview
    {
        public int FriendsCount { get; set; }
        public int FriendsOnline { get; set; }
        public uint GameCount { get; set; }

        public string AvatarFullURL { get; set; }
    }
}
