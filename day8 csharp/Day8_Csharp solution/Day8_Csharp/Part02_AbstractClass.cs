using System;

namespace Part02_AbstractClass
{
    // Abstract class: can hold shared state and shared (concrete) behavior,
    // plus members that subclasses MUST implement.
    public abstract class Shape
    {
        // Every shape has a name - shared field, not something each subclass
        // needs to redeclare.
        public string Name { get; set; }

        protected Shape(string name)
        {
            Name = name;
        }

        // Abstract method: no body here, every subclass must provide its own.
        public abstract double GetArea();

        // Non-abstract (concrete) method: shared implementation,
        // reused by every subclass without rewriting it.
        public void Display()
        {
            Console.WriteLine($"{Name} has an area of {GetArea():F2}");
        }
    }

    public class Rectangle : Shape
    {
        private double _width;
        private double _height;

        public Rectangle(double width, double height) : base("Rectangle")
        {
            _width = width;
            _height = height;
        }

        public override double GetArea()
        {
            return _width * _height;
        }
    }

    public class Circle : Shape
    {
        private double _radius;

        public Circle(double radius) : base("Circle")
        {
            _radius = radius;
        }

        public override double GetArea()
        {
            return Math.PI * _radius * _radius;
        }
    }

    // Purely for comparison: an interface-based version of the same idea.
    // Notice it CANNOT provide Display() with a body, and it cannot hold
    // a shared "Name" field - every implementer has to repeat that.
    public interface IShape
    {
        double GetArea();
    }

    public class InterfaceRectangle : IShape
    {
        private double _width, _height;
        public InterfaceRectangle(double w, double h) { _width = w; _height = h; }
        public double GetArea() => _width * _height;
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Shape[] shapes = new Shape[]
            {
                new Rectangle(4, 5),
                new Circle(3)
            };

            foreach (Shape shape in shapes)
            {
                // Display() is inherited, unchanged, from the abstract class.
                shape.Display();
            }

            Console.WriteLine();
            Console.WriteLine("-- Interface-based version --");
            IShape ishape = new InterfaceRectangle(4, 5);
            // With the interface, there is no Display() to call - we'd have
            // to write that logic again in every consumer, or add an
            // extension method, since interfaces (pre-C# 8) cannot carry
            // shared code or state.
            Console.WriteLine($"Area: {ishape.GetArea():F2}");
        }
    }
}

/*
QUESTION: When should you prefer an abstract class over an interface?

- When subclasses share common STATE (fields/properties) - like Shape.Name
  above - not just a common method signature. Interfaces can't hold instance
  fields.

- When you want to provide shared, ready-to-use IMPLEMENTATION (like
  Display() above) that every subclass gets for free, instead of every
  implementer rewriting the same logic.

- When the types being modeled have a genuine "is-a" relationship and a
  natural single base type (a Circle IS-A Shape), rather than just "can do X"
  (IComparable, IDisposable).

- When you need constructors, access modifiers other than public, or
  protected members - abstract classes support all of these; interfaces
  are more limited (though modern C# interfaces do allow default method
  bodies, they still can't have instance fields or constructors).

- Prefer an interface instead when you need multiple, unrelated capabilities
  on the same class (a class can implement many interfaces but inherit from
  only one abstract class), or when there's no shared implementation/state
  to factor out - just a contract.
*/
