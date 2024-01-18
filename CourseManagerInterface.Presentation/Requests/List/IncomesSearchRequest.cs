using CourseManagerInterface.Presentation.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.Requests.List
{
    public class IncomesSearchRequest : Request
    {
        private readonly ClientHost _clientHost;

        public IncomesSearchRequest(ClientHost clientHost)
        {
            _clientHost = clientHost;
        }

        public override void Execute(object? args)
        {
            var arguments = (IncomesSearchRequestArguments?)args;

            MasterMessage masterMessage = new MasterMessage
            {
                Command = "incomes_search",
                CommandData = JsonSerializer.Serialize(arguments)
            };

            _clientHost.SendMessage(JsonSerializer.Serialize(masterMessage));
        }
    }

    public class IncomesSearchRequestArguments
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
