using SteamGamesWeAllCanPlayWASM.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamGamesWeAllCanPlayWASM.Data.Repositories
{
    public class UserRepository: GenericRepository<MUser>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
            
        }
        public async Task<MUser> GetBySteamIdAsync(string steamId)
        {
            return (await FindAsync(u => u.SteamId == steamId))?.FirstOrDefault();
        }
    }
}
