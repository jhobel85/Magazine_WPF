using System;
using System.Threading.Tasks;
using System.Windows;
using Magazine_WPF.Service;
using Magazine_WPF.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Magazine_WPF
{
    public partial class App : Application
    {
        private IHost? host;

        public App()
        {
            host = Host.CreateDefaultBuilder()
                .ConfigureServices(ConfigureServices)
                .Build();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IFindFreePlaceService, FindFreePlaceService>();
            services.AddTransient<MagazineViewModel>();
            services.AddTransient<MainWindow>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            if (host == null)
            {
                throw new InvalidOperationException("Host was not initialized.");
            }

            await host.StartAsync();

            var mainWindow = host.Services.GetRequiredService<MainWindow>();
            MainWindow = mainWindow;
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            if (host != null)
            {
                await host.StopAsync();
                host.Dispose();
            }

            base.OnExit(e);
        }
    }
}
