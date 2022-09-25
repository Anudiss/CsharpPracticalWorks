using ConsoleDrawing.Types;
using System;

namespace ConsoleDrawing.IO
{
    public static class Console
    {
        static Console()
        {
            
        }

        public static void Write(string text, Box box = default, Alignment alignment = Alignment.Top | Alignment.Left)
        {
            if (box == default)
                box = Box.Default;
            string[] lines = text.PutIntoBox(box).Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                System.Console.CursorLeft = box.X;
                System.Console.CursorTop = box.Y + i;
                System.Console.Write(line);
            }
        }

        public static void WriteLine(string text)
        {

        }
    }
}
