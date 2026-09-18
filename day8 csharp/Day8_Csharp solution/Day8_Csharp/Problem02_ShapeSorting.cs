using System;

namespace Problem02_ShapeSorting
{
    public class Shape : IComparable<Shape>
    {
        public string Name { get; set; }
        public double Area { get; set; }

        public Shape(string name, double area)
        {
            Name = name;
            Area = area;
        }

        // Natural ordering: by Area, ascending.
        public int CompareTo(Shape other)
        {
            if (other == null) return 1;
            return this.Area.CompareTo(other.Area);
        }

        public override string ToString()
        {
            return $"{Name,-10} Area = {Area:F2}";
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Shape[] shapes = new Shape[]
            {
                new Shape("Square", 16.0),      // 4x4
                new Shape("Circle", 78.54),      // radius 5
                new Shape("Rectangle", 24.0),    // 4x6
                new Shape("Square", 4.0),        // 2x2
                new Shape("Circle", 12.57),       // radius 2
                new Shape("Rectangle", 50.0)     // 5x10
            };

            Console.WriteLine("Before sorting:");
            foreach (var s in shapes) Console.WriteLine(s);

            // Array.Sort uses Shape.CompareTo because Shape implements
            // IComparable<Shape> - no custom comparer needed.
            Array.Sort(shapes);

            Console.WriteLine("\nAfter sorting by Area (ascending):");
            foreach (var s in shapes) Console.WriteLine(s);
        }
    }
}
