using Steam.Models.SteamCommunity;
using SteamGamesWeAllCanPlayWASM.Data.Repositories;
using SteamGamesWeAllCanPlayWASM.Shared.Models;

namespace SteamGamesWeAllCanPlayWASM.Sharded.Repositories
{
    public interface IPlayerSummaryRepository: IGenericRepository<MPlayerSummary>
    {

        public MPlayerSummary GetByPlayerSummarySteamId(string steamId);

        public Task SaveChanges();
    }
}