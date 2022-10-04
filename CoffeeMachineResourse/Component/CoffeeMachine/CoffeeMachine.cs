using CoffeeMachineResourse.Component.DisplayComponents;
using CoffeeMachineResourse.Component.SelectComponents;
using CoffeeMachineResourse.Component.ResourseComponents;
using CoffeeMachineResourse.Types;
using System;
using System.Collections.Generic;
using CoffeeMachineResourse.IO;
using static CoffeeMachineResourse.IO.Control;
using static CoffeeMachineResourse.IO.ControlCommand;
using System.Linq;
using static Colorful.Console;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Input;
using System.Threading;

namespace CoffeeMachineResourse.Component.CoffeeMachine
{
    public class CoffeeMachine : Component
    {
        private readonly List<Component> Components = new List<Component>();
        private int _selectedComponent;

        public int SelectedComponent
        {
            get => _selectedComponent;
            set
            {
                _selectedComponent = Extensions.Clamp(0, Components.Count - 1, value);

                Components[_selectedComponent].IsFocus = true;
                Draw();
            }
        }

        public CoffeeMachine(Box box) : base(box)
        {
            ResourseContainerGroup resourseContainerGroup = new ResourseContainerGroup(
                    x: Box.X + 1,
                    y: Box.Y + 1,
                    width: Box.Width - 2,
                    Instances.GetTypes<(Resourse, int, int)>(typeof((Resourse, int, int)))
                )
            {
                IsFocus = false
            };
            resourseContainerGroup.OnComponentFocused += ComponentFocused;

            SelectComponent selectComponent = new SelectComponent(
                    new Box(
                            x: Box.X + 1,
                            y: Box.Y + 2 + resourseContainerGroup.Box.Height,
                            width: (Box.Width - 2) / 3 * 2,
                            height: Box.Height - 3 - resourseContainerGroup.Box.Height
                        ),
                    rows: 3,
                    columns: 2,
                    Instances.GetTypes<Coffee>(typeof(Coffee))
                )
            {
                IsFocus = false
            };
            selectComponent.OnComponentFocused += ComponentFocused;

            DisplayComponent displayComponent = new DisplayComponent(
                    new Box(
                            x: Box.X + Box.Width - (Box.Width - 2 - selectComponent.Box.Width),
                            y: selectComponent.Box.Y,
                            width: (Box.Width - 2) / 3,
                            height: selectComponent.Box.Height
                        )
                )
            {
                IsFocus = false
            };
            displayComponent.OnComponentFocused += ComponentFocused;

            Components.Add(resourseContainerGroup);
            Components.Add(selectComponent);
            Components.Add(displayComponent);

            IsFocus = true;

            SelectedComponent = 1;

            Keyboard.OnKeyPressed += Control;

            selectComponent.OnCoffeeSelected += CoffeeSelected;
            selectComponent.OnCoffeeDeleted += CoffeeDeleted;
        }

        private void Control(KeyInfo keyInfo)
        {
            if (keyInfo == ControlKeys[FocusNext])
                SelectedComponent++;
            else if (keyInfo == ControlKeys[FocusPrevious])
                SelectedComponent--;
            else if (keyInfo == ControlKeys[Preparing])
                PreparingCoffee();
        }

        private void PreparingCoffee()
        {
            Dictionary<Resourse, double> neededResourses = new Dictionary<Resourse, double>();
            Dictionary<Resourse, double> lack = new Dictionary<Resourse, double>();
            #region NeedResourses Calculating
            foreach ((Coffee coffee, double coffeeCount) in (Components[2] as DisplayComponent).Coffees)
            {
                foreach (var keyPair in coffee.Recipe)
                {
                    if (neededResourses.ContainsKey(keyPair.Key))
                        neededResourses[keyPair.Key] += keyPair.Value * coffeeCount;
                    else
                        neededResourses.Add(keyPair.Key, keyPair.Value * coffeeCount);
                }
            }
            #endregion
            #region Is resourses enough
            bool enough = true;
            ResourseContainerGroup resourseContainerGroup = (Components[0] as ResourseContainerGroup);
            foreach (var (resourse, count) in neededResourses.Select(keyPair => (keyPair.Key, keyPair.Value)))
            {
                if (resourseContainerGroup.ResourseContainers.ContainsKey(resourse) == false)
                {
                    enough = false;
                    lack.Add(resourse, count);
                }
                if (resourseContainerGroup[resourse].Count < count)
                {
                    enough = false;
                    lack.Add(resourse, count - resourseContainerGroup[resourse].Count);
                }
            }
            #endregion
            #region Error is not enough resourses
            if (enough == false)
            {
                Error($"Не достаточно ресурсов\n  {string.Join("\n  ", lack.Select(keyPair => $"{keyPair.Key.Name}: {keyPair.Value:F2}"))}");
                return;
            }
            #endregion
            #region Waste resourses
            foreach (var (resourse, count) in neededResourses.Select(e => (e.Key, e.Value)))
            {
                double step = count / 10;
                for (int i = 0; i < count / step; i++)
                {
                    resourseContainerGroup[resourse].Count -= step;
                    resourseContainerGroup.Draw();
                    Thread.Sleep(50);
                }
            }

            (Components[2] as DisplayComponent).Coffees.Clear();
            #endregion
            Draw();
        }

        private void CoffeeSelected(Coffee coffee) => (Components[2] as DisplayComponent).AddCoffee(coffee);
        private void CoffeeDeleted(Coffee coffee) => (Components[2] as DisplayComponent).RemoveCoffee(coffee);

        private void ComponentFocused(Component component)
        {
            for (int i = 0; i < Components.Count; i++)
                if (Components[i] != component)
                    Components[i].IsFocus = false;
        }

        public override void Draw()
        {
            Clear();

            Box.Draw();

            foreach (var component in Components)
                component.Draw();

            #region Drawing instruction

            int cols = 4;
            (ControlCommand controlCommand, KeyInfo keyInfo)[] instruction = ControlKeys.Select(e => (e.Key, e.Value)).ToArray();
            int width = instruction.Max(e => Print(e.controlCommand, e.keyInfo).Length) + 3;
            int x = (WindowWidth - width * cols + 3) / 2;
            for (int i = 0; i < instruction.Length; i++)
            {
                CursorLeft = x + i % cols * width;
                CursorTop = WindowHeight - i / cols - 1;
                Write(Print(instruction[i].controlCommand, instruction[i].keyInfo));
            }

            #endregion
        }

        public void Error(string message)
        {
            string[] lines = message.Split('\n');
            int width = lines.Max(e => e.Length) + 2;
            int height = lines.Length + 1;
            int x = (Console.WindowWidth - width) / 2;
            int y = (Console.WindowHeight - height) / 2;

            CursorLeft = x;
            CursorTop = y++;
            BackgroundColor = Color.DarkRed;
            Write("Ошибка".PadLeft((width + 6) / 2).PadRight(width));

            BackgroundColor = Color.Snow;
            ForegroundColor = Color.Black;
            foreach (string line in lines)
            {
                CursorLeft = x;
                CursorTop = y++;
                Write($"{line}".PadRight(width));
            }

            BackgroundColor = Color.White;
            ForegroundColor = Color.Black;

            ReadKey(true);
            Draw();
        }
    }
}
