using CourseManagerInterface.Presentation.Core;
using CourseManagerInterface.Presentation.MVVM.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            services.AddSingleton<TestViewModel>();
            services.AddSingleton<IncomeRegisterViewModel>();
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<IncomeManagementViewModel>();
            services.AddSingleton<ProductManagementViewModel>();
            services.AddSingleton<CreateOutgoingViewModel>();

            services.AddSingleton<Func<Type, ViewModel>>(serviceProvider => viewModelType => (ViewModel)serviceProvider.GetRequiredService(viewModelType));
        }
    }
}
