using System;

namespace day9_csharp
{
    internal class Program
    {
        static void Main()
        {
            #region 1) Weekdays enum
            Console.WriteLine("---- 1) Weekdays ----");
            foreach (Weekdays day in Enum.GetValues(typeof(Weekdays)))
            {
                Console.WriteLine($"{day} = {(int)day}");
            }
            // Q: Why explicitly assign enum values?
            // - To match external/real-world values (DB codes, protocol values, day numbers).
            // - To keep values stable even if members are reordered or new ones inserted later.
            // - To leave gaps on purpose (e.g. flags enums use powers of 2).
            #endregion

            Console.WriteLine();

            #region 2) Grades enum (short, F = -1)
            Console.WriteLine("---- 2) Grades (short) ----");
            foreach (Grades g in Enum.GetValues(typeof(Grades)))
            {
                Console.WriteLine($"{g} = {(short)g}");
            }
            // Q: What happens if a value exceeds the underlying type's range?
            // - It's a compile-time error (constant value doesn't fit in the type),
            //   e.g. assigning 40000 to a "short" enum member won't compile.
            #endregion

            Console.WriteLine();

            #region 3) Person + Department
            Console.WriteLine("---- 3) Person ----");
            Person p1 = new Person("Ali", 23, "Networking");
            Person p2 = new Person("Sara", 25, "Cybersecurity");
            Console.WriteLine(p1);
            Console.WriteLine(p2);
            // Q: Purpose of virtual on a property?
            // - Lets a derived class override the getter/setter with its own logic
            //   (validation, formatting, computed value) while code that only knows
            //   about the base Person type still calls the derived version at
            //   runtime (late/dynamic binding) instead of the base version.
            #endregion

            Console.WriteLine();

            #region 4) Child.DisplaySalary (sealed Salary)
            Console.WriteLine("---- 4) Child sealed Salary ----");
            Child child = new Child();
            child.Salary = 5000; // goes through the sealed setter (+1000 bonus)
            child.DisplaySalary();
            // Q: Why can't you override a sealed property/method?
            // - "sealed" explicitly stops the override chain at that class, so the
            //   compiler rejects any further override attempt in a subclass.
            //   It's a design decision to lock in behavior and prevent it from
            //   being changed further down the inheritance hierarchy.
            #endregion

            Console.WriteLine();

            #region 5) Static Utility.CalcRectanglePerimeter
            Console.WriteLine("---- 5) Rectangle perimeter (static) ----");
            double perimeter = Utility.CalcRectanglePerimeter(4, 6); // no instance needed
            Console.WriteLine($"Perimeter: {perimeter}");
            // Q: Key difference between static and object (instance) members?
            // - Static members belong to the class itself: one shared copy, called
            //   via ClassName.Member, no object needed.
            // - Instance (object) members belong to each object separately: each
            //   object has its own copy of the data, called via objectRef.Member.
            #endregion

            Console.WriteLine();

            #region 6) ComplexNumber multiplication
            Console.WriteLine("---- 6) ComplexNumber * ----");
            ComplexNumber c1 = new ComplexNumber { Real = 2, Imag = 3 };
            ComplexNumber c2 = new ComplexNumber { Real = 4, Imag = 5 };
            ComplexNumber sum = c1 + c2;
            ComplexNumber product = c1 * c2;
            Console.WriteLine($"{c1} + {c2} = {sum}");
            Console.WriteLine($"{c1} * {c2} = {product}");
            // Q: Can you overload all operators in C#?
            // - No. Only a specific set is overloadable (+ - * / % == != < > <= >=
            //   ! ~ ++ -- & | ^ << >> true false, and conversion operators).
            //   Operators like =, &&, ||, ?:, and member access (.) cannot be
            //   overloaded directly (&& / || follow from true/false + & / |).
            #endregion

            Console.WriteLine();

            #region 7) Gender enum (byte) memory usage
            Console.WriteLine("---- 7) Gender (byte) ----");
            Console.WriteLine($"Gender underlying type size: {sizeof(Gender)} byte(s)");
            Console.WriteLine($"Default int enum size would be: {sizeof(int)} byte(s)");
            // Q: When should you consider changing an enum's underlying type?
            // - When you have a very large number of enum instances stored/sent
            //   over the wire (memory/bandwidth matters), or when the values must
            //   fit a specific contract/protocol size (e.g. a byte field in a
            //   binary format or database column).
            #endregion

            Console.WriteLine();

            #region 8) Static Utility temperature conversion
            Console.WriteLine("---- 8) Temperature conversion (static) ----");
            Console.WriteLine($"25C -> {Utility.CelsiusToFahrenheit(25)}F");
            Console.WriteLine($"77F -> {Utility.FahrenheitToCelsius(77)}C");
            // Q: Why can't a static class have instance constructors?
            // - A static class can never be instantiated (no "new"), so an
            //   instance constructor would be meaningless. The compiler only
            //   allows a parameterless static constructor, used once to
            //   initialize static members before first use.
            #endregion

            Console.WriteLine();

            #region 9) Enum.TryParse for Grades
            Console.WriteLine("---- 9) Enum.TryParse ----");
            string[] inputs = { "A", "F", "Z" };
            foreach (string input in inputs)
            {
                if (Enum.TryParse<Grades>(input, out Grades parsedGrade))
                    Console.WriteLine($"'{input}' -> {parsedGrade} ({(short)parsedGrade})");
                else
                    Console.WriteLine($"'{input}' is not a valid Grade");
            }
            // Q: Advantages of Enum.TryParse over direct parsing?
            // - Doesn't throw an exception on invalid input; returns false instead.
            // - No need for try/catch around parsing logic -> cleaner, faster code.
            // - Returns a strongly typed enum value directly via the out parameter.
            #endregion

            Console.WriteLine();

            #region 10) Employee.Equals + Helper2<Employee>.SearchArray
            Console.WriteLine("---- 10) Employee Equals + SearchArray ----");
            Department it = new Department(1, "IT");
            Employee[] employees =
            {
                new Employee(1, "Ali", 6000, it),
                new Employee(2, "Sara", 7000, it),
                new Employee(3, "Omar", 5500, it)
            };
            Employee target = new Employee(2, "Sara", 0, null); // same Id+Name -> "equal" by our rule
            int index = Helper2<Employee>.SearchArray(employees, target);
            Console.WriteLine(index >= 0
                ? $"Found at index {index}: {employees[index]}"
                : "Not found");
            // Q: Difference between overriding Equals and == for struct vs class?
            // - class: default Equals AND default == both compare references
            //   (identity). Overriding Equals changes only Equals's behavior;
            //   == still compares references unless you also overload operator ==.
            // - struct: default Equals (from ValueType) compares field DATA via
            //   reflection; == is NOT implemented by default at all (won't
            //   compile) unless you overload it yourself.
            // Q: Why is overriding ToString beneficial?
            // - Gives a meaningful, readable string ("Emp Id=2, Name=Sara...")
            //   instead of the default "Namespace.Employee", which is invaluable
            //   for logging, debugging, and Console.WriteLine output.
            #endregion

            Console.WriteLine();

            #region 11) Helper.Max<T> (constrained generic)
            Console.WriteLine("---- 11) Helper.Max<T> ----");
            Console.WriteLine(Helper.Max(5, 9));
            Console.WriteLine(Helper.Max(3.5, 2.1));
            Console.WriteLine(Helper.Max("apple", "banana"));
            // Q: Can generics be constrained to specific types? Example:
            // public static T Max<T>(T a, T b) where T : IComparable<T>
            // - "where T : IComparable<T>" guarantees T has CompareTo, so Max
            //   can compare any T (int, double, string, custom types) safely.
            //   Other constraints: where T : class / struct / new() / BaseClass.
            #endregion

            Console.WriteLine();

            #region 12) Helper2<T>.ReplaceArray
            Console.WriteLine("---- 12) Helper2<T>.ReplaceArray ----");
            int[] numbers = { 1, 2, 3, 2, 5 };
            Helper2<int>.ReplaceArray(numbers, 2, 99);
            Console.WriteLine(string.Join(", ", numbers));

            string[] words = { "cat", "dog", "cat", "bird" };
            Helper2<string>.ReplaceArray(words, "cat", "lion");
            Console.WriteLine(string.Join(", ", words));
            // Q: Key differences between generic methods and generic classes?
            // - Generic method: only the method is generic (Method<T>(...)); each
            //   call can use a different T, defined inside a normal (or generic) class.
            // - Generic class: the WHOLE class is parameterized by T
            //   (class Helper2<T> {...}); T is fixed for the entire instance/usage
            //   of that class, and typically declared once (Helper2<int>).
            #endregion

            Console.WriteLine();

            #region 13) Rectangle non-generic Swap
            Console.WriteLine("---- 13) Rectangle Swap ----");
            Rectangle r1 = new Rectangle(10, 5);
            Rectangle r2 = new Rectangle(20, 8);
            Console.WriteLine($"Before: r1=({r1}), r2=({r2})");
            Utility.Swap(ref r1, ref r2);
            Console.WriteLine($"After : r1=({r1}), r2=({r2})");
            // Q: Why prefer a generic swap over a custom method per type?
            // - One generic Swap<T> works for int, double, Rectangle, or any type,
            //   instead of writing/maintaining a near-identical overload for each
            //   type -> less code duplication, easier maintenance, more scalable.
            #endregion

            Console.WriteLine();

            #region 14) Department property on Employee + search by department
            Console.WriteLine("---- 14) Search employees by Department ----");
            Department hr = new Department(2, "HR");
            Employee[] staff =
            {
                new Employee(10, "Mona", 6500, it),
                new Employee(11, "Karim", 7200, hr)
            };
            Employee searchTarget = new Employee(11, "Karim", 0, hr);
            int foundIndex = Helper2<Employee>.SearchArray(staff, searchTarget);
            Console.WriteLine(foundIndex >= 0
                ? $"Found: {staff[foundIndex]}"
                : "Not found");
            // Q: How does overriding Equals on Department improve search accuracy?
            // - Employee.Equals only compares Id/Name here; if we compared
            //   Department too, Department needs its own Equals (done above:
            //   Id + Name) so two Department objects with the same data compare
            //   equal instead of failing due to reference inequality. That lets
            //   searches match "same department" correctly instead of missing
            //   valid matches whose Department is a different object instance.
            #endregion

            Console.WriteLine();

            #region 15) Circle struct vs Circle class: == and Equals
            Console.WriteLine("---- 15) Circle (struct) vs CircleClass (reference) ----");
            Circle circA = new Circle(5, "Red");
            Circle circB = new Circle(5, "Red");
            // circA == circB;  // <-- would NOT compile: struct has no default ==
            Console.WriteLine($"Circle struct Equals (data compare): {circA.Equals(circB)}");

            CircleClass circRefA = new CircleClass { Radius = 5, Color = "Red" };
            CircleClass circRefB = new CircleClass { Radius = 5, Color = "Red" };
            Console.WriteLine($"CircleClass == (reference compare): {circRefA == circRefB}");       // false
            Console.WriteLine($"CircleClass Equals (default, still reference): {circRefA.Equals(circRefB)}"); // false
            // Q: Why is == not implemented by default for structs?
            // - The compiler has no generic way to know which fields matter for
            //   "equality" on every possible struct, so it doesn't generate ==
            //   automatically (unlike Equals, which uses reflection over all
            //   fields as a safe default). You must overload == (and !=)
            //   yourself if you want struct instances comparable with ==.
            #endregion

            Console.WriteLine("\nDone.");
        }
    }
}
