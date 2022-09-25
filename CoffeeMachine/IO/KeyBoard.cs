using System;
using System.Threading;
using static Colorful.Console;

namespace CoffeeMachine.IO
{
    public static class KeyBoard
    {
        public static event Action<ConsoleKeyInfo> KeyPressed;

        static KeyBoard() => new Thread(new ThreadStart(Input)).Start();

        private static void Input()
        {
            while(true)
            {
                KeyPressed?.Invoke(ReadKey(true));

                Thread.Sleep(1000 / 30);
            }
        }
    }
}
