using CoffeeMachine.Interface;

namespace CoffeeMachine.CoffeeMachineTypes
{
    public class Coffee : IChild, ISelectable
    {
        public string Name { get; }
        public Recipe Recipe { get; }
        public bool IsOpen { get; set; }

        public Coffee(string name, Recipe recipe)
        {
            Name = name;
            Recipe = recipe;
        }

        public void ToggleMenu() => IsOpen = !IsOpen;
        public string Display() => Name;
        public string Display(int width) => $"{Name.PadRight((width + Name.Length) / 2)}";
    }
}
