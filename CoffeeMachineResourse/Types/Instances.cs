using System;
using System.Linq;
using System.Reflection;

namespace CoffeeMachineResourse.Types
{
    public static class Instances
    {
        #region Resourses
        public static Resourse Coffee = new Resourse("Кофе");
        public static Resourse Water = new Resourse("Вода");
        public static Resourse Milk = new Resourse("Молоко");
        #endregion
        #region Resourse Containers Data
        public static (Resourse, int count, int capacity) CoffeeContainer = (Coffee, count: 399, capacity: 400);
        public static (Resourse, int count, int capacity) WaterContainer = (Water, count: 1980, capacity: 2000);
        public static (Resourse, int count, int capacity) MilkContainer = (Milk, count: 1000, capacity: 1000);
        #endregion
        #region Coffee
        public static Coffee Espresso = new Coffee("Эспрессо",
                                                   (Coffee, 7),
                                                   (Water, 30));
        public static Coffee Americano = new Coffee("Американо",
                                                   (Espresso, 1),
                                                   (Water, 60));
        public static Coffee Latte = new Coffee("Латте",
                                                   (Espresso, 2),
                                                   (Milk, 100));
        public static Coffee Cappuccino = new Coffee("Капучино",
                                                   (Espresso, 2),
                                                   (Milk, 60));
        public static Coffee Lungo = new Coffee("Лунго",
                                                   (Coffee, 7),
                                                   (Water, 60));
        public static Coffee Macchiato = new Coffee("Макиато",
                                                   (Espresso, 1),
                                                   (Milk, 15));
        public static Coffee Ristretto = new Coffee("Ристретто",
                                                   (Coffee, 7),
                                                   (Water, 15));
        public static Coffee Doppio = new Coffee("Доппио",
                                                   (Espresso, 2));
        public static Coffee Trippio = new Coffee("Триппио",
                                                   (Espresso, 3));

        #endregion

        public static T[] GetTypes<T>(Type type)
        {
            FieldInfo[] fields = typeof(Instances).GetFields();
            return fields.Where(field => field.IsStatic && field.IsPublic && field.GetValue(null).GetType() == type)
                         .Select(field => (T)field.GetValue(null))
                         .ToArray();
        }
    }
}
