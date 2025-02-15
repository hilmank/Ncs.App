using Microsoft.Extensions.DependencyInjection;
using Ncs.Wpf.Services;
using Ncs.Wpf.Services.Interfaces;
using Ncs.Wpf.ViewModels;
using Ncs.Wpf.Views;
using System.Windows;

namespace Ncs.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            // Register Services
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IAdminService, AdminService>();
            services.AddSingleton<ICustomerService, CustomerService>();

            // Register ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<AdminViewModel>();
            services.AddSingleton<CustomerViewModel>();

            // Register Views
            services.AddSingleton<MainWindow>();
            services.AddSingleton<AdminWindow>();
            services.AddSingleton<CustomerWindow>();

            ServiceProvider = services.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }

}
