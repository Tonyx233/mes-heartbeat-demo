using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace HeartbeatDemo
{
    public class HeartbeatClient
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private Thread _heartbeatThread;

        private bool _running = false;

        public void Start(string ip, int port)
        {
            _client = new TcpClient();

            Console.WriteLine("[CLIENT] Connecting...");
            _client.Connect(ip, port);
            _stream = _client.GetStream();

            Console.WriteLine("[CLIENT] Connected.");

            _running = true;

            _heartbeatThread = new Thread(HeartbeatLoop);
            _heartbeatThread.Start();
        }

        private void HeartbeatLoop()
        {
            while (_running)
            {
                try
                {
                    string heartbeat = "HEARTBEAT";
                    byte[] data = Encoding.UTF8.GetBytes(heartbeat);

                    _stream.Write(data, 0, data.Length);
                    _stream.Flush();

                    Console.WriteLine("[CLIENT] Sent heartbeat.");

                    Thread.Sleep(2000); // Send by every 2 seconds
                }
                catch
                {
                    Console.WriteLine("[CLIENT] Heartbeat failed.");
                    _running = false;
                }
            }
        }

        public void Stop()
        {
            Console.WriteLine("[Client] Stopping client.");
            _running = false;
            _stream?.Close();
            _client?.Close();
            Console.WriteLine("[Client] Client stopped.");
        }
    }
}