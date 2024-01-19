using CourseManagerInterface.Presentation.Models;
using CourseManagerInterface.Presentation.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
            public List<IncomeProduct> IncomeProducts { get; set; } = new();
        }

    }
}
