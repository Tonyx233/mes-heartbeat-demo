using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HeartbeatDemo
{
    public class HeartbeatServer
    {
        private TcpListener _listener;
        private bool _running = false;

        public void Start(int port)
        {
            _listener = new TcpListener(IPAddress.Any, port);
            _listener.Start();
            _running = true;

            Console.WriteLine($"[SERVER] Heartbeat server started on port {port}");

            // accept client connections asynchronously
            _listener.BeginAcceptTcpClient(OnClientConnected, null);
        }

        private void OnClientConnected(IAsyncResult ar)
        {
            if (!_running) return;

            var client = _listener.EndAcceptTcpClient(ar);
            Console.WriteLine("[SERVER] Client connected.");

            // allow next client connection
            _listener.BeginAcceptTcpClient(OnClientConnected, null);

            byte[] buffer = new byte[1024];
            var stream = client.GetStream();

            while (client.Connected)
            {
                try
                {
                    int len = stream.Read(buffer, 0, buffer.Length);
                    if (len == 0) break;

                    string msg = Encoding.UTF8.GetString(buffer, 0, len);
                    Console.WriteLine($"[SERVER] Received: {msg}");
                }
                catch
                {
                    break;
                }
            }

            Console.WriteLine("[SERVER] Client disconnected.");
        }
        public void Stop()
        {
            Console.WriteLine("[SERVER] Stopping server...");

            _running = false;

            try
            {
                _listener?.Stop();
            }
            catch { }

            Console.WriteLine("[SERVER] Server stopped.");
        }
    }
}