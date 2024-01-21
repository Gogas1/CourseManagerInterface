using CourseManagerInterface.Presentation.Models;
using CourseManagerInterface.Presentation.Networking;
using System.Text.Json;

namespace CourseManagerInterface.Presentation.Requests.List
{
    public class SubmitOutgoingRequest : Request
    {
        private readonly ClientHost _clientHost;

        public SubmitOutgoingRequest(ClientHost clientHost)
        {
            _clientHost = clientHost;
        }

        public override void Execute(object? args)
        {
            MasterMessage masterMessage = new MasterMessage
            {
                Command = "submit_outgoing",
                CommandData = JsonSerializer.Serialize((SubmitOutgoingRequestArguments)(args ?? string.Empty))
            };

            _clientHost.SendMessage(JsonSerializer.Serialize(masterMessage));
        }
    }
    public class SubmitOutgoingRequestArguments
    {
        public string Manager { get; set; } = string.Empty;
        public List<Product> Products { get; set; } = new();
    }
}
