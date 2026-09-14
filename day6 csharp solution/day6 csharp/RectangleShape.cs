using System;

namespace day6_csharp.AbstractDemo
{
    // note: named "RectangleShape" (not "Rectangle") only to avoid a name
    // clash with Interfaces.Rectangle from Part6/7 in this same solution
    internal class RectangleShape : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public RectangleShape(double width, double height)
        {
            Width = width;
            Height = height;
        }

        // overriding the virtual method (optional, but we customize it here)
        public override void Draw()
        {
            Console.WriteLine("Drawing Rectangle");
        }

        // MUST implement this -> it was abstract in Shape (no default body existed)
        public override double CalculateArea()
        {
            return Width * Height;
        }
    }
}
