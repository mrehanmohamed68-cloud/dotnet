using System;

namespace day9_csharp
{
    public static class Utility
    {
        // Static method: called through the class itself, no instance needed.
        public static double CalcRectanglePerimeter(double length, double width)
        {
            return 2 * (length + width);
        }

        public static double CelsiusToFahrenheit(double celsius)
        {
            return (celsius * 9.0 / 5.0) + 32;
        }

        public static double FahrenheitToCelsius(double fahrenheit)
        {
            return (fahrenheit - 32) * 5.0 / 9.0;
        }

        // Non-generic Swap written specifically for the Rectangle struct.
        public static void Swap(ref Rectangle a, ref Rectangle b)
        {
            Rectangle temp = a;
            a = b;
            b = temp;
        }
    }
}
