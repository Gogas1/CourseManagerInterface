using CourseManagerInterface.Presentation.Requests;
using CourseManagerInterface.Presentation.Requests.List;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.Extensions
{
    public static class RegisterRequestExtensions
    {
        public static void AddRequests(this IServiceCollection services)
        {
            services.AddScoped<TestRequest>();
            services.AddScoped<AddIncomeRequest>();
            services.AddScoped<AddIncomeSearchProductRequest>();

            services.AddSingleton<RequestsService>();

            services.AddSingleton<Func<Type, Request>>(serviceProvider => requestType => (Request)serviceProvider.GetRequiredService(requestType));
        }
    }
}
