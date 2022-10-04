using CoffeeMachineResourse.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeMachineResourse
{
    public static class Extensions
    {
        public static string Repeat(this string text, int count) => string.Join("", Enumerable.Repeat(text, count));
        public static int Clamp(int lower, int upper, int value) => Math.Max(lower, Math.Min(upper, value));
        public static double Clamp(double lower, double upper, double value) => Math.Max(lower, Math.Min(upper, value));
        public static bool IsInside(int lower, int upper, int value) => lower <= value && upper >= value;
        public static IEnumerable<T> Slice<T>(this IEnumerable<T> enumerable, int index, int count)
        {
            return enumerable.Select((e, i) => (e, i))
                             .Where(e => e.i >= index && e.i < index + count)
                             .Select(e => e.e);
        }
        public static Coffee Clone(this Coffee coffee) => new Coffee(coffee.Name, coffee.Recipe.Select(keyPair => (keyPair.Key, keyPair.Value)).ToArray());
    }
}
