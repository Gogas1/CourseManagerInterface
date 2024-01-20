using CourseManagerInterface.Presentation.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.Requests.List
{
    public class AddIncomeRequest : Request
    {
        private readonly ClientHost _clientHost;

        public AddIncomeRequest(ClientHost clientHost)
        {
            _clientHost = clientHost;
        }

        public override void Execute(object? args)
        {
            MasterMessage message = new MasterMessage()
            {
                Command = "create_income",
                CommandData = JsonSerializer.Serialize((AddIncomeRequestArguments?)args)
            };

            _clientHost.SendMessage(JsonSerializer.Serialize(message));
        }
    }

    public class AddIncomeRequestArguments
    {
        public DateTime DateTime { get; set; }
        public string Supplier { get; set; } = string.Empty;
        public List<RequestIncomeProduct> Products { get; set; } = new();
    }

    public class RequestIncomeProduct
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Count { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}
