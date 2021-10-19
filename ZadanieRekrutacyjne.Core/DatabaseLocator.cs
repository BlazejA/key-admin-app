using ZadanieRekrutacyjne.Database;

namespace ZadanieRekrutacyjne.Core
{
    public class DatabaseLocator
    {
        public static ZadanieDbContext Database { get; set; }
    }
}
