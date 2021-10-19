using System.Windows;
using ZadanieRekrutacyjne.Core;
using ZadanieRekrutacyjne.Database;

namespace ZadanieRekrutacyjne
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var database = new ZadanieDbContext();

            database.Database.EnsureCreated();

            DatabaseLocator.Database = database;
        }
    }
}
