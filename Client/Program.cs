using System.Net.Sockets;
using System.Text;
using Animation;
using Date;
using IO;

class Client
{
    
    
    static void Main()
    {   
        Screen screen = new Screen();
        Anima ani = new Anima(screen);
        TcpClient player = new TcpClient();
        

        System.Console.Write("Put the ip addres: ");
        string[] ip = Console.ReadLine().Split(':');

        player.Connect(ip[0], int.Parse(ip[1]));
        NetworkStream stream = player.GetStream();
        Run run = new Run(screen, stream, ani);

        byte[] Buf = new byte[1024];
        int Read = stream.Read(Buf, 0, Buf.Length);
        string mesege = Encoding.UTF8.GetString(Buf, 0, Read).Trim();
        if (mesege == "Wait")
        {
            bool flag = true;
            Task t1 = Task.Run(() =>
                {
                    while (flag){
                    screen.CleanConsole();
                    screen.ClearScreen();   
                    ani.Wait();
                    screen.Draw();
                    Thread.Sleep(200);
                }});


            Task t2 = Task.Run(() =>
                {
                    while(flag){
                        if (stream.DataAvailable){
                            Read = stream.Read(Buf, 0, Buf.Length);
                            mesege = Encoding.UTF8.GetString(Buf, 0, Read);
                            if  (mesege.Length == 1) 
                            {
                                flag = false;
                                break;
                            }
                        }  
                }});
            Task.WaitAll(t1, t2);
        }
        run.RunGame();
        
    

    }

}
