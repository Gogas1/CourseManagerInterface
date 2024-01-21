using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CourseManagerInterface.Presentation.Networking
{
    public class Client
    {
        private string _host;
        private int _port;

        private TcpClient _tcpClient;

        public Action<string>? OnLog;
        public Action<string>? OnReceive;

        public event Action? OnConnect;
        public Action? OnDisconnect;

        public Action? OnSent;

        public Client()
        {
            AssignHostAndPort("127.0.0.1", 8000);
            _tcpClient = new TcpClient();
        }

        public void AssignHostAndPort(string host, int port)
        {
            _host = host;
            _port = port;
        }

        public void Connect()
        {
            _tcpClient = new TcpClient();
            _tcpClient.BeginConnect(IPAddress.Parse(_host), _port, ConnectCallback, null);
        }

        private void ConnectCallback(IAsyncResult asyncResult)
        {
            try
            {
                _tcpClient.EndConnect(asyncResult);
                OnConnect?.Invoke();
                Listen();
            }
            catch (SocketException ex)
            {

            }
        }

        public void Listen()
        {
            if (_tcpClient.Connected)
            {
                var stream = _tcpClient.GetStream();

                if (!stream.CanWrite && !stream.CanRead) return;

                try
                {
                    byte[] buffer = new byte[4096];
                    stream.BeginRead(buffer, 0, buffer.Length, ReadCallback, buffer);
                }
                catch (IOException ex)
                {
                    Listen();
                }
            }
            else
            {
                Disconnect();
            }
        }

        private void ReadCallback(IAsyncResult asyncResult)
        {
            if (_tcpClient.Connected)
            {
                byte[] buffer = (byte[])asyncResult.AsyncState;

                var stream = _tcpClient.GetStream();

                try
                {
                    int bytesReaded = stream.EndRead(asyncResult);
                    if (bytesReaded > 0)
                    {
                        string data = Encoding.UTF8.GetString(buffer);
                        data = data.Trim('\0');
                        OnReceive?.Invoke(data);

                        if (_tcpClient.Connected)
                        {
                            Listen();
                        }
                    }
                }
                catch (IOException ex)
                {
                    stream.BeginRead(buffer, 0, buffer.Length, ReadCallback, buffer);
                }
            }
            else
            {
                Disconnect();
            }
        }

        public void SendMessage(string message)
        {
            if (_tcpClient.Connected)
            {
                var stream = _tcpClient.GetStream();

                try
                {
                    if (stream.CanWrite && _tcpClient.Connected)
                    {
                        byte[] data = Encoding.UTF8.GetBytes(message);
                        stream.BeginWrite(data, 0, data.Length, SendingCallback, null);
                    }
                }
                catch (IOException ex)
                {


                }

            }
            else
            {
                Disconnect();
            }

        }

        private void SendingCallback(IAsyncResult asyncResult)
        {
            if (_tcpClient.Connected)
            {
                var stream = _tcpClient.GetStream();

                try
                {
                    stream.EndWrite(asyncResult);
                    OnSent?.Invoke();
                }
                catch (IOException ex)
                {


                }

            }
            else
            {
                Disconnect();
            }
        }

        public void Disconnect()
        {
            OnDisconnect?.Invoke();

            if (_tcpClient.Client.Connected)
            {
                _tcpClient.Client.Disconnect(true);
            }

        }

        public void Close()
        {
            Disconnect();
            _tcpClient.Close();
        }
    }
}
