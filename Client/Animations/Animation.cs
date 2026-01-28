using IO;

namespace Animation
{
    class Anima
    {
        Screen screen;
        int n=1;

        public Anima(Screen screen){
            this.screen = screen;
        }

        public void DrawAsset(string[] asset, int x, int y, ConsoleColor color= ConsoleColor.White)
        {
            for (int ScY = 0; ScY < asset.Length; ScY++)
            {
                for (int ScX = 0; ScX < asset[ScY].Length; ScX++)
                {
                    screen.SetPixel(x + ScX, y + ScY, asset[ScY][ScX], color);
                }
            }
        }
        public void Wait()
        {
            char[] a = new char[]{'-','\\','|','/'};

            Box("Wait for Second Player "+a[n%a.Length], 4,10);
            n++;
        }
        public void TextAnimation(string a, int x, int y)
        {
            for (int i = 0; i<a.Length; i++)
            {
                screen.CleanConsole();
                screen.SetPixel(x,y,a[i]);
                x++;
                screen.Draw();
                Thread.Sleep(400);
            }
        }

        public void Box(string title, int y, int x, ConsoleColor color = ConsoleColor.White)
        {
            char[] text = title.ToCharArray();
            int t=0;
            for (int i = y-3; i<y; i++)
            {
                for (int j = x; j < x+title.Length+2; j++)
                {
                    System.Console.WriteLine();
                    if (   (i == y-3 && j == x) ||
                        (i == y-1 && j == x) ||
                        (i == y-3 && j == x + title.Length + 1) ||
                        (i == y-1 && j == x + title.Length + 1))
                    {
                        screen.SetPixel(j,i,'+', color);
                    }else if (i==y-3 || i==y-1)
                    {
                        screen.SetPixel(j,i,'-',color);
                    }else if (j==x+title.Length+1 || j==x)
                    {
                        screen.SetPixel(j,i,'|',color);

                    }
                    else
                    {
                        screen.SetPixel(j,i,text[t],color);
                        t++;
                        
                    } 
                }
            }
        }
    }
}