using System;

namespace HeartbeatDemo
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Select mode: 1 = Server, 2 = Client");
            string mode = Console.ReadLine();

            if (mode == "1")
            {
                var server = new HeartbeatServer();
                server.Start(9999);

                Console.WriteLine("Press ENTER to stop server...");
                Console.ReadLine();
                server.Stop();
            }
            else if (mode == "2")
            {
                var client = new HeartbeatClient();
                client.Start("127.0.0.1", 9999);

                Console.WriteLine("Press ENTER to stop client...");
                Console.ReadLine();
                client.Stop();
            }
            else
            {
                Console.WriteLine("Invalid mode selected.");
            }
        }
    }
}