using CoffeeMachine.CoffeeMachineTypes;
using CoffeeMachine.Components.DisplayComponents;
using CoffeeMachine.Components.ResourseComponents;
using CoffeeMachine.Components.SelectComponents;
using CoffeeMachine.Interface;
using CoffeeMachine.IO;
using System;
using System.Collections.Generic;
using static Colorful.Console;

namespace CoffeeMachine.Components
{
    public class CoffeeMachine : Component
    {
        public const int width = 70;
        public const int height = 25;

        public List<Component> Components { get; } = new List<Component>();

        private int _selectedComponent;

        public int SelectedComponent
        {
            get => _selectedComponent;
            set => _selectedComponent = Extensions.Clamp(0, Components.Count - 1, value);
        }


        public CoffeeMachine() : base((WindowWidth - width) / 2, (WindowHeight - height) / 2, width, height)
        {
            IsFocus = true;

            SelectComponent selectComponent = new SelectComponent(
                x: X + 1,
                y: Y + Height - 11,
                rows: 3,
                cols: 2,
                new Coffee("Кофе0", new Recipe((Program.Coffee, 2), (Program.Milk, 2), (Program.Water, 2))),
                new Coffee("Кофе1", new Recipe((Program.Coffee, 2), (Program.Milk, 2), (Program.Water, 2))),
                new Coffee("Кофе2", new Recipe((Program.Coffee, 2), (Program.Milk, 2), (Program.Water, 2))),
                new Coffee("Кофе3", new Recipe((Program.Coffee, 2), (Program.Milk, 2), (Program.Water, 2))),
                new Coffee("Кофе4", new Recipe((Program.Coffee, 2), (Program.Milk, 2), (Program.Water, 2))),
                new Coffee("Кофе5", new Recipe((Program.Coffee, 2), (Program.Milk, 2), (Program.Water, 2))),
                new Coffee("Кофе6", new Recipe((Program.Coffee, 2), (Program.Milk, 2), (Program.Water, 2))),
                new Coffee("Кофе7", new Recipe((Program.Coffee, 2), (Program.Milk, 2), (Program.Water, 2))),
                new Coffee("Кофе8", new Recipe((Program.Coffee, 2), (Program.Milk, 2), (Program.Water, 2))),
                new Coffee("Кофе9", new Recipe((Program.Coffee, 2), (Program.Milk, 2), (Program.Water, 2))),
                new Coffee("Кофе10", new Recipe((Program.Coffee, 2), (Program.Milk, 2), (Program.Water, 2))),
                new Coffee("Кофе11kjahjiop", new Recipe((Program.Coffee, 2), (Program.Milk, 2), (Program.Water, 2))),
                new Coffee("Кофе12", new Recipe((Program.Coffee, 2), (Program.Milk, 2), (Program.Water, 2))),
                new Coffee("Кофе13", new Recipe((Program.Coffee, 2), (Program.Milk, 2), (Program.Water, 2))))
            {
                IsFocus = true
            };

            ResourseContainerGroup resourseContainerGroup = new ResourseContainerGroup(
                x: X + 1,
                y: Y + 1,
                width: Width - 2,
                Program.CoffeeResourseContainer,
                Program.WaterResourseContainer,
                Program.MilkResourseContainer);

            Components.Add(selectComponent);
            Components.Add(resourseContainerGroup);

            KeyBoard.KeyPressed += Control;
        }

        private void Control(ConsoleKeyInfo keyInfo)
        {
            if (IsFocus == false)
                return;

            Draw();
        }

        public override void Draw()
        {
            Clear();

            //─│╭╮╰╯

            CursorLeft = X;
            CursorTop = Y;
            Write($"╭{"─".Repeat(Width - 2)}╮");

            for (int y = Y + 1; y < Y + Height; y++)
            {
                CursorLeft = X;
                CursorTop = y;
                Write($"│{" ".Repeat(Width - 2)}│");
            }

            CursorLeft = X;
            CursorTop = Y + Height;
            Write($"╰{"─".Repeat(Width - 2)}╯");

            foreach (var component in Components)
                component.Draw();
        }
    }
}
