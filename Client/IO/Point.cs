using System.Security.Cryptography.X509Certificates;

namespace IO
{
    public struct Point
    {
        public char Value;
        public ConsoleColor Color;

        public ConsoleColor BackgroundColor;


        public Point(char Symbol, ConsoleColor color = ConsoleColor.White, ConsoleColor bkcolor = ConsoleColor.Black)
        {
            Value = Symbol;
            Color = color;
            BackgroundColor = bkcolor;
        }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}