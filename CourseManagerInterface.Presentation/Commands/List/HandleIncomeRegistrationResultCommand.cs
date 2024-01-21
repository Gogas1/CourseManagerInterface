using CourseManagerInterface.Presentation.MVVM.ViewModel;
using System.Text.Json;

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
