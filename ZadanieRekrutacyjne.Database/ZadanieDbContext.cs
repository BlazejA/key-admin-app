using Microsoft.EntityFrameworkCore;
using System.IO;

namespace ZadanieRekrutacyjne.Database
{
    public class ZadanieDbContext : DbContext
    {
        public DbSet<Key> Keys { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeKey> EmployeeKeys { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite($"Filename={Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "ZadanieRekrutacyjne.sqlite")}");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<EmployeeKey>()
                .HasKey(x => new { x.EmployeeId, x.KeyId });

        }
    }
}
