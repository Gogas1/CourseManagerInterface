using CourseManagerInterface.Presentation.Models;
using CourseManagerInterface.Presentation.MVVM.ViewModel;
using System.Text.Json;

namespace CourseManagerInterface.Presentation.Commands.List
{
    public class IncomeProductsFoundCommand : Command
    {
        private readonly IncomeManagementViewModel _incomeManagementViewModel;

        public IncomeProductsFoundCommand(IncomeManagementViewModel incomeManagementViewModel)
        {
            _incomeManagementViewModel = incomeManagementViewModel;
        }

        public override void Execute(string? args)
        {
            CommandData commandData = JsonSerializer.Deserialize<CommandData>(args ?? string.Empty) ?? new CommandData();
            _incomeManagementViewModel.ShowIncomeProducts(commandData.IncomeProducts);
        }

        private class CommandData
        {
            public List<Product> IncomeProducts { get; set; } = new();
        }

    }
}
