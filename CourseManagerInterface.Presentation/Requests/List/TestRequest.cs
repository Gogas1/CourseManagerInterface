using CourseManagerInterface.Presentation.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CourseManagerInterface.Presentation.Requests.List
{
    public class TestRequest : Request
    {
        private readonly ClientHost _clientHost;

        public TestRequest(ClientHost clientHost)
        {
            _clientHost = clientHost;
        }

        public override void Execute(object? args)
        {
            MasterMessage message = new MasterMessage()
            {
                Command = "test",
                CommandData = JsonSerializer.Serialize((TestRequestArguments)args)
            };

            _clientHost.SendMessage(JsonSerializer.Serialize(message));
        }
    }

    public class TestRequestArguments()
    {
        public int SomeInt { get; set; }
        public string SomeString { get; set; } = string.Empty;
    }
}
