using CourseManagerInterface.Presentation.Models;
using CourseManagerInterface.Presentation.MVVM.ViewModel;
using System.Text.Json;

namespace CourseManagerInterface.Presentation.Commands.List
{
    public class ManagementProductsFoundCommand : Command
    {
        private readonly ProductManagementViewModel _productManagementViewModel;

        public ManagementProductsFoundCommand(ProductManagementViewModel productManagementViewModel)
        {
            _productManagementViewModel = productManagementViewModel;
        }

        public override void Execute(string? args)
        {
            CommandData commandData = JsonSerializer.Deserialize<CommandData>(args ?? string.Empty) ?? new CommandData();

            _productManagementViewModel.ShowFoundProducts(commandData.FoundProducts);
        }

        private class CommandData
        {
            public List<Product> FoundProducts { get; set; } = new();
        }
    }
}
