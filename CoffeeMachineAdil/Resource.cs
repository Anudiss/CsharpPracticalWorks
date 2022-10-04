namespace CoffeeMachineAdil
{
    public class Resourse
    {
        public string Name { get; }

        public Resourse(string name)
        {
            Name = name;
        }
    }

    public static class Resourses
    {
        public static Resourse Coffee = new Resourse("Кофе");
        public static Resourse Water = new Resourse("Вода");
        public static Resourse Milk = new Resourse("Молоко");
    }

}
