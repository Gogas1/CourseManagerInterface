using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.Commands.List
{
    public class AddIncomeProductFoundCommand : Command
    {
        private readonly AddIncomeProductFoundCommandCallbackService _callbackService;

        public AddIncomeProductFoundCommand(AddIncomeProductFoundCommandCallbackService callbackService)
        {
            _callbackService = callbackService;
        }

        public override void Execute(string? args)
        {
            try
            {
                AddIncomeProductFoundCommandData? data = JsonSerializer.Deserialize<AddIncomeProductFoundCommandData>(args ?? string.Empty);

                if(data != null)
                {
                    _callbackService.Execute(data);
                }               
            }
            catch
            {

            }
            
        }
    }

    public class AddIncomeProductFoundCommandData
    {
        public bool ProductFound { get; set; }
        public List<ProductFound> FoundProducts { get; set; } = new();
    }

    public class ProductFound
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }

    public class AddIncomeProductFoundCommandCallbackService
    {
        public event Action<AddIncomeProductFoundCommandData>? OnExecuting;

        public void Execute(AddIncomeProductFoundCommandData data)
        {
            OnExecuting?.Invoke(data);
        }
    }
}
