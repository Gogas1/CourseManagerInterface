using CourseManagerInterface.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.Commands.List
{
    public class OutgoingProductsFoundCommand : Command
    {
        private readonly OutgoingProductsFoundCommandCallbackService _callbackService;

        public OutgoingProductsFoundCommand(OutgoingProductsFoundCommandCallbackService callbackService)
        {
            _callbackService = callbackService;
        }

        public override void Execute(string? args)
        {
            CommandData data = JsonSerializer.Deserialize<CommandData>(args ?? string.Empty) ?? new CommandData();

            _callbackService.Execute(data.FoundProducts);
        }

        private class CommandData
        {
            public List<Product> FoundProducts { get; set; } = new();
        }
    }

    public class OutgoingProductsFoundCommandCallbackService
    {
        public event Action<IEnumerable<Product>>? OnExecuting;

        public void Execute(IEnumerable<Product> products)
        {
            OnExecuting?.Invoke(products);
        }
    }
}
