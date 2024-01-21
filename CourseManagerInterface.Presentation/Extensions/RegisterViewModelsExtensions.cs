using CourseManagerInterface.Presentation.Core;
using CourseManagerInterface.Presentation.MVVM.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace CourseManagerInterface.Presentation.Extensions
{
    public static class RegisterViewModelsExtensions
    {
        public static void AddViewModels(this IServiceCollection services)
        {
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MenuViewModel>();

            services.AddSingleton<ConnectViewModel>();
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<IncomeRegisterViewModel>();
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<IncomeManagementViewModel>();
            services.AddSingleton<ProductManagementViewModel>();
            services.AddSingleton<CreateOutgoingViewModel>();
            services.AddSingleton<OutgoingManagementViewModel>();

            services.AddSingleton<Func<Type, ViewModel>>(serviceProvider => viewModelType => (ViewModel)serviceProvider.GetRequiredService(viewModelType));
        }
    }
}
