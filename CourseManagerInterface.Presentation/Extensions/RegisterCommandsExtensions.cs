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
            services.AddKeyedTransient<Command, ManagementProductsFoundCommand>("products_found");
            services.AddKeyedTransient<Command, OutgoingProductFoundCommand>("outgoing_product_search_result");
            services.AddKeyedTransient<Command, OutgoingProductsFoundCommand>("outgoing_products_search_result");
            services.AddKeyedTransient<Command, HandleOutgoingSubmitResultCommand>("outgoing_submit_result");
            services.AddKeyedTransient<Command, HandleIncomeRegistrationResultCommand>("addincome_command_result");

            services.AddSingleton<AddIncomeProductFoundCommandCallbackService>();
            services.AddSingleton<OutgoingProductsFoundCommandCallbackService>();

            services.AddSingleton<CommandHandler>();

            services.AddSingleton<Func<string, Command?>>(serviceProvider => key => serviceProvider.GetKeyedService<Command>(key));
        }
    }
}
