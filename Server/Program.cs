using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    public static void SendClient(NetworkStream player1, string mesege)
    {
        byte[] data = Encoding.UTF8.GetBytes(mesege);
        player1.Write(data, 0, data.Length);
        Thread.Sleep(200);
    }
    public static void SendClient(NetworkStream player1, NetworkStream player2 , string mesege)
    {
        byte[] data = Encoding.UTF8.GetBytes(mesege);
        player1.Write(data, 0, data.Length);
        player2.Write(data, 0, data.Length);
        Thread.Sleep(200);
    }



    static void Main()
    {
        Random r = new Random();
        TcpListener server = new TcpListener(IPAddress.Any, 0);
        server.Start();
        Console.Clear();
        foreach (IPAddress ip in Dns.GetHostAddresses(Dns.GetHostName()))
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                Console.Write("Server ip: " + ip+":");
                break;
            }
        }
        Console.WriteLine(((IPEndPoint)server.LocalEndpoint).Port);

        TcpClient client1 = server.AcceptTcpClient();
        NetworkStream stream1 = client1.GetStream();

        SendClient(stream1, "Wait");


        TcpClient client2 = server.AcceptTcpClient();
        NetworkStream stream2 = client2.GetStream();


        SendClient(stream1,stream2, "S");
        Run run = new Run(stream1, stream2);


        run.RunGame();


        server.Stop();
    }
}
