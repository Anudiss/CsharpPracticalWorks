using CoffeeMachineResourse.IO;
using CoffeeMachineResourse.Types;
using Colorful;
using System.Collections.Generic;
using System.Linq;
using static CoffeeMachineResourse.IO.Control;
using static CoffeeMachineResourse.IO.ControlCommand;
using static Colorful.Console;

namespace CoffeeMachineResourse.Component.DisplayComponents
{
    public class DisplayComponent : Component
    {
        public readonly List<(Coffee coffee, int count)> Coffees = new List<(Coffee coffee, int count)>();
        private int _selectedCoffee;
        private int _scroll;

        public int SelectedCoffee
        {
            get => _selectedCoffee;
            set
            {
                _selectedCoffee = Extensions.Clamp(0, Coffees.Count - 1, value);

                int selectedCoffeeHeight = Coffees.Where((coffee, i) => i < _selectedCoffee)
                                                   .Select(coffee => 1 + (coffee.coffee.IsOpen ? coffee.coffee.Recipe.Count : 0))
                                                   .Sum();
                _scroll = Box.Y + 1 - Extensions.Clamp(0, CoffeesHeight - Box.Height + 1, selectedCoffeeHeight);
            }
        }
        public int CoffeesHeight => Coffees.Select(coffee => 1 + (coffee.coffee.IsOpen ? coffee.coffee.Recipe.Count : 0)).Sum();

        public DisplayComponent(Box box, params Coffee[] coffees) : base(box)
        {
            AddCoffee(coffees);

            SelectedCoffee = 0;
            Keyboard.OnKeyPressed += Control;
        }

        private void Control(KeyInfo keyInfo)
        {
            if (IsFocus == false)
                return;

            if (keyInfo == ControlKeys[MoveUp])
                SelectedCoffee--;
            else if (keyInfo == ControlKeys[MoveDown])
                SelectedCoffee++;
            else if (keyInfo == ControlKeys[Delete])
                RemoveCoffee(Coffees[SelectedCoffee].coffee);
            else if (keyInfo == ControlKeys[Increment])
                AddCoffee(Coffees[SelectedCoffee].coffee);
            else if (keyInfo == ControlKeys[Decrement])
                RemoveCoffee(Coffees[SelectedCoffee].coffee);
            else if (keyInfo == ControlKeys[Select])
                Coffees[SelectedCoffee].coffee.Toggle();

            Draw();
        }

        public void AddCoffee(params Coffee[] coffees)
        {
            for (int i = 0; i < coffees.Length; i++)
            {
                if (Coffees.Select(e => e.coffee).Contains(coffees[i]))
                {
                    int index = Coffees.FindIndex(e => e.coffee == coffees[i]);
                    Coffees[index] = (coffees[i], Coffees[index].count + 1);
                }
                else
                {
                    Coffees.Add((coffees[i], 1));
                }
            }
            Draw();
        }

        public void RemoveCoffee(Coffee coffee)
        {
            if (Coffees.Select(e => e.coffee).Contains(coffee) == false)
                return;

            int index = Coffees.FindIndex(e => e.coffee == coffee);
            if (Coffees[index].count < 2)
            {
                Coffees.RemoveAt(index);
                SelectedCoffee--;
            }
            else
                Coffees[index] = (coffee, Coffees[index].count - 1);
            Draw();
        }

        public override void Focused()
        {
            base.Focused();
            Draw();
        }

        public override void Unfocused()
        {
            base.Unfocused();
            Draw();
        }

        public override void Draw()
        {
            //─│╭╮╰╯
            Clear();

            Box.Draw();

            if (Coffees.Count == 0)
                return;

            CursorLeft = Box.X + Box.Width - 1;
            CursorTop = Box.Y + 1 + (int)((double)SelectedCoffee / Coffees.Count * Box.Height);
            Write("┃");

            int y = _scroll;
            for (int i = 0; i < Coffees.Count; i++)
            {
                Coffee coffee = Coffees[i].coffee;

                #region Styles
                StyleSheet style;
                if (IsFocus)
                    style = i == SelectedCoffee ? ComponentStyles.FocusText : ComponentStyles.Default;
                else
                    style = ComponentStyles.Default;
                #endregion
                #region Drawing coffee name
                if (Extensions.IsInside(Box.Y + 1, Box.Y + Box.Height - 1, y))
                {
                    CursorLeft = Box.X + 1;
                    CursorTop = y;
                    WriteStyled($"{(coffee.IsOpen ? "-" : "+")} {Coffees[i].count}x{coffee.Name}", style);
                }
                y++;
                #endregion
                #region Drawing recipe
                if (coffee.IsOpen == false)
                    continue;

                foreach (var recipePosition in coffee.Recipe)
                {
                    if (Extensions.IsInside(Box.Y + 1, Box.Y + Box.Height - 1, y) == false)
                    {
                        y += 1;
                        continue;
                    }

                    CursorLeft = Box.X + 1;
                    CursorTop = y++;
                    string value = $"{recipePosition.Value * Coffees[i].count}";
                    WriteStyled($"  {recipePosition.Key.Name.PadRight(Box.Width - 4 - value.Length, '.')}{value}", style);
                }
                #endregion
            }
        }
    }
}
