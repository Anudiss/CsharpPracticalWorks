namespace CoffeeMachineAdir
{
    public class Coffee
    {
        public string Name { get; }
        public double Price { get; }

        public Coffee(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }
}
