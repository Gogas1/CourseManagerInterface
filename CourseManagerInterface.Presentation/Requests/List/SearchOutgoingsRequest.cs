using CourseManagerInterface.Presentation.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.Requests.List
{
    public class SearchOutgoingsRequest : Request
    {
        private readonly ClientHost _clientHost;

        public SearchOutgoingsRequest(ClientHost clientHost)
        {
            _clientHost = clientHost;
        }

        public override void Execute(object? args)
        {
            var arguments = (SearchOutgoingsRequestArguments?)args;
            MasterMessage masterMessage = new MasterMessage
            {
                Command = "outgoings_search",
                CommandData = JsonSerializer.Serialize(arguments)
            };

            _clientHost.SendMessage(JsonSerializer.Serialize(masterMessage));
        }

        public class SearchOutgoingsRequestArguments
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }
    }
}
