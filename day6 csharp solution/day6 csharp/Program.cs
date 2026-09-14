using day6_csharp.Inheritance;
using day6_csharp.Interfaces;
using day6_csharp.AbstractDemo;
using System;

namespace day6_csharp
{
    internal class Program
    {
        static void Main()
        {
            #region Part1 - Car : constructor overloading
            Car c1 = new Car();
            Console.WriteLine(c1); // Id: 0, Brand: Unknown, Price: 0

            Car c2 = new Car(1);
            Console.WriteLine(c2); // Id: 1, Brand: Unknown, Price: 0

            Car c3 = new Car(2, "BMW");
            Console.WriteLine(c3); // Id: 2, Brand: BMW, Price: 0

            Car c4 = new Car(3, "Mercedes", 1500000);
            Console.WriteLine(c4); // Id: 3, Brand: Mercedes, Price: 1500000
            Console.WriteLine();
            #endregion

            #region Part2 - Calculator : method overloading
            Calculator calc = new Calculator();
            Console.WriteLine(calc.Sum(2, 3));      // 5
            Console.WriteLine(calc.Sum(2, 3, 4));   // 9
            Console.WriteLine(calc.Sum(2.5, 3.5));  // 6
            Console.WriteLine();
            #endregion

            #region Part3+4+5 - Inheritance / ctor chaining / new vs override / ToString
            Parent p1 = new Parent(2, 3);
            Console.WriteLine(p1);            // (2,3)
            Console.WriteLine(p1.Product());  // 6
            Console.WriteLine(p1.Sum());      // 5

            Child ch1 = new Child(2, 3, 4);
            Console.WriteLine(ch1);              // (2,3,4)
            Console.WriteLine(ch1.Product());    // 24 -> Child's own "new" version
            Console.WriteLine(ch1.Sum());        // 9  -> Child's "override" version

            // key demo: "new" is resolved by REFERENCE type (compile-time / early binding)
            Parent pRef = ch1;
            Console.WriteLine(pRef.Product()); // 6 -> back to Parent's version! (hidden, not overridden)
            Console.WriteLine(pRef.Sum());     // 9 -> still Child's version (true override, late binding)

            // "override" is resolved by the ACTUAL OBJECT type (runtime / late binding)
            ChildOverrideProduct ch2 = new ChildOverrideProduct(2, 3, 4);
            Parent pRef2 = ch2;
            Console.WriteLine(pRef2.Product()); // 24 -> Child's version, even through a Parent reference
            Console.WriteLine();
            #endregion

            #region Part6+7 - Interfaces / get-only property / default implementation
            IShape rect = new Rectangle(4, 5);
            rect.Draw();                  // Drawing a Rectangle 4x5
            Console.WriteLine(rect.Area); // 20
            rect.PrintDetails();          // Shape area = 20 (used IShape's default impl)

            IShape circle = new Circle(3);
            circle.Draw();                // Drawing a Circle with radius 3
            circle.PrintDetails();        // Shape area = 28.274... (default impl, no code duplicated in Circle)
            Console.WriteLine();
            #endregion

            #region Part8 - IMovable via interface reference
            IMovable movableCar = new Car(5, "Toyota", 900000);
            movableCar.Move(); // Toyota is moving...
            Console.WriteLine();
            #endregion

            #region Part9 - implementing multiple interfaces
            MyFile file = new MyFile("report.docx");
            IReadable reader = file;
            IWritable writer = file;
            reader.Read();   // Reading from report.docx...
            writer.Write();  // Writing to report.docx...
            Console.WriteLine();
            #endregion

            #region Part10 - virtual vs abstract
            Shape s = new RectangleShape(6, 2);
            s.Draw();                              // Drawing Rectangle (overridden)
            Console.WriteLine(s.CalculateArea());  // 12
            #endregion
        }
    }
}
