using System;
using System.Reflection.Metadata.Ecma335;

namespace ConsoleDrawing.Types
{
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Point TopLeft(Box box) => new(box.X, box.Y);
        public static Point TopCenter(Box box) => new(box.X + box.Width / 2, box.Y);
        public static Point TopRight(Box box) => new(box.X + box.Width, box.Y);
        public static Point MiddleLeft(Box box) => new(box.X, box.Y + box.Height / 2);
        public static Point MiddleCenter(Box box) => new(box.X + box.Width / 2, box.Y + box.Height / 2);
        public static Point MiddleRight(Box box) => new(box.X + box.Width, box.Y + box.Height / 2 );
        public static Point BottomLeft(Box box) => new(box.X, box.Y + box.Height);
        public static Point BottomCenter(Box box) => new(box.X + box.Width / 2, box.Y + box.Height);
        public static Point BottomRight(Box box) => new(box.X + box.Width, box.Y + box.Height);

        public static Point operator +(Point point1, Point point2) => new(point1.X + point2.X, point1.Y + point2.Y);
        public static Point operator -(Point point1, Point point2) => new(point1.X - point2.X, point1.Y - point2.Y);
        public static Point operator *(Point point, int number) => new(point.X * number, point.Y * number);
        public static implicit operator Point((int x, int y) point) => new(point.x, point.y);
    }

    [Flags]
    public enum Alignment
    {
        None = 0,
        Top = 1,
        Middle = 2,
        Bottom = 4,
        Left =  8,
        Center = 16,
        Right = 32
    }
}
