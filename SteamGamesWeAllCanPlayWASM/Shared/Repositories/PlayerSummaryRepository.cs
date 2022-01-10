using Microsoft.EntityFrameworkCore;
using Steam.Models.SteamCommunity;
using SteamGamesWeAllCanPlayWASM.Data;
using SteamGamesWeAllCanPlayWASM.Data.Repositories;
using SteamGamesWeAllCanPlayWASM.Sharded.Repositories;
using SteamGamesWeAllCanPlayWASM.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamGamesWeAllCanPlayWASM.Shared.Repositories
{
    public class PlayerSummaryRepository : GenericRepository<MPlayerSummary>, IPlayerSummaryRepository
    {
        public PlayerSummaryRepository(ApplicationContext context) : base(context)
        {
        }

        public MPlayerSummary GetByPlayerSummarySteamId(string steamId)
        {
            return _context.Set<MPlayerSummary>().Where(p => p.SteamId == ulong.Parse(steamId)).Include(p => p.Friends)?.FirstOrDefault();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
