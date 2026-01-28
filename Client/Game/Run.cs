using System.Net.Sockets;
using Animation;
using Date;
using IO;

class Run
{
    Screen screen;

    StreamReader reader;
    StreamWriter writer;

    string hp, hpEn;
    Anima ani;
    public Run(Screen screen, NetworkStream stream, Anima ani)
    {
        this.screen = screen;
        this.reader = new StreamReader(stream);
        this.writer = new StreamWriter(stream);
        this.ani = ani;
        hp="3";
        hpEn="3";
    }


    public string GetCommand()
    {
        char[] Buf = new char[1024];
        int Read = reader.Read(Buf, 0, Buf.Length);
        return new string(Buf, 0, Read).Trim();
    }


    public void Send(string m)
    {
        writer.Write(m);
        writer.Flush();

    }

    public bool flag = true;
    public void RunGame()
    {
        screen.CleanConsole();
        screen.ClearScreen();
        ani.DrawAsset(Assets.Board,0,0);



        string Command; 
        string bulet;
        while (flag)
        {
            Command = GetCommand();
            screen.CleanConsole();
            screen.ClearScreen();
            DrawBoard();
            switch (Command)
            {
                case "YouTurn":
                    YouTurn();
                    break;
                case "Reload":
                    bulet = GetCommand();
                    Reload(bulet);
                    break;
                case "EnemyTurn":
                    DrawBoard();
                    screen.Draw();
                    break;
                case "Win":
                    screen.ClearScreen();
                    ani.Box(" YOU WIN ", 10,35);
                    screen.Draw();
                    flag = false;
                    break;
                case "UnWin":
                    screen.ClearScreen();
                    ani.Box(" YOU LOSER ", 10,35);
                    screen.Draw();
                    flag = false;
                    break;
                case "Hp":
                    string[] h = GetCommand().Split(':');
                    hp = h[0];
                    hpEn = h[1];
                    break;
            }
            screen.Draw();
        }
    }
    public void DrawBoard()
    {
        ani.DrawAsset(Assets.Board,0,0);

        for (int i =0; i<int.Parse(hpEn);i++)
        {
            screen.SetPixel(45+i,15,'\u2665', ConsoleColor.Red);
        }
        for (int i =0; i<int.Parse(hp);i++)
        {
            screen.SetPixel(45+i,22,'\u2665', ConsoleColor.Green);
        }
    }

    public void YouTurn()
    {
        int select=0;
        bool flag = true;
        do
        {
            screen.CleanConsole();
            screen.ClearScreen();
            if (select == 0)
            {
                DrawBoard();
                ani.Box("Shoot Enemy", 5,20,ConsoleColor.Yellow);
                ani.Box(" Shoot Me  ", 36,20,ConsoleColor.Green);

            }else if(select == 1)
            {
                DrawBoard();
                ani.Box("Shoot Enemy", 5,20,ConsoleColor.Green);
                ani.Box(" Shoot Me  ", 36,20,ConsoleColor.Yellow);
            }
            screen.Draw();
            ConsoleKey key = new ConsoleKey();
            key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    if (select<1) select++;
                    break;
                case ConsoleKey.S:                
                case ConsoleKey.DownArrow:
                    if (select>0)select--;
                    break;
                case ConsoleKey.Enter:
                    Send(select.ToString());
                    flag=false;
                    break;
                                    
            }
        }
        while (flag);


    }
    public void Reload(string a)
    {
        int space = 39;
        int redBul= int.Parse(a[0].ToString());
        int blueBul= int.Parse(a[1].ToString());
        for (int i = 0;i<redBul;i++)
        {
            screen.SetPixel(space,19,'\u2588', ConsoleColor.DarkRed);
            space+=2;
            screen.CleanConsole();
            screen.Draw();
            Thread.Sleep(1000);
        }
        for (int i = 0;i<blueBul;i++)
        {
            screen.SetPixel(space,19,'\u2588', ConsoleColor.DarkBlue);
            space+=2;
            screen.CleanConsole();
            screen.Draw();
            Thread.Sleep(1000);
        }
        ani.TextAnimation("Blue Bulet - "+blueBul+", Red Bulet - "+redBul, 59,19);

    }


}