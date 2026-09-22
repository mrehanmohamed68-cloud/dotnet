using System;

namespace day9_csharp
{
    // Value type: == is NOT implemented by default for structs, so
    // "circleA == circleB" won't even compile unless we overload it ourselves.
    // Equals (inherited from ValueType) DOES compare field data by default.
    public struct Circle
    {
        public double Radius { get; set; }
        public string Color { get; set; }

        public Circle(double radius, string color)
        {
            Radius = radius;
            Color = color;
        }

        public override string ToString() => $"Circle(Radius={Radius}, Color={Color})";
    }

    // Reference type version: == compiles by default, but compares
    // references (identity), not data — even with identical Radius/Color,
    // two different objects are "not equal" unless we override == and Equals.
    public class CircleClass
    {
        public double Radius { get; set; }
        public string Color { get; set; }

        // Needed so object-initializer syntax (new CircleClass { Radius = ..., Color = ... })
        // has a constructor to call before setting the properties.
        public CircleClass() { }

        public CircleClass(double radius, string color)
        {
            Radius = radius;
            Color = color;
        }

        public override string ToString() => $"CircleClass(Radius={Radius}, Color={Color})";
    }
}
