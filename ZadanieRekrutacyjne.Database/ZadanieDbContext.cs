using Microsoft.EntityFrameworkCore;
using System.IO;

namespace ZadanieRekrutacyjne.Database
{
    public class ZadanieDbContext : DbContext
    {
        public DbSet<Key> Keys { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite($"Filename={Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "ZadanieRekrutacyjne.sqlite")}");
        }
    }
}
