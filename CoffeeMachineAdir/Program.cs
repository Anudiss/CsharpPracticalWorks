using System;
using System.Collections.Generic;

namespace CoffeeMachineAdir
{
    public class Program
    {
        public static readonly List<Coffee> Coffees = new List<Coffee>()
        {
            new Coffee("Капучино", 83.7),
            new Coffee("Капучино", 83.7),
            new Coffee("Капучино", 83.7),
            new Coffee("Капучино", 83.7),
            new Coffee("Капучино", 83.7),
            new Coffee("Капучино", 83.7)
        };

        public static readonly List<Additive> Additives = new List<Additive>()
        {
            new Additive("Сахар", 2.1),
            new Additive("Сахар", 2.1),
            new Additive("Сахар", 2.1),
            new Additive("Сахар", 2.1),
            new Additive("Сахар", 2.1),
            new Additive("Сахар", 2.1),
            new Additive("Сахар", 2.1)
        };

        static void Main(string[] args)
        {
            Coffee coffee = Menu.SelectMenu(
                title: "Выберите кофе",
                print: (e) => $"{e.Name} {e.Price}",
                Coffees.ToArray());

            Console.Clear();

            Additive additive = Menu.SelectMenu(
                title: "Выбрите добавку",
                print: (e) => $"{e.Name} {e.Price}",
                Additives.ToArray());

            Console.Clear();

            double sum = coffee.Price + additive.Price;

            void Check()
            {
                Console.WriteLine($"Вы заказали:");
                Console.WriteLine($"{coffee.Name.PadRight(20, '.')}{coffee.Price}");
                Console.WriteLine($"{additive.Name.PadRight(20, '.')}{additive.Price}");
                Console.WriteLine($"{"Итого".PadRight(20, '.')}{sum}");
            }

            Check();

            double userInput = 0;
            while ((userInput += InputMoney("Внесите деньги")) < sum)
            {
                Console.Clear();
                Check();
                Console.WriteLine($"Недостаточно! Осталось: {sum - userInput}");
            }

            Console.WriteLine($"Ваш кофе готов\nВаша сдача: {userInput - sum}");
            Console.ReadKey();
        }

        static double InputMoney(string message)
        {
            while (true)
            {
                try
                {
                    Console.Write($"{message}: ");
                    double input = double.Parse(Console.ReadLine());
                    if (input < 0)
                        throw new StackOverflowException();
                    return input;
                }
                catch (Exception)
                {
                    Console.WriteLine("Неверный ввод\nНажмите любую клавишу, чтобы продолжить...");
                    Console.ReadKey();
                }
            }
        }
    }
}
