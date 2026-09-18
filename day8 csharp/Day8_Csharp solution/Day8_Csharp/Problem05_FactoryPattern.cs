using System;

namespace Problem05_FactoryPattern
{
    // Same hierarchy as Problem03_ShapeHierarchy, reproduced here so this
    // project is self-contained and independently runnable.
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
        public Triangle(double baseLength, double height) : base(baseLength, height) { }

        public override double CalculateArea() => 0.5 * Dimension1 * Dimension2;

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
        public Rectangle(double width, double height) : base(width, height) { }

        public override double CalculateArea() => Dimension1 * Dimension2;

        public override double Perimeter => 2 * (Dimension1 + Dimension2);
    }

    // The factory hides object CREATION behind one method. Calling code
    // asks for a shape by name and gets back a GeometricShape - it never
    // needs to know the concrete constructor it's calling, or even that
    // Triangle/Rectangle exist as types.
    public static class ShapeFactory
    {
        public static GeometricShape CreateShape(string shapeType, double dim1, double dim2)
        {
            switch (shapeType.Trim().ToLowerInvariant())
            {
                case "triangle":
                    return new Triangle(dim1, dim2);
                case "rectangle":
                    return new Rectangle(dim1, dim2);
                default:
                    throw new ArgumentException($"Unknown shape type: '{shapeType}'");
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            GeometricShape rectangle = ShapeFactory.CreateShape("Rectangle", 5, 8);
            GeometricShape triangle = ShapeFactory.CreateShape("Triangle", 6, 4);

            foreach (var shape in new[] { rectangle, triangle })
            {
                Console.WriteLine($"{shape.GetType().Name}: Area = {shape.CalculateArea():F2}, Perimeter = {shape.Perimeter:F2}");
            }

            try
            {
                ShapeFactory.CreateShape("Hexagon", 1, 1);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
