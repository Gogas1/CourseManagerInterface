using CourseManagerInterface.Presentation.Dialogues;
using CourseManagerInterface.Presentation.MVVM.View.Dialogue;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseManagerInterface.Presentation.Extensions
{
    public static class RegisterDialogueWindowsExtensions
    {
        public static void AddDialogueWindows(this IServiceCollection services)
        {
            services.AddTransient<AddIncomeProductDialog>();
            services.AddTransient<AddOutgoingProductShortDialog>();
            
            services.AddSingleton<DialogueService>();            
            services.AddSingleton<Func<Type, Window>>(serviceProvider => dialogWindowType => (Window)serviceProvider.GetRequiredService(dialogWindowType));
        }
    }
}
