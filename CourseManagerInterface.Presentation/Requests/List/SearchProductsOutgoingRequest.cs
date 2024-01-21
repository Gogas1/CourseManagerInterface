using CourseManagerInterface.Presentation.Networking;
using System.Text.Json;

namespace CourseManagerInterface.Presentation.Requests.List
{
    public class SearchProductsOutgoingRequest : Request
    {
        private readonly ClientHost _clientHost;

        public SearchProductsOutgoingRequest(ClientHost clientHost)
        {
            _clientHost = clientHost;
        }

        public override void Execute(object? args)
        {
            MasterMessage masterMessage = new MasterMessage
            {
                Command = "search_products_outgoing",
                CommandData = JsonSerializer.Serialize((SearchProductsOutgoingRequestArguments)(args ?? string.Empty))
            };

            _clientHost.SendMessage(JsonSerializer.Serialize(masterMessage));
        }
    }

    public class SearchProductsOutgoingRequestArguments
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
