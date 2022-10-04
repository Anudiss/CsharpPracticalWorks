using Colorful;
using System.Drawing;

namespace CoffeeMachineResourse.Component
{
    public static class ComponentStyles
    {
        public static StyleSheet Default, FocusText, FocusBorder, FocusProgressBar;

        static ComponentStyles()
        {
            Default = new StyleSheet(Color.Black);
            FocusText = new StyleSheet(Color.Black);
            FocusBorder = new StyleSheet(Color.Black);
            FocusProgressBar = new StyleSheet(Color.Black);

            FocusText.AddStyle("[\\w\\.]", Color.IndianRed);
            FocusBorder.AddStyle("[─│╭╮╰╯]", Color.IndianRed);
            FocusProgressBar.AddStyle("[▏▎▍▌▋▊▉█]", Color.IndianRed);
        }
    }
}
