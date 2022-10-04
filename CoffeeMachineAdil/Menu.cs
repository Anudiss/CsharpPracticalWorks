using System;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeMachineAdil
{
    public static class Menu
    {
        public static T SelectMenu<T>(string title, Func<T, string> print, params T[] values)
        {
            Console.CursorVisible = false;
            int selected = 0;
            int y = Console.CursorTop;

            void Draw()
            {
                #region Clear
                for (int i = 0; i < values.Length; i++)
                {
                    Console.CursorLeft = 0;
                    Console.CursorTop = y + i + 1;
                    Console.WriteLine(string.Join("", Enumerable.Repeat(" ", Console.WindowWidth)));
                }
                #endregion

                Console.CursorLeft = 0;
                Console.CursorTop = y;
                Console.WriteLine($"{title}:");

                for (int i = 0; i < values.Length; i++)
                {
                    if (i == selected)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.CursorLeft = 0;
                    Console.CursorTop = y + i + 1;
                    Console.WriteLine($"{print(values[i])}");

                    Console.ResetColor();
                }
            }

            Draw();
            ConsoleKey key;
            while((key = Console.ReadKey(true).Key) != ConsoleKey.Enter)
            {
                if (key == ConsoleKey.UpArrow)
                    selected--;
                else if (key == ConsoleKey.DownArrow)
                    selected++;

                selected = Math.Min(values.Length - 1, Math.Max(0, selected));

                Draw();
            }

            Console.CursorVisible = true;
            return values[selected];
        }
        public static void ShowList(string title, Dictionary<Resourse, int> recipe)
        {
            //─│┌┐└┘
            int maxCountWidth = recipe.Max(recipePosition => $"{recipePosition.Value}".Length);
            int maxNameWidth = recipe.Max(recipePosition => $"{recipePosition.Key.Name}".Length) + 5;
            int maxWidth = Math.Max(title.Length, maxCountWidth + maxNameWidth);

            Console.CursorLeft = Console.WindowWidth - maxWidth - 1;
            Console.CursorTop = 0;
            Console.WriteLine($"│{title.PadLeft((maxWidth + title.Length) / 2).PadRight(maxWidth)}");

            int y = 1;
            foreach ((Resourse resourse, int count) in recipe.Select(e => (e.Key, e.Value)))
            {
                Console.CursorLeft = Console.WindowWidth - maxWidth - 1;
                Console.CursorTop = y++;
                Console.WriteLine($"│{resourse.Name.PadRight(maxNameWidth, '.')}{$"{count}".PadLeft(maxCountWidth, '.')}");
            }

            Console.CursorLeft = Console.WindowWidth - maxWidth - 1;
            Console.CursorTop = y;
            Console.WriteLine($"└{string.Join("", Enumerable.Repeat("─", maxWidth))}");
        }
    }
}
