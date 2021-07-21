using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using PasswordManager.IoC;
using PasswordManager.Views.Windows;

namespace PasswordManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var startup = new Startup();
            var collection = new ServiceCollection();
            startup.ConfigureServices(collection);

            collection.AddSingleton<MainWindow>();
            collection.AddSingleton<AddEditWindow>();
                
            var provider = collection.BuildServiceProvider();
            var mainWindow = provider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
