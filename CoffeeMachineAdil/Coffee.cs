using System.Collections.Generic;

namespace CoffeeMachineAdil
{
    public class Coffee
    {
        public string Name { get; }
        public Dictionary<Resourse, int> Recipe;

        public Coffee(string name, params (Resourse, int)[] recipePosition)
        {
            Name = name;
            foreach ((Resourse resourse, int count) in recipePosition)
                Recipe.Add(resourse, count);
        }
    }
}
