using CoffeeMachineResourse.Component.CoffeeMachine;
using CoffeeMachineResourse.Component.DisplayComponents;
using CoffeeMachineResourse.Component.ResourseComponents;
using CoffeeMachineResourse.Component.SelectComponents;
using CoffeeMachineResourse.Types;
using System.Drawing;
using System.Linq;
using System.Text;
using static Colorful.Console;

namespace CoffeeMachineResourse
{
    public class Program
    {
        public const int Width = 82;
        public const int Height = 25;

        static void Main(string[] args)
        {
            InitConsole();

            CoffeeMachine coffeeMachine = new CoffeeMachine(
                    new Box(
                            x: (WindowWidth - Width) / 2,
                            y: 0,
                            width: Width,
                            height: Height
                        )
                );

            coffeeMachine.Draw();
        }

        private static void InitConsole()
        {
            Title = "Coffee Machine";

            CursorVisible = false;

            InputEncoding = Encoding.Unicode;
            OutputEncoding = Encoding.Unicode;

            BackgroundColor = Color.White;
            ForegroundColor = Color.Black;

            Clear();
        }
    }
}
