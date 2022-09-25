using System;
using static Colorful.Console;

namespace CoffeeMachine.Components
{
    public abstract class Component
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsFocus
        {
            get => _focus;
            set
            {
                _focus = value;
                if (_focus)
                    OnComponentFocused?.Invoke(this);
                else
                    OnComponentUnfocused?.Invoke(this);
            }
        }

        public event Action<Component> OnComponentFocused;
        public event Action<Component> OnComponentUnfocused;

        private bool _focus;
        private readonly string _clear;

        public Component(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;

            _clear = " ".Repeat(Width);
        }

        public abstract void Draw();

        public void Clear()
        {
            for (int y = Y; y < Y + Height; y++)
            {
                CursorLeft = X;
                CursorTop = y;
                Write(_clear);
            }
        }
    }
}
