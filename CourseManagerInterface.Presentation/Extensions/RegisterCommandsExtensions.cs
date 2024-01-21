using CourseManagerInterface.Presentation.Commands;
using CourseManagerInterface.Presentation.Commands.List;
using Microsoft.Extensions.DependencyInjection;

namespace CourseManagerInterface.Presentation.Extensions
{
    public static class RegisterCommandsExtensions
    {
        public static void AddCommands(this IServiceCollection services)
        {
            services.AddKeyedTransient<Command, AddIncomeProductFoundCommand>("addincome_productfound");
            services.AddKeyedTransient<Command, IncomesFoundCommand>("incomes_found");
            services.AddKeyedTransient<Command, IncomeProductsFoundCommand>("income_products_found");
            services.AddKeyedTransient<Command, ManagementProductsFoundCommand>("products_found");
            services.AddKeyedTransient<Command, OutgoingProductFoundCommand>("outgoing_product_search_result");
            services.AddKeyedTransient<Command, OutgoingProductsFoundCommand>("outgoing_products_search_result");
            services.AddKeyedTransient<Command, HandleOutgoingSubmitResultCommand>("outgoing_submit_result");
            services.AddKeyedTransient<Command, HandleIncomeRegistrationResultCommand>("addincome_command_result");
            services.AddKeyedTransient<Command, OutgoingsFoundCommand>("outgoings_found");

            services.AddSingleton<AddIncomeProductFoundCommandCallbackService>();
            services.AddSingleton<OutgoingProductsFoundCommandCallbackService>();

            services.AddSingleton<CommandHandler>();

            services.AddSingleton<Func<string, Command?>>(serviceProvider => key => serviceProvider.GetKeyedService<Command>(key));
        }
    }
}
