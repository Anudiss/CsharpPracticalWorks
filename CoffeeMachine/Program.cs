using System.Text;
using System.Drawing;
using static Colorful.Console;
using CoffeeMachine.CoffeeMachineTypes;
using CoffeeMachine.Components;
using System.Threading;

namespace CoffeeMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            InitConsole();

            ResourseContainer resourseContainer = new ResourseContainer(
                resourse: new Resourse("Кофе"),
                count: 400,
                capacity: 400);

            ResourseContainer resourseContainer1 = new ResourseContainer(
                resourse: new Resourse("Молоко"),
                count: 76,
                capacity: 100);

            ResourseContainer resourseContainer2 = new ResourseContainer(
                resourse: new Resourse("Вода"),
                count: 250,
                capacity: 300);

            ResourseContainerGroup resourseContainerGroup = new ResourseContainerGroup(10, 10, 80, resourseContainer, resourseContainer1, resourseContainer2);
            resourseContainerGroup.Draw();

            ReadKey();
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
