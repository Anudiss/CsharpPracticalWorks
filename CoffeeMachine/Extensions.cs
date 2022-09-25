using System;
using System.Linq;

namespace CoffeeMachine
{
    public static class Extensions
    {
        public static string Repeat(this string text, int count) => string.Join("", Enumerable.Repeat(text, count));
        public static string Repeat(this char symbol, int count) => string.Join("", Enumerable.Repeat(symbol, count));
        public static double Clamp(double lower, double upper, double value) => Math.Max(lower, Math.Min(upper, value));
    }
}
