using System;

namespace day6_csharp.Interfaces
{
    internal class Rectangle : IShape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        // its OWN version of Area
        public double Area => Width * Height;

        // its OWN version of Draw()
        public void Draw()
        {
            Console.WriteLine($"Drawing a Rectangle {Width}x{Height}");
        }

        // PrintDetails() not written here on purpose ->
        // it will use IShape's default implementation automatically
    }
}
