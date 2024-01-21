using CourseManagerInterface.Presentation.Core;
using CourseManagerInterface.Presentation.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.Commands.List
{
    public class HandleIncomeRegistrationResultCommand : Command
    {
        private readonly IncomeRegisterViewModel _viewModel;

        public HandleIncomeRegistrationResultCommand(IncomeRegisterViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(string? args)
        {
            CommandData commandData = JsonSerializer.Deserialize<CommandData>(args ?? string.Empty) ?? new CommandData();
            _viewModel.SetSubmitResult(commandData.Success);
        }

        private class CommandData
        {
            public bool Success { get; set; }
        }
    }
}
