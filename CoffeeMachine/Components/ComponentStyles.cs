using Colorful;
using System.Drawing;

namespace CoffeeMachine.Components
{
    public static class ComponentStyles
    {
        public static StyleSheet Default = new StyleSheet(Color.Black),
            FocusProgressBar = new StyleSheet(Color.Black),
            FocusBorder = new StyleSheet(Color.Black),
            FocusProgressBarAndBorder = new StyleSheet(Color.Black),
            FocusText = new StyleSheet(Color.Black);

        static ComponentStyles()
        {
            FocusProgressBar.AddStyle("[▏▎▍▌▋▊▉█]", Color.FromArgb(125, 45, 45));
            FocusBorder.AddStyle("[─│╭╮╰╯├┤]", Color.FromArgb(227, 32, 32));

            FocusProgressBarAndBorder.AddStyle("[▏▎▍▌▋▊▉█─│╭╮╰╯├┤]", Color.FromArgb(125, 45, 45));

            FocusText.AddStyle("[\\w\\.]", Color.FromArgb(227, 32, 32));
        }
    }
}
