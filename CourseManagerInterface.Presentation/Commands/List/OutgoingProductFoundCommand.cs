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
    public class OutgoingProductFoundCommand : Command
    {
        private readonly CreateOutgoingViewModel _viewModel;

        public OutgoingProductFoundCommand(CreateOutgoingViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(string? args)
        {
            CommandData? commandData = JsonSerializer.Deserialize<CommandData>(args ?? string.Empty) ?? new CommandData();

            _viewModel.AddProduct(commandData.Success, commandData.FoundProduct);
        }

        private class CommandData
        {
            public bool Success { get; set; }
            public Product? FoundProduct { get; set; }
        }
    }

    public class OutgoingProductFoundCommandCallbackService
    {
        public event Action<IEnumerable<Product>>? OnExecuting;

        public void Execute(IEnumerable<Product> products)
        {
            OnExecuting?.Invoke(products);
        }
    }
}
