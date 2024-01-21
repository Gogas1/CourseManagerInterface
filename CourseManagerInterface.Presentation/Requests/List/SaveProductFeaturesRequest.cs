using CourseManagerInterface.Presentation.Networking;
using System.Text.Json;

namespace CourseManagerInterface.Presentation.Requests.List
{
    public class SaveProductFeaturesRequest : Request
    {
        private readonly ClientHost _clientHost;

        public SaveProductFeaturesRequest(ClientHost clientHost)
        {
            _clientHost = clientHost;
        }

        public override void Execute(object? args)
        {
            MasterMessage masterMessage = new MasterMessage()
            {
                Command = "update_product_features",
                CommandData = JsonSerializer.Serialize((SaveProductFeatureRequestArgs)(args ?? string.Empty))
            };

            _clientHost.SendMessage(JsonSerializer.Serialize(masterMessage));
        }
    }

    public class SaveProductFeatureRequestArgs
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
