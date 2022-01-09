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
        public int Id { get; set; }
        public DateTime LastUpdated { get; set; }

        public IList<MPlayerSummary> Friends { get; set; }
    }
}
