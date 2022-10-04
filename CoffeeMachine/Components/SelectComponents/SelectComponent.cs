using System;
using System.Linq;
using System.Collections.Generic;
using static Colorful.Console;
using CoffeeMachine.IO;
using CoffeeMachine.CoffeeMachineTypes;

namespace CoffeeMachine.Components.SelectComponents
{
    public class SelectComponent : Component
    {
        private int _page;

        public int Columns { get; }
        public int Rows { get; }
        public int Page
        {
            get => _page;
            set
            {
                _page = Extensions.Clamp(0, PagesCount - 1, value);
                Draw();
            }
        }
        public int PagesCount { get; }
        public int MaxWidth { get; }

        public event Action<SelectableComponent> OnSelect;

        public List<SelectableComponent> Components = new List<SelectableComponent>();

        private int _selectedComponent;

        public int SelectedComponent
        {
            get => _selectedComponent;
            set
            {
                _selectedComponent = Extensions.Clamp(0, Components.Count - 1, value);
                Components[_selectedComponent].IsFocus = true;

                Page = SelectedComponent / (Rows * Columns);
            }
        }

        public SelectComponent(int x, int y, int selectionWidth, int selectionHeight, int rows = 3, int cols = 2, params Coffee[] objects)
            : base(x, y, selectionWidth, selectionHeight)
        {
            Columns = cols;
            Rows = rows;
            PagesCount = (int)Math.Ceiling((double)objects.Length / (rows * cols));

            MaxWidth = objects.Max(e => $"{e.Name.PadRight(((Width - 2) / Columns + e.Name.Length) / 2).PadLeft((Width - 2) / Columns)}".Length);

            int startX = X + (Width - 2 - MaxWidth * cols) / 2;
            for (int i = 0; i < PagesCount; i++)
            {
                Coffee[] pageObjects = objects.Slice(
                                                index: i * rows * cols,
                                                count: rows * cols).ToArray();
                for (int j = 0; j < pageObjects.Length; j++)
                {
                    int xx = j / rows;
                    int yy = j % rows;

                    SelectableComponent component = new SelectableComponent(x: startX + xx * MaxWidth + 1,
                                                            y: Y + yy * 3 + 1,
                                                            width: MaxWidth,
                                                            value: pageObjects[j]);
                    component.OnComponentFocused += OnFocus;
                    Components.Add(component);
                }
            }

            Components[0].IsFocus = true;

            KeyBoard.KeyPressed += Control;
        }
        public SelectComponent(int x, int y, int rows = 3, int cols = 2, params Coffee[] objects)
            : this(x, y, objects.Max(e => e.Display().Length + 2) * cols + 2, rows * 3 + 1, rows, cols, objects)
        {
        }

        private void Control(ConsoleKeyInfo keyInfo)
        {
            if (IsFocus == false)
                return;

            if (keyInfo.Key == ConsoleKey.UpArrow)
                SelectedComponent--;
            else if (keyInfo.Key == ConsoleKey.DownArrow)
                SelectedComponent++;
            else if (keyInfo.Key == ConsoleKey.LeftArrow)
                SelectedComponent -= Rows;
            else if (keyInfo.Key == ConsoleKey.RightArrow)
                SelectedComponent += Rows;
            else if (keyInfo.Key == ConsoleKey.Enter)
                OnSelect?.Invoke(Components[SelectedComponent]);
        }

        public override void Draw()
        {
            //○●
            //─│╭╮╰╯
            Clear();

            CursorLeft = X;
            CursorTop = Y;
            Write($"╭{"─".Repeat(Width - 2)}╮");

            for (int y = Y + 1; y < Y + Height; y++)
            {
                CursorLeft = X;
                CursorTop = y;
                Write($"│");

                CursorLeft = X + Width - 1;
                CursorTop = y;
                Write($"│");
            }

            CursorLeft = X;
            CursorTop = Y + Height;
            Write($"╰{"─".Repeat(Width - 2)}╯");

            foreach (var selectComponent in Components.Slice(index: Page * Rows * Columns,
                                                             count: Rows * Columns))
                selectComponent.Draw();

            Title = $"Page: {Page} Selected component: {_selectedComponent}";

            string pagesString = string.Join(" ", "○".Repeat(PagesCount - 1).Insert(Page, "●").ToCharArray());
            CursorLeft = X + (Width - pagesString.Length) / 2 + pagesString.Length % 2;
            CursorTop = Y + Height;
            Write(pagesString);
        }

        private void OnFocus(Component component)
        {
            foreach (var comp in Components)
                if (comp != component)
                    comp.IsFocus = false;
        }
    }
}