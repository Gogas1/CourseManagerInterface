using CourseManagerInterface.Presentation.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.Requests.List
{
    public class SearchProductsRequest : Request
    {
        private readonly ClientHost _clientHost;

        public SearchProductsRequest(ClientHost clientHost)
        {
            _clientHost = clientHost;
        }

        public override void Execute(object? args)
        {
            MasterMessage masterMessage = new MasterMessage
            {
                Command = "search_products",
                CommandData = JsonSerializer.Serialize((SearchProductsRequestArguments?)args)
            };

            _clientHost.SendMessage(JsonSerializer.Serialize(masterMessage));
        }
    }

    public class SearchProductsRequestArguments
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
