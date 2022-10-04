using CoffeeMachineResourse.IO;
using CoffeeMachineResourse.Types;
using Colorful;
using System;
using System.Collections.Generic;
using System.Linq;
using static CoffeeMachineResourse.IO.Control;
using static CoffeeMachineResourse.IO.ControlCommand;
using static Colorful.Console;

namespace CoffeeMachineResourse.Component.SelectComponents
{
    public class SelectComponent : Component
    {
        private readonly List<Coffee> _coffees = new List<Coffee>();

        private int _selectedCoffee;
        private int _page;

        public int PagesCount { get; private set; }
        public int Rows { get; }
        public int Columns { get; }
        public int MaxWidth { get; private set; }
        public int SelectedCoffee
        {
            get => _selectedCoffee;
            set
            {
                _selectedCoffee = Extensions.Clamp(0, _coffees.Count - 1, value);
                Page = _selectedCoffee / (Rows * Columns);

                Draw();
            }
        }
        public int Page
        {
            get => _page;
            set => _page = Extensions.Clamp(0, PagesCount - 1, value);
        }

        public event Action<Coffee> OnCoffeeSelected;
        public event Action<Coffee> OnCoffeeDeleted;

        public SelectComponent(Box box, int rows, int columns, params Coffee[] coffees) : base(box)
        {
            Rows = rows;
            Columns = columns;

            AddCoffee(coffees);

            Keyboard.OnKeyPressed += Control;
        }

        private void Control(KeyInfo keyInfo)
        {
            if (IsFocus == false)
                return;

            if (keyInfo == ControlKeys[MoveLeft])
                SelectedCoffee -= Rows;
            else if (keyInfo == ControlKeys[MoveRigt])
                SelectedCoffee += Rows;
            else if (keyInfo == ControlKeys[MoveUp])
                SelectedCoffee -= 1;
            else if (keyInfo == ControlKeys[MoveDown])
                SelectedCoffee += 1;
            else if (keyInfo == ControlKeys[Select])
                OnCoffeeSelected?.Invoke(_coffees[SelectedCoffee]);
            else if (keyInfo == ControlKeys[Delete])
                OnCoffeeDeleted?.Invoke(_coffees[SelectedCoffee]);
        }

        public void AddCoffee(params Coffee[] coffees)
        {
            _coffees.AddRange(coffees);

            PagesCount = (int)Math.Ceiling((double)_coffees.Count / (Rows * Columns));
            MaxWidth = Math.Max((Box.Width - 2) / Columns, _coffees.Max(e => e.Name.Length) + 2);
        }

        public override void Draw()
        {
            //─│╭╮╰╯●○
            Clear();

            Box.Draw();

            #region Coffee drawing
            IEnumerator<(Coffee, int)> enumerator = _coffees.Select((e, i) => (e, i))
                                                            .Slice(Page * Rows * Columns, Rows * Columns)
                                                            .GetEnumerator();
            bool running = true;
            int centeredX = Box.X + (Box.Width - MaxWidth * Columns) / 2;
            for (int col = 0; col < Columns; col++)
            {
                if (running == false)
                    break;
                for (int row = 0; row < Rows; row++)
                {
                    running = enumerator.MoveNext();
                    if (running == false)
                        break;
                    (Coffee coffee, int i) = enumerator.Current;

                    StyleSheet style;
                    if (IsFocus)
                        style = i == SelectedCoffee ? ComponentStyles.FocusBorder : ComponentStyles.Default;
                    else
                        style = ComponentStyles.Default;

                    int xx = centeredX + col * MaxWidth;
                    int yy = Box.Y + 1 + row * 3;

                    CursorLeft = xx;
                    CursorTop = yy;
                    WriteStyled($"╭{"─".Repeat(MaxWidth - 2)}╮", style);

                    CursorLeft = xx;
                    CursorTop = yy + 1;
                    WriteStyled($"│{coffee.Name.PadLeft((MaxWidth - 2 + coffee.Name.Length) / 2).PadRight(MaxWidth - 2)}│", style);

                    CursorLeft = xx;
                    CursorTop = yy + 2;
                    WriteStyled($"╰{"─".Repeat(MaxWidth - 2)}╯", style);
                }
            }
            #endregion

            #region Pages indicator drawing
            string pagesIndicator = string.Join(" ", "○".Repeat(PagesCount - 1).Insert(Page, "●").ToCharArray());
            int pagesIndicatorY = Box.Y + Box.Height;
            int pagesIndicatorX = Box.X + (Box.Width - pagesIndicator.Length) / 2;
            
            CursorLeft = pagesIndicatorX;
            CursorTop = pagesIndicatorY;
            Write(pagesIndicator);
            #endregion
        }
    }
}
