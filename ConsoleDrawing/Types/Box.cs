using System;

namespace ConsoleDrawing.Types
{
    public struct Box
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public static readonly Box Default = new(0, 0, Console.WindowWidth, Console.WindowHeight);

        public Box(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public Box(Point leftTop, Point bottomRight)
        {
            X = leftTop.X;
            Y = leftTop.Y;
            Width = bottomRight.X - leftTop.X;
            Height = bottomRight.Y - leftTop.Y;
        }

        public static implicit operator Box((int x, int y, int width, int height) box) => new(box.x, box.y, box.width, box.height);

        public static bool operator ==(Box box1, Box box2) => box1.X == box2.X && box1.Y == box2.Y && box1.Width == box2.Width && box1.Height == box2.Height;
        public static bool operator !=(Box box1, Box box2) => box1.X != box2.X && box1.Y != box2.Y && box1.Width != box2.Width && box1.Height != box2.Height;

    }
}
