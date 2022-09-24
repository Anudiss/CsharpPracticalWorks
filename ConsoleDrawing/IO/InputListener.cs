using System;
using System.Threading;

namespace ConsoleDrawing.IO
{
    public static class InputListener
    {
        public static Action<ConsoleKeyInfo> KeyPressed;

        static InputListener() => new Thread(new ThreadStart(Input)).Start();

        private static void Input()
        {
            while(true)
            {
                KeyPressed?.Invoke(System.Console.ReadKey(true));
            }
        }
    }
}
