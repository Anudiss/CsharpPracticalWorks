using System.Collections.Generic;

namespace CoffeeMachine.CoffeeMachineTypes
{
    public class Recipe
    {
        public Dictionary<Resourse, int> RecipePosition = new Dictionary<Resourse, int>();

        public Recipe(params (Resourse resourse, int count)[] recipePositions)
        {
            foreach ((Resourse resourse, int count) in recipePositions)
                RecipePosition.Add(resourse, count);
        }
    }
}
