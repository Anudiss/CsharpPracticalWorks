using CoffeeMachine.CoffeeMachineTypes;
using CoffeeMachine.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using static Colorful.Console;

namespace CoffeeMachine.Components.DisplayComponents
{
    public class DisplayComponent : Component
    {
        public List<Coffee> Coffees { get; }

        public int SelectedCoffee
        {
            get => _selectedCoffee;
            set
            {
                _selectedCoffee = Extensions.Clamp(0, Coffees.Count - 1, value);

                int selectedCoffeeHeight = Coffees.Where((coffee, i) => i < _selectedCoffee)
                                                  .Select(coffee => 1 + (coffee.IsOpen ? coffee.Recipe.RecipePosition.Count : 0))
                                                  .Sum();
                _scroll = Extensions.Clamp(0, _coffeesHeight - Height + 1, selectedCoffeeHeight);

                Draw();
            }
        }

        private int _coffeesHeight;
        private int _scroll = 0;
        private int _selectedCoffee = 0;

        public DisplayComponent(int x, int y, int width, int height, params Coffee[] coffees) : base(x, y, width, height)
        {
            Coffees = coffees.ToList();
            _coffeesHeight = Coffees.Select(coffee => 1 + (coffee.IsOpen ? coffee.Recipe.RecipePosition.Count : 0)).Sum();

            KeyBoard.KeyPressed += Control;
        }

        public void AddCoffee(Coffee coffee)
        {
            Coffees.Add(coffee);
            _coffeesHeight = Coffees.Select(e => 1 + (e.IsOpen ? e.Recipe.RecipePosition.Count : 0)).Sum();
        }

        public void RemoveCoffee(Coffee coffee)
        {
            Coffees.Remove(coffee);
            _coffeesHeight = Coffees.Select(e => 1 + (e.IsOpen ? e.Recipe.RecipePosition.Count : 0)).Sum();
        }

        private void Control(ConsoleKeyInfo keyInfo)
        {
            if (IsFocus == false)
                return;

            if (keyInfo.Key == ConsoleKey.UpArrow)
                SelectedCoffee--;
            else if (keyInfo.Key == ConsoleKey.DownArrow)
                SelectedCoffee++;
            else if (keyInfo.Key == ConsoleKey.Enter)
            {
                Coffees[SelectedCoffee].ToggleMenu();
                _coffeesHeight = Coffees.Select(coffee => 1 + (coffee.IsOpen ? coffee.Recipe.RecipePosition.Count : 0)).Sum();
            }

            Draw();
        }

        public override void Draw()
        {
            //─│╭╮╰╯

            Clear();
            CursorLeft = X;
            CursorTop = Y;
            Write($"╭{"─".Repeat(Width - 2)}╮");

            int y = Y + 1 - _scroll;
            for (int i = 0; i < Coffees.Count; i++)
            {
                var style = i == SelectedCoffee ? ComponentStyles.FocusText : ComponentStyles.Default;
                Coffee coffee = Coffees[i];

                CursorLeft = X;
                if (Extensions.IsInside(0, Height - 2, y - Y - 1))
                {
                    CursorTop = y++;
                    WriteStyled($"│{(coffee.IsOpen ? "-" : "+")} {coffee.Name.PadRight(Width - 4)}│", style);

                }
                else
                    y++;
                if (coffee.IsOpen == false)
                    continue;

                foreach (var recipePosition in coffee.Recipe.RecipePosition)
                {
                    CursorLeft = X;
                    if (Extensions.IsInside(0, Height - 2, y - Y - 1))
                    {
                        CursorTop = y++;
                        WriteStyled($"│  {recipePosition.Key.Name.PadRight(Width - 4 - $"{recipePosition.Value}".Length, '.')}{recipePosition.Value}│", style);
                    }
                    else
                        y++;
                }
            }

            for (int i = y; i < Y + Height; i++)
            {
                CursorLeft = X;
                CursorTop = i;
                Write($"│{" ".Repeat(Width - 2)}│");
            }

            CursorLeft = X;
            CursorTop = Y + Height;
            Write($"╰{"─".Repeat(Width - 2)}╯");
        }
    }
}
