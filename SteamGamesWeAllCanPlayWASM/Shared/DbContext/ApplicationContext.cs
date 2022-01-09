using Microsoft.EntityFrameworkCore;
using SteamGamesWeAllCanPlayWASM.Shared.Models;

namespace SteamGamesWeAllCanPlayWASM.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<MUser> Users { get; set; }
        public DbSet<MPlayerSummary> PlayerSummaries { get; set; }


        public string DbPath { get; private set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
