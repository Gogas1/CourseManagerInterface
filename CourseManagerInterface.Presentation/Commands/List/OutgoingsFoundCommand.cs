using CourseManagerInterface.Presentation.Models;
using CourseManagerInterface.Presentation.MVVM.ViewModel;
using System.Text.Json;

namespace CourseManagerInterface.Presentation.Commands.List
{
    public class OutgoingsFoundCommand : Command
    {
        private readonly OutgoingManagementViewModel _viewModel;

        public OutgoingsFoundCommand(OutgoingManagementViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(string? args)
        {
            CommandData data = JsonSerializer.Deserialize<CommandData>(args ?? string.Empty) ?? new CommandData();
            _viewModel.ShowSearchResult(data.OutgoingFound);
        }

        private class CommandData
        {
            public List<Outgoing> OutgoingFound { get; set; } = new();
        }
    }
}
