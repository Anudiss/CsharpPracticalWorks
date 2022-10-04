using CoffeeMachine.CoffeeMachineTypes;
using CoffeeMachine.Interface;
using System;
using static Colorful.Console;

namespace CoffeeMachine.Components.ResourseComponents
{
    public class ResourseContainer : Component, IChild
    {
        public Resourse Resourse { get; }
        public double Capacity { get; }
        public double Count
        {
            get => _count;
            set
            {
                _count = Extensions.Clamp(0, Capacity, value);
                OnCountChanged?.Invoke(this);
            }
        }

        public event Action<ResourseContainer> OnCountChanged;

        private double _count;

        public ResourseContainer(Resourse resourse, double count, double capacity)
            : base(0, 0, 0, 0)
        {
            Resourse = resourse;
            Capacity = capacity;
            Count = count;
        }

        public override void Draw()
        {
            Clear();

            // ▏▎▍▌▋▊▉█
            //─│╭╮╰╯

            CursorLeft = X;
            CursorTop = Y;
            string topLine = $"{Resourse.Name} {Count}/{Capacity}";
            Write($"╭{$"{topLine.Substring(0, Math.Min(topLine.Length, Width - 2))}{"─".Repeat(Math.Max(Width - topLine.Length - 2, 0))}"}╮");
            string fade = " ▏▎▍▌▋▊▉";
            double percent = Count / Capacity;
            double filled = percent * (Width - 2);
            double fadePerPercent = fade.Length / 100.0;
            double delta = (filled - Math.Floor(filled)) * fadePerPercent;

            string progressBar = "█".Repeat((int)Math.Floor(filled)) + fade[(int)(delta * 100)];
            progressBar = progressBar.Substring(0, Math.Min(progressBar.Length, Width - 2));
            for (int y = Y + 1; y < Y + Height; y++)
            {
                CursorLeft = X;
                CursorTop = y;
                WriteStyled($"│{progressBar.PadRight(Width - 2)}│", IsFocus ? ComponentStyles.FocusProgressBar : ComponentStyles.Default);
            }

            CursorLeft = X;
            CursorTop = Y + Height;
            Write($"╰{"─".Repeat(Width - 2)}╯");
        }
    }
}
