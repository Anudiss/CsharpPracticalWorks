using static Colorful.Console;
using CoffeeMachine.CoffeeMachineTypes;

namespace CoffeeMachine.Components.SelectComponents
{
    public class SelectableComponent : Component
    {
        public Coffee Value { get; }

        public SelectableComponent(int x, int y, int width, Coffee value) : base(x, y, width, 2)
        {
            Value = value;
        }

        public override void Draw()
        {
            //─│╭╮╰╯
            Clear();

            var style = IsFocus ? ComponentStyles.FocusBorder : ComponentStyles.Default;

            CursorLeft = X;
            CursorTop = Y;
            WriteStyled($"╭{"─".Repeat(Width - 2)}╮", style);

            CursorLeft = X;
            CursorTop = Y + 1;
            WriteStyled($"│{$"{Value.Name.PadRight((Width - 2 + Value.Name.Length) / 2).PadLeft(Width - 2)}"}│", style);

            CursorLeft = X;
            CursorTop = Y + 2;
            WriteStyled($"╰{"─".Repeat(Width - 2)}╯", style);
        }
    }
}
