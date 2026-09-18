using System;

namespace Problem03_ShapeHierarchy
{
    public abstract class GeometricShape
    {
        public double Dimension1 { get; set; }
        public double Dimension2 { get; set; }

        protected GeometricShape(double dimension1, double dimension2)
        {
            Dimension1 = dimension1;
            Dimension2 = dimension2;
        }

        public abstract double CalculateArea();

        public abstract double Perimeter { get; }
    }

    public class Triangle : GeometricShape
    {
        // Dimension1 = base, Dimension2 = height.
        public Triangle(double baseLength, double height) : base(baseLength, height)
        {
        }

        public override double CalculateArea()
        {
            return 0.5 * Dimension1 * Dimension2;
        }

        // NOTE: a triangle's true perimeter needs all three side lengths,
        // but we're only given base and height. To still provide a
        // meaningful Perimeter, we treat Dimension1/Dimension2 as the two
        // legs of a RIGHT triangle and derive the hypotenuse with the
        // Pythagorean theorem. This is a simplifying assumption, not a
        // general formula for any triangle.
        public override double Perimeter
        {
            get
            {
                double hypotenuse = Math.Sqrt(Dimension1 * Dimension1 + Dimension2 * Dimension2);
                return Dimension1 + Dimension2 + hypotenuse;
            }
        }
    }

    public class Rectangle : GeometricShape
    {
        // Dimension1 = width, Dimension2 = height.
        public Rectangle(double width, double height) : base(width, height)
        {
        }

        public override double CalculateArea()
        {
            return Dimension1 * Dimension2;
        }

        public override double Perimeter => 2 * (Dimension1 + Dimension2);
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            GeometricShape[] shapes = new GeometricShape[]
            {
                new Triangle(6, 4),
                new Rectangle(5, 8)
            };

            // The loop only knows about GeometricShape - abstraction in
            // action, exactly as discussed in the questions above.
            foreach (GeometricShape shape in shapes)
            {
                Console.WriteLine($"{shape.GetType().Name}: Area = {shape.CalculateArea():F2}, Perimeter = {shape.Perimeter:F2}");
            }
        }
    }
}
