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
    public class ProductsFoundCommand : Command
    {
        private readonly ProductManagementViewModel _productManagementViewModel;

        public ProductsFoundCommand(ProductManagementViewModel productManagementViewModel)
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
