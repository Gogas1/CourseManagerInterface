using CourseManagerInterface.Presentation.Networking;
using System.Text.Json;

namespace CourseManagerInterface.Presentation.Commands
{
    public class CommandHandler
    {
        private readonly Func<string, Command?> _commandFactory;
        private readonly ClientHost _clientHost;

        public CommandHandler(Func<string, Command?> commandFactory, ClientHost clientHost)
        {
            _commandFactory = commandFactory;
            _clientHost = clientHost;
            _clientHost.OnReceive += HandleMessage;
        }

        public void HandleMessage(string message)
        {
            try
            {
                MasterMessage? masterMessage = JsonSerializer.Deserialize<MasterMessage>(message ?? string.Empty);
                if (masterMessage == null) return;

                var targetCommand = _commandFactory.Invoke(masterMessage.Command);
                targetCommand?.Execute(masterMessage.CommandData);
            }
            catch
            {

            }
        }
    }
}
