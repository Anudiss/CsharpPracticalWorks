using CoffeeMachineResourse.Types;
using System;
using static Colorful.Console;

namespace CoffeeMachineResourse.Component
{
    public class ProgressBar : Component
    {
        private double _count;

        public double Count {
            get => _count;
            set => _count = Extensions.Clamp(0, Capacity, value);
        }
        public double Capacity { get; }

        public ProgressBar(int x, int y, int width, double count, double capacity) : base(new Box(x, y, width, 0))
        {
            Capacity = capacity;
            Count = count;
        }

        public override void Draw()
        {
            Clear();

            // ▏▎▍▌▋▊▉█
            //─│╭╮╰╯

            string fade = " ▏▎▍▌▋▊▉";
            double percent = Count / Capacity;
            double filled = percent * Box.Width;
            double fadePerPercent = fade.Length / 100.0;
            double delta = (filled - Math.Floor(filled)) * fadePerPercent;

            string progressBar = "█".Repeat((int)Math.Floor(filled)) + fade[(int)(delta * 100)];
            progressBar = progressBar.Substring(0, Math.Min(progressBar.Length, Box.Width));
            
            CursorLeft = Box.X;
            CursorTop = Box.Y;
            WriteStyled($"{progressBar.PadRight(Box.Width)}", IsFocus ? ComponentStyles.FocusProgressBar : ComponentStyles.Default);
        }
    }
}
