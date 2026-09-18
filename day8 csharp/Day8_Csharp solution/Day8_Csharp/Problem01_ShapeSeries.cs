using System;

namespace Problem01_ShapeSeries
{
    public interface IShapeSeries
    {
        int CurrentShapeArea { get; set; }
        void GetNextArea();
        void ResetSeries();
    }

    // Areas of squares with side lengths 1, 2, 3, ...  -> 1, 4, 9, 16, ...
    public class SquareSeries : IShapeSeries
    {
        private int _sideLength = 0;

        public int CurrentShapeArea { get; set; }

        public void GetNextArea()
        {
            _sideLength++;
            CurrentShapeArea = _sideLength * _sideLength;
        }

        public void ResetSeries()
        {
            _sideLength = 0;
            CurrentShapeArea = 0;
        }
    }

    // Areas of circles with radius 1, 2, 3, ...  -> pi*1^2, pi*2^2, ...
    // The interface requires an int area, so we round the double result.
    public class CircleSeries : IShapeSeries
    {
        private int _radius = 0;

        public int CurrentShapeArea { get; set; }

        public void GetNextArea()
        {
            _radius++;
            double exactArea = Math.PI * _radius * _radius;
            CurrentShapeArea = (int)Math.Round(exactArea);
        }

        public void ResetSeries()
        {
            _radius = 0;
            CurrentShapeArea = 0;
        }
    }

    public class Program
    {
        // Works with ANY IShapeSeries - it never mentions SquareSeries or
        // CircleSeries by name. This is "coding against the interface."
        public static void PrintTenShapes(IShapeSeries series)
        {
            series.ResetSeries();
            for (int i = 1; i <= 10; i++)
            {
                series.GetNextArea();
                Console.WriteLine($"Shape {i}: Area = {series.CurrentShapeArea}");
            }
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("-- Square series --");
            PrintTenShapes(new SquareSeries());

            Console.WriteLine("\n-- Circle series --");
            PrintTenShapes(new CircleSeries());
        }
    }
}
