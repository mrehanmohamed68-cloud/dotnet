using System;

namespace day6_csharp.AbstractDemo
{
    // abstract class -> can't be instantiated directly (new Shape() is not allowed)
    internal abstract class Shape
    {
        // virtual -> HAS a default body, overriding it is OPTIONAL
        public virtual void Draw()
        {
            Console.WriteLine("Drawing Shape");
        }

        // abstract -> NO body at all here, every concrete derived class
        // is FORCED to implement it
        public abstract double CalculateArea();
    }
}
