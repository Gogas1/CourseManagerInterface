using CourseManagerInterface.Presentation.Core;
using CourseManagerInterface.Presentation.MVVM.ViewModel;
using CourseManagerInterface.Presentation.Navigation;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.Configuration;
using CourseManagerInterface.Presentation.Networking;
using CourseManagerInterface.Presentation.Extensions;
using CourseManagerInterface.Presentation.Commands;
using AutoMapper;
using CourseManagerInterface.Presentation.Mapper;
using CourseManagerInterface.Presentation.Interfaces;

namespace CourseManagerInterface.Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly CommandHandler _commandHandler;
        private readonly NavigationControlService _navigationControlService;

        public App()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appconfig.json")
                .Build();

            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });

            services.AddSingleton<IConfiguration>(configuration);

            services.AddSingleton<ClientHost>(new ClientHost(500));

            services.AddViewModels();
            services.AddCommands();
            services.AddRequests();
            services.AddDialogueWindows();

            services.AddSingleton<IMapper>(AutoMapperConfigurer.GetConfuguredMapper());

            services.AddSingleton<NavigationService>();
            services.AddSingleton<NavigationControlService>();

            _serviceProvider = services.BuildServiceProvider();
            _commandHandler = _serviceProvider.GetRequiredService<CommandHandler>();
            _navigationControlService = _serviceProvider.GetRequiredService<NavigationControlService>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            var host = _serviceProvider.GetRequiredService<ClientHost>();
            host.CloseClient();

            base.OnExit(e);
        }
    }

}
