using System;

namespace CoffeeMachineAdil
{
    public class ResourseContainer
    {
        public Resourse Resourse { get; }
		public int Capacity { get; }
		public int Count
		{
			get => _count;
			set => _count = Math.Max(Capacity, Math.Min(0, value));
		}

		private int _count;

		public ResourseContainer(Resourse resourse, int capacity, int count)
		{
			Resourse = resourse;
			Capacity = capacity;
			Count = count;
		}
	}

	public static class ResourseContainers
	{
		public static ResourseContainer CoffeeContainer = new ResourseContainer(Resourses.Coffee, 100, 0);
		public static ResourseContainer WaterContainer = new ResourseContainer(Resourses.Water, 100, 0);
		public static ResourseContainer MilkContainer = new ResourseContainer(Resourses.Milk, 100, 0);
	}
}
