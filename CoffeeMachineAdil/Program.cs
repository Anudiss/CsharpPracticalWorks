using System;
using System.Collections.Generic;

namespace CoffeeMachineAdil
{
    public class Program
    {
        static void Main(string[] args)
        {
            Dictionary<Resourse, int> recipe = new Dictionary<Resourse, int>()
            {
                { Resourses.Coffee, 2 },
                { Resourses.Milk, 4 },
                { Resourses.Water, 3 }
            };
            Menu.ShowList("Рецепт", recipe);
        }
    }
}
