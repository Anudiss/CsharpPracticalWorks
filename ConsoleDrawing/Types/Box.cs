using System;

namespace ConsoleDrawing.Types
{
    public struct Box
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

        public Box(Point leftTop, Point bottomRight)
        {
            X = leftTop.X;
            Y = leftTop.Y;
            Width = bottomRight.X - leftTop.X;
            Height = bottomRight.Y - leftTop.Y;
        }

        public static implicit operator Box((int x, int y, int width, int height) box) => new(box.x, box.y, box.width, box.height);
    }
}
