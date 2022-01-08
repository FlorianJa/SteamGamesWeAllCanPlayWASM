using SteamGamesWeAllCanPlayWASM.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamGamesWeAllCanPlayWASM.Data.Repositories
{
    public interface IUserRepository :IGenericRepository<MUser>
    {
        Task<MUser> GetBySteamIdAsync(string steamId);
    }

}
