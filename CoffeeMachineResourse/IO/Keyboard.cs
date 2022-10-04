using static Colorful.Console;
using System;
using System.Threading;

namespace CoffeeMachineResourse.IO
{
    public static class Keyboard
    {
        public static event KeyPressed OnKeyPressed;
        public delegate void KeyPressed(KeyInfo keyInfo);

        static Keyboard() => new Thread(new ThreadStart(Input)).Start();

        private static void Input()
        {
            while (true)
            {
                ConsoleKeyInfo keyInfo = ReadKey(true);
                OnKeyPressed?.Invoke(new KeyInfo() { Key = keyInfo.Key, Modifiers = keyInfo.Modifiers });

                Thread.Sleep(1000 / 30);
            }
        }
    }
}
