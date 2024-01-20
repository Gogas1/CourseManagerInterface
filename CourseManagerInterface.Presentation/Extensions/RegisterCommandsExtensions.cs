using CourseManagerInterface.Presentation.Commands;
using CourseManagerInterface.Presentation.Commands.List;
using CourseManagerInterface.Presentation.Core;
using CourseManagerInterface.Presentation.Requests;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.Extensions
{
    public static class RegisterCommandsExtensions
    {
        public static void AddCommands(this IServiceCollection services)
        {
            services.AddKeyedTransient<Command, TestCommand>("testanswer");
            services.AddKeyedTransient<Command, AddIncomeProductFoundCommand>("addincome_productfound");
            services.AddKeyedTransient<Command, IncomesFoundCommand>("incomes_found");
            services.AddKeyedTransient<Command, IncomeProductsFoundCommand>("income_products_found");
            services.AddKeyedTransient<Command, ProductsFoundCommand>("products_found");

            services.AddSingleton<AddIncomeProductFoundCommandCallbackService>();

            services.AddSingleton<CommandHandler>();

            services.AddSingleton<Func<string, Command?>>(serviceProvider => key => serviceProvider.GetKeyedService<Command>(key));
        }
    }
}
