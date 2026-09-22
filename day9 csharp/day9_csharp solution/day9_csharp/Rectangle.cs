using System;

namespace day9_csharp
{
    public struct Rectangle
    {
        public double Length { get; set; }
        public double Width { get; set; }

        public Rectangle(double length, double width)
        {
            Length = length;
            Width = width;
        }

        public override string ToString() => $"Length={Length}, Width={Width}";
    }
}
