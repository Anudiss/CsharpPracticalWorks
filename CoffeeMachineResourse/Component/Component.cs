using CoffeeMachineResourse.Types;
using System;
using static Colorful.Console;

namespace CoffeeMachineResourse.Component
{
    public abstract class Component
    {
        public Box Box { get; }
        public bool IsFocus
        {
            get => _focus;
            set
            {
                _focus = value;
                if (_focus)
                    Focused();
                else
                    Unfocused();
            }
        }

        public event Action<Component> OnComponentFocused;
        public event Action<Component> OnComponentUnfocused;

        private bool _focus = false;
        private readonly string _clearString;

        public Component(Box box)
        {
            Box = box;
            _clearString = " ".Repeat(box.Width);
        }

        public abstract void Draw();
        public virtual void Focused() => OnComponentFocused?.Invoke(this);
        public virtual void Unfocused() => OnComponentUnfocused?.Invoke(this);
        public void Clear()
        {
            for (int y = Box.Y; y <= Box.Y + Box.Height; y++)
            {
                CursorLeft = Box.X;
                CursorTop = y;
                Write(_clearString);
            }
        }
    }
}
