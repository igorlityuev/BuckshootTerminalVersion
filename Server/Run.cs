using System.Collections.Concurrent;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;

struct Players
{
    public int hp;
    public string name;
    public NetworkStream ipserver;
}
class Run
{
    Players player1;
    Players player2;
    Random r;
    int bulet =0;
    List<bool> clip = new List<bool>();
    public Run(NetworkStream stream1,NetworkStream stream2)
    {
        player1.hp =3;
        player2.hp =3;

        player1.ipserver = stream1;
        player2.ipserver = stream2;

        player1.name = "player1";
        player2.name = "player2";
        r = new Random();
    }

    public void SendClient(NetworkStream player1, NetworkStream player2 , string mesege)
    {
        byte[] data = Encoding.UTF8.GetBytes(mesege);
        player1.Write(data, 0, data.Length);
        player2.Write(data, 0, data.Length);
        Thread.Sleep(200);
    }

    public string GetClient(NetworkStream player1)
    {
        byte[] data = new byte[1024];
        int bytesRead = player1.Read(data, 0, data.Length);
        return Encoding.UTF8.GetString(data, 0, bytesRead);
    }

    public void SendClient(NetworkStream player1 , string mesege)
    {
        byte[] data = Encoding.UTF8.GetBytes(mesege);
        player1.Write(data, 0, data.Length);
        Thread.Sleep(200);
    }

    public void ShootClient(ref Players players)
    {
        
        if (clip.Last()){
            players.hp--;
            System.Console.WriteLine("SHOOTT");
            }

        System.Console.WriteLine("player1 hp: "+player1.hp+" player2 hp: "+player2.hp);
        clip.RemoveAt(clip.Count - 1);
        bulet--;
    }
    public string Reload()
    {
        clip.Clear();
        bulet = r.Next(2,7);  
        int BlueBulet = r.Next(0,bulet);
        int RedBulet = bulet - BlueBulet;

        for (int i =0; i < RedBulet; i++)
        {
            clip.Add(true);
        }
        

        for (int i =0; i < BlueBulet; i++)
        {
            clip.Add(false);
        }


        for (int i = clip.Count - 1; i > 0; i--)
        {

            int j = r.Next(i + 1);
            (clip[i], clip[j]) = (clip[j], clip[i]);
        }
        foreach (var item in clip)
        {
            System.Console.Write(item + " ");
        }
        return RedBulet.ToString()+BlueBulet.ToString();
    }


    public void RunGame()
    {
        int turn = 0;

        while (true)
        {

            if (player1.hp <= 0)
            {
                SendClient(player2.ipserver, "Win"); 
                SendClient(player1.ipserver, "UnWin"); 
                break;
            }
            else if (player2.hp <= 0)
            {
                SendClient(player1.ipserver, "Win"); 
                SendClient(player2.ipserver, "UnWin"); 
                break;
            }
            else if (bulet == 0)
            {               
                
                Thread.Sleep(200);
                SendClient(player1.ipserver, player2.ipserver, "Reload");

                string a = Reload();
                SendClient(player1.ipserver, player2.ipserver, a);
                System.Console.WriteLine("REloaded: "+ a);
            
            }else if (turn == 0)
            {
                SendClient(player1.ipserver, "YouTurn");
                SendClient(player2.ipserver, "EnemyTurn");
                if (GetClient(player1.ipserver) == "0")
                {
                    if (clip.Last())
                        turn++;
                    ShootClient(ref player1);

                    SendClient(player1.ipserver,player2.ipserver, "Hp");
                    SendClient(player1.ipserver, player1.hp+":"+player2.hp);
                    SendClient(player2.ipserver, player2.hp + ":"+player1.hp);

                }
                else
                {
                    ShootClient(ref player2);
                    SendClient(player1.ipserver,player2.ipserver, "Hp");
                    SendClient(player1.ipserver, player1.hp+":"+player2.hp);
                    SendClient(player2.ipserver, player2.hp + ":"+player1.hp);
                    turn++;
                }
            }else if (turn == 1)
            {
                SendClient(player1.ipserver, "EnemyTurn");
                SendClient(player2.ipserver, "YouTurn");
                if (GetClient(player2.ipserver) == "0")
                {
                    if (clip.Last())
                        turn--;
                    ShootClient(ref player2);
                    SendClient(player1.ipserver,player2.ipserver, "Hp");
                    SendClient(player1.ipserver, player1.hp+":"+player2.hp);
                    SendClient(player2.ipserver, player2.hp + ":"+player1.hp);
                }
                else
                {
                    ShootClient(ref player1);
                    SendClient(player1.ipserver,player2.ipserver, "Hp");
                    SendClient(player1.ipserver, player1.hp+":"+player2.hp);
                    SendClient(player2.ipserver, player2.hp + ":"+player1.hp);
                    turn--;
                }

            }
        }

        player1.ipserver.Close();
        player2.ipserver.Close();
    }
}