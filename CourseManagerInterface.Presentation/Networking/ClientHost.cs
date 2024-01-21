namespace CourseManagerInterface.Presentation.Networking
{
    public class ClientHost
    {
        private readonly Client Client;

        private readonly Queue<string> messages = new Queue<string>();

        public event Action<string>? OnLog;

        public event Action? OnConnect;
        public event Action? OnDisconnect;

        public event Action? OnSend;
        public event Action<string>? OnReceive;

        public ClientHost(int connectionAttemptDelayInMS)
        {
            Client = new Client();
            Client.OnConnect += InvokeOnConnect;
            Client.OnDisconnect += InvokeOnDisconnect;
            Client.OnReceive += InvokeOnReceive;
        }

        public void RunClient(string host, int port)
        {
            Client.AssignHostAndPort(host, port);
            Client.Connect();
            OnLog?.Invoke("Client started");
        }

        public void Disconnect()
        {
            Client.Disconnect();
        }

        public void CloseClient()
        {
            Client.Close();
        }

        public void SendMessage(string message)
        {
            Client.SendMessage(message);
        }

        private void InvokeOnConnect()
        {
            OnConnect?.Invoke();
        }

        private void InvokeOnDisconnect()
        {
            OnDisconnect?.Invoke();
        }

        private void InvokeOnReceive(string message)
        {
            OnReceive?.Invoke(message);
        }
    }
}
