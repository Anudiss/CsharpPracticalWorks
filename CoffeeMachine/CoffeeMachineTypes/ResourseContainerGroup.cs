using CoffeeMachine.Components;
using System.Linq;
using static Colorful.Console;

namespace CoffeeMachine.CoffeeMachineTypes
{
    public class ResourseContainerGroup : Component
    {
        public ResourseContainer[] ResourseContainers { get; }

        public ResourseContainerGroup(int x, int y, int width, params ResourseContainer[] resourseContainers) : base(x, y, width, resourseContainers.Length * 3 - resourseContainers.Length - 1)
        {
            ResourseContainers = resourseContainers;
            for (int i = 0; i < ResourseContainers.Length; i++)
            {
                ResourseContainers[i].X = X;
                ResourseContainers[i].Y = Y + i * 2;
                ResourseContainers[i].Width = Width;
                ResourseContainers[i].Height = 2;
                ResourseContainers[i].OnCountChanged += ResourseContainerCountChanged;
            }
        }

        public override void Draw()
        {
            for (int i = 0; i < ResourseContainers.Length; i++)
            {
                ResourseContainers[i].Draw();
                if (i == 0)
                    continue;
                CursorLeft = X;
                CursorTop = Y + i * 2;
                Write("├");
                CursorLeft = X + Width - 1;
                CursorTop = Y + i * 2;
                Write("┤");
            }
        }

        private void ResourseContainerCountChanged(ResourseContainer resourseContainer) => Draw();
    }
}
