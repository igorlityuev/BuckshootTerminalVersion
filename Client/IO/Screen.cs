using System.Text;

namespace IO
{
    public class Screen
    {
        int width = 140, height = 45;
        private Point[,] screen;


        ~Screen()
        {
            Console.CursorVisible = true;
        }


        public int Width
        {
            get
            {
                return width;
            }
        }

        public int Height
        {
            get
            {
                return height;
            }
        }

        public Screen()
        {
            width = 140;
            height = 45;
            screen = new Point[width, height];
            
        }

        public Screen(int width, int height)
        {
            this.width = width;
            this.height = height;
            screen = new Point[width, height];
        }
        public void SetPixel(int x, int y, char c, ConsoleColor color = ConsoleColor.White, ConsoleColor backgroundcolor = ConsoleColor.Black)
        {
            if (x < 0 || x >= width || y < 0 || y >= height)
                return;

            screen[x, y] = new Point(c, color, backgroundcolor);
        }
        public void ClearScreen()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    screen[x, y] = new Point(' ', ConsoleColor.White, ConsoleColor.Black);
                }
            }
        }


        public void CleanConsole()
        {
            Console.Clear();
        }

        public void Draw()
        {
            var output = new StringBuilder();
            ConsoleColor? consoleColor = null;
            ConsoleColor? bkColor = null;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var cell = screen[x, y];
                    if (cell.Color != consoleColor)
                    {
                        System.Console.Write(output.ToString());
                        output.Clear();

                        consoleColor = Console.ForegroundColor = cell.Color;
                    }
                     if (cell.BackgroundColor != bkColor)
                    {
                        System.Console.Write(output.ToString());
                        output.Clear();
                        bkColor = Console.BackgroundColor = cell.BackgroundColor;
                    }
                    output.Append(cell.Value);
                }
                output.AppendLine();
            }

            System.Console.Write(output.ToString());
            Console.ResetColor();
        }


        public void SetString(string text,int x, int y,ConsoleColor color = ConsoleColor.White, ConsoleColor bkcolor = ConsoleColor.Black)
        {
            for (int i = 0; i < text.Length; i++)
            {
                SetPixel(x + i, y, text[i], color,bkcolor);
            }
        }

    }
}