using CourseManagerInterface.Presentation.MVVM.ViewModel;
using System.Text.Json;

namespace CourseManagerInterface.Presentation.Commands.List
{
    public class HandleOutgoingSubmitResultCommand : Command
    {
        private readonly CreateOutgoingViewModel viewModel;

        public HandleOutgoingSubmitResultCommand(CreateOutgoingViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(string? args)
        {
            CommandData commandData = JsonSerializer.Deserialize<CommandData>(args ?? string.Empty) ?? new CommandData();
            viewModel.SetSubmitResult(commandData.Success);
        }

        private class CommandData
        {
            public bool Success { get; set; }
        }
    }
}
