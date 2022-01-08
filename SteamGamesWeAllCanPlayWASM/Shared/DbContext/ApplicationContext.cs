using Microsoft.EntityFrameworkCore;
using SteamGamesWeAllCanPlayWASM.Shared.Models;

namespace SteamGamesWeAllCanPlayWASM.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<MUser> Users { get; set; }
        public string DbPath { get; private set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
        {
            //var folder = Environment.SpecialFolder.LocalApplicationData;
            //var path = Environment.GetFolderPath(folder);
            //DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}blogging.db";
        }

        // The following configures EF to create a Sqlite database file in the
        //// special "local" folder for your platform.
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseSqlite($"Data Source={DbPath}");
    }
}
