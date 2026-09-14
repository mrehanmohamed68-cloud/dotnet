using System;

namespace day6_csharp.Interfaces
{
    internal class Circle : IShape
    {
        public double Radius { get; set; }

        public Circle(double radius)
        {
            Radius = radius;
        }

        public double Area => Math.PI * Radius * Radius;

        public void Draw()
        {
            Console.WriteLine($"Drawing a Circle with radius {Radius}");
        }

        // Part7: Circle does NOT implement PrintDetails() itself,
        // so calling it uses IShape's default implementation directly
    }
}
