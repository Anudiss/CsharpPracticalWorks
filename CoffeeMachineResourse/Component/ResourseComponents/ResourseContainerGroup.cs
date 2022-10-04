using CoffeeMachineResourse.IO;
using static CoffeeMachineResourse.IO.Control;
using CoffeeMachineResourse.Types;
using System.Collections.Generic;
using static Colorful.Console;
using System.Linq;

namespace CoffeeMachineResourse.Component.ResourseComponents
{
    public class ResourseContainerGroup : Component
    {
        public readonly Dictionary<Resourse, ProgressBar> ResourseContainers = new Dictionary<Resourse, ProgressBar>();

        private Resourse _focusedResourseContainer;

        public ResourseContainerGroup(int x, int y, int width, params (Resourse, int, int)[] progressBars)
            : base(new Box(x, y, width, progressBars.Length * 2))
        {
            int i = 0;
            foreach ((Resourse resourse, int count, int capacity) in progressBars)
            {
                ProgressBar progressBar = new ProgressBar(x + 1, y + i++ * 2 + 1, width - 2, count, capacity);
                progressBar.OnComponentFocused += ProgressBarFocused;
                ResourseContainers.Add(key: resourse,
                                  value: progressBar);
            }

            ResourseContainers.First().Value.IsFocus = true;

            Keyboard.OnKeyPressed += Control;
        }

        public override void Focused()
        {
            base.Focused();
            ResourseContainers[_focusedResourseContainer].IsFocus = true;
            Draw();
        }

        public override void Unfocused()
        {
            base.Unfocused();
            ResourseContainers[_focusedResourseContainer].IsFocus = false;
            Draw();
        }

        private void Control(KeyInfo keyInfo)
        {
            if (IsFocus == false)
                return;

            if (keyInfo == ControlKeys[ControlCommand.Increment])
                ResourseContainers[_focusedResourseContainer].Count += 1;
            else if (keyInfo == ControlKeys[ControlCommand.Decrement])
                ResourseContainers[_focusedResourseContainer].Count -= 1;
            else if (keyInfo == ControlKeys[ControlCommand.HugeIncrement])
                ResourseContainers[_focusedResourseContainer].Count += 10;
            else if (keyInfo == ControlKeys[ControlCommand.HugeDecrement])
                ResourseContainers[_focusedResourseContainer].Count -= 10;
            else if (keyInfo == ControlKeys[ControlCommand.MoveDown])
                FocusNextContainer();
            else if (keyInfo == ControlKeys[ControlCommand.MoveUp])
                FocusPreviousContainer();

            Draw();
        }

        public ProgressBar this[Resourse resourse] => ResourseContainers[resourse];

        public override void Draw()
        {
            Clear();
            //─│╭╮╰╯├┤
            Box.Draw();
            
            int i = 0;
            foreach (var resourseContainer in ResourseContainers)
            {
                CursorLeft = Box.X;
                CursorTop = Box.Y + i++ * 2;
                if (i == 1)
                    Write($"╭─{$"{resourseContainer.Key.Name} {resourseContainer.Value.Count:F2}/{resourseContainer.Value.Capacity:F2}".PadRight(Box.Width - 3, '─')}╮");
                else
                    Write($"├─{$"{resourseContainer.Key.Name} {resourseContainer.Value.Count:F2}/{resourseContainer.Value.Capacity:F2}".PadRight(Box.Width - 3, '─')}┤");

                resourseContainer.Value.Draw();
            }
        }

        private void FocusNextContainer()
        {
            int i = Extensions.Clamp(0, ResourseContainers.Count - 1, ResourseContainers.Keys.Select((key, index) => (key, index)).First(e => e.key == _focusedResourseContainer).index + 1);
            ResourseContainers[ResourseContainers.Keys.ToArray()[i]].IsFocus = true;
        }

        private void FocusPreviousContainer()
        {
            int i = Extensions.Clamp(0, ResourseContainers.Count - 1, ResourseContainers.Keys.Select((key, index) => (key, index)).First(e => e.key == _focusedResourseContainer).index - 1);
            ResourseContainers[ResourseContainers.Keys.ToArray()[i]].IsFocus = true;
        }

        private void ProgressBarFocused(Component component)
        {
            foreach (var resourseContainer in ResourseContainers)
                if (resourseContainer.Value != component)
                    resourseContainer.Value.IsFocus = false;
            _focusedResourseContainer = ResourseContainers.First(e => e.Value == (ProgressBar)component).Key;
            Draw();
        }
    }
}
