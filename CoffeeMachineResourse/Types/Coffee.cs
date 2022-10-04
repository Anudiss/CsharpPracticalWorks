using System.Collections.Generic;
using System.Linq;

namespace CoffeeMachineResourse.Types
{
    public class Coffee
    {
        public string Name { get; }
        public Dictionary<Resourse, double> Recipe = new Dictionary<Resourse, double>();
        public bool IsOpen { get; private set; } = false;

        public Coffee(string name, params (Resourse, double)[] recipe)
        {
            Name = name;
            foreach ((Resourse resourse, double count) in recipe)
                Recipe.Add(resourse, count);
        }

        public Coffee(string name, params (object resourse, double count)[] recipe)
        {
            if (recipe.All(e => e.resourse is Coffee || e.resourse is Resourse) == false)
                return;

            Name = name;
            foreach ((object obj, double count) in recipe)
            {
                if (obj is Resourse resourse)
                {
                    if (Recipe.ContainsKey(resourse))
                        Recipe[resourse] += count;
                    else
                        Recipe.Add(resourse, count);
                }
                else if (obj is Coffee coffee)
                {
                    foreach ((Resourse res, double coun) in ((Resourse, double)[])coffee)
                    {
                        if (Recipe.ContainsKey(res))
                            Recipe[res] += coun * count;
                        else
                            Recipe.Add(res, coun * count);
                    }
                }
            }
        }

        public void Toggle() => IsOpen = !IsOpen;

        public static implicit operator (Resourse, double)[](Coffee coffee) => coffee.Recipe.Select(recipePosition => (recipePosition.Key, recipePosition.Value)).ToArray();
    }

    public class Resourse
    {
        public string Name { get; }

        public Resourse(string name)
        {
            Name = name;
        }
    }
}
