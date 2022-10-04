using static Colorful.Console;

namespace CoffeeMachineResourse.Types
{
    public class Box
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Box(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public void Draw()
        {
            //─│╭╮╯╰
            CursorLeft = X;
            CursorTop = Y;
            Write($"╭{"─".Repeat(Width - 2)}╮");

            for (int y = Y + 1; y < Y + Height; y++)
            {
                CursorLeft = X;
                CursorTop = y;
                Write($"│");

                CursorLeft = X + Width - 1;
                CursorTop = y;
                Write($"│");
            }

            CursorLeft = X;
            CursorTop = Y + Height;
            Write($"╰{"─".Repeat(Width - 2)}╯");
        }
    }
}
