using System;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeMachine
{
    public static class Extensions
    {
        public static string Repeat(this string text, int count) => string.Join("", Enumerable.Repeat(text, count));
        public static string Repeat(this char symbol, int count) => string.Join("", Enumerable.Repeat(symbol, count));
        public static double Clamp(double lower, double upper, double value) => Math.Max(lower, Math.Min(upper, value));
        public static int Clamp(int lower, int upper, int value) => Math.Max(lower, Math.Min(upper, value));
        public static IEnumerable<T> Slice<T>(this IEnumerable<T> array, int index, int count)
        {
            count = Clamp(0, array.Count() - 1, count);

            return array.Select((e, i) => (e, i))
                        .Where(e => e.i >= index && e.i < index + count)
                        .Select(e => e.e)
                        .ToArray();
        }
        public static bool IsInside(int lower, int upper, int value) => value >= lower && value <= upper;
        public static IEnumerable<T> Where<T>(this IEnumerable<T> enumerable, Func<T, int, bool> predicate)
        {
            return enumerable.Select((e, index) => (e, index)).Where(e => predicate(e.e, e.index)).Select(e => e.e);
        }
    }
}
