using CourseManagerInterface.Presentation.Requests;
using CourseManagerInterface.Presentation.Requests.List;
using Microsoft.Extensions.DependencyInjection;

namespace CourseManagerInterface.Presentation.Extensions
{
    public static class RegisterRequestExtensions
    {
        public static void AddRequests(this IServiceCollection services)
        {
            services.AddScoped<AddIncomeRequest>();
            services.AddScoped<AddIncomeSearchProductRequest>();
            services.AddScoped<IncomesSearchRequest>();
            services.AddScoped<GetIncomeProductsRequest>();
            services.AddScoped<SearchProductsRequest>();
            services.AddScoped<GetOugoingProductFastRequest>();
            services.AddScoped<SubmitOutgoingRequest>();
            services.AddScoped<SearchProductsOutgoingRequest>();
            services.AddScoped<SaveProductFeaturesRequest>();
            services.AddScoped<SearchOutgoingsRequest>();

            services.AddSingleton<RequestsService>();

            services.AddSingleton<Func<Type, Request>>(serviceProvider => requestType => (Request)serviceProvider.GetRequiredService(requestType));
        }
    }
}
