using System.Text;
using System.Drawing;
using static Colorful.Console;
using CoffeeMachine.CoffeeMachineTypes;
using CoffeeMachine.Components;
using System.Threading;
using CoffeeMachine.Components.SelectComponents;
using CoffeeMachine.Components.ResourseComponents;
using CoffeeMachine.Components.DisplayComponents;
using System.Net.Http.Headers;

namespace CoffeeMachine
{
    class Program
    {
        public static Resourse Coffee = new Resourse("Кофе");
        public static Resourse Water = new Resourse("Вода");
        public static Resourse Milk = new Resourse("Молоко");

        public static ResourseContainer CoffeeResourseContainer = new ResourseContainer(Coffee, 400, 400);
        public static ResourseContainer WaterResourseContainer = new ResourseContainer(Water, 400, 400);
        public static ResourseContainer MilkResourseContainer = new ResourseContainer(Milk, 400, 400);

        static void Main(string[] args)
        {
            InitConsole();

            Components.CoffeeMachine coffeeMachine = new Components.CoffeeMachine();
            coffeeMachine.Draw();
        }

        private static void InitConsole()
        {
            Title = "Coffee Machine";

            InputEncoding = Encoding.Unicode;
            OutputEncoding = Encoding.Unicode;

            CursorVisible = false;

            BackgroundColor = Color.White;
            ForegroundColor = Color.Black;

            Clear();
        }
    }
}
