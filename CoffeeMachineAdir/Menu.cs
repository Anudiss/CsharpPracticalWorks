using System;
using System.Linq;

namespace CoffeeMachineAdir
{
    public static class Menu
    {
        public static T SelectMenu<T>(string title, Func<T, string> print, params T[] objects)
        {
            Console.CursorVisible = false;
            int y = Console.CursorTop;
            int selected = 0;

            void Draw()
            {
                #region Clear

                for (int i = 0; i < objects.Length; i++)
                {
                    Console.CursorTop = y + i;
                    Console.CursorLeft = 0;
                    Console.Write(string.Join("", Enumerable.Repeat(" ", Console.WindowWidth)));
                }

                #endregion

                Console.CursorTop = y;
                Console.CursorLeft = 0;
                Console.WriteLine($"{title}:");

                for (int i = 0; i < objects.Length; i++)
                {
                    if (i == selected)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine($"{print(objects[i])}");

                    Console.ResetColor();
                }
            }

            Draw();
            ConsoleKey key;
            while ((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
            {
                if (key == ConsoleKey.UpArrow)
                    selected--;
                else if (key == ConsoleKey.DownArrow)
                    selected++;

                selected = Math.Min(objects.Length - 1, Math.Max(0, selected));
                Draw();
            }

            return objects[selected];
        }
    }
}
