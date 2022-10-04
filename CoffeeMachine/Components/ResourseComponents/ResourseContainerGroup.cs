using CoffeeMachine.IO;
using System;
using static Colorful.Console;

namespace CoffeeMachine.Components.ResourseComponents
{
    public class ResourseContainerGroup : Component<ResourseContainer>
    {
        private int _focusedResourseContainer;

        private int FocusedResourseContainer
        {
            get => _focusedResourseContainer;
            set
            {
                _focusedResourseContainer = Extensions.Clamp(0, Children.Count - 1, value);
                Children[_focusedResourseContainer].IsFocus = true;
            }
        }

        public ResourseContainerGroup(int x, int y, int width, params ResourseContainer[] resourseContainers) : base(x, y, width, resourseContainers.Length * 3 - resourseContainers.Length - 1)
        {
            for (int i = 0; i < resourseContainers.Length; i++)
            {
                AddChild(resourseContainers[i]);

                Children[i].X = X;
                Children[i].Y = Y + i * 2;
                Children[i].Width = Width;
                Children[i].Height = 2;
                Children[i].OnCountChanged += ResourseContainerCountChanged;
                Children[i].OnComponentFocused += ResourseContainerFocused;

                if (Children[i].IsFocus)
                    FocusedResourseContainer = i;
            }

            KeyBoard.KeyPressed += Control;
        }

        private void Control(ConsoleKeyInfo keyInfo)
        {
            if (IsFocus == false)
                return;

            if (keyInfo.Key == ConsoleKey.DownArrow)
                FocusedResourseContainer += 1;
            else if (keyInfo.Key == ConsoleKey.UpArrow)
                FocusedResourseContainer -= 1;

            if (keyInfo.Key == ConsoleKey.OemPlus)
                Children[FocusedResourseContainer].Count += 1 * (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift) ? 10 : 1);
            else if (keyInfo.Key == ConsoleKey.OemMinus)
                Children[FocusedResourseContainer].Count -= 1 * (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift) ? 10 : 1);
        }

        public override void Draw()
        {
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].Draw();

                if (i == 0)
                    continue;

                CursorLeft = X;
                CursorTop = Y + i * 2;
                Write("├");
                CursorLeft = X + Width - 1;
                CursorTop = Y + i * 2;
                Write("┤");
            }
        }

        private void ResourseContainerFocused(Component resourseContainer)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                if (Children[i] != resourseContainer)
                    Children[i].IsFocus = false;
            }

            Draw();
        }

        private void ResourseContainerCountChanged(ResourseContainer resourseContainer) => Draw();
    }
}
