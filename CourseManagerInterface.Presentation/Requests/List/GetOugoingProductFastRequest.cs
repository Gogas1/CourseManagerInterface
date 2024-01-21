using CourseManagerInterface.Presentation.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.Requests.List
{
    public class GetOugoingProductFastRequest : Request
    {
        private readonly ClientHost _clientHost;

        public GetOugoingProductFastRequest(ClientHost clientHost)
        {
            _clientHost = clientHost;
        }

        public override void Execute(object? args)
        {
            int argument = (int)(args ?? 0);

            MasterMessage masterMessage = new MasterMessage
            {
                Command = "get_outgoingproduct_id",
                CommandData = JsonSerializer.Serialize(argument)
            };
            
            _clientHost.SendMessage(JsonSerializer.Serialize(masterMessage));
        }
    }
}
