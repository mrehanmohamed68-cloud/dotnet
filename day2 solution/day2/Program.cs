using day2;
using System;


Problem1_Comments();
Problem3_FixErrors();
Problem5_VariablesNaming();
Problem7_ReferenceType();
Problem9_ArithmeticOperators();
Problem10_ModuloQuestion();
Problem11_GreaterThanTenAndEven();
Problem13_Casting();
Problem15_ParseAge();
Problem16_PrefixPostfix();

Console.WriteLine("\n=== End of Assignment ===");

// ---------------- Problem 1 ----------------
void Problem1_Comments()
{
    Console.WriteLine("---- Problem 1: Comments ----");

    /* This block calculates the sum of two integers (x and y)
       and prints the result to the console. */
    int x = 10; // first number
    int y = 20; // second number
    int sum = x + y; // add x and y together
    Console.WriteLine(sum); // display the result (30)
}

// ---------------- Problem 3 ----------------
// Original broken code:
//   int x = "10";
//   console.WriteLine(x + y);
// Errors: 1) "10" is a string not an int  2) "console" must be "Console"  3) y not declared
//
// Q: Runtime error vs Logical error
// - Runtime error: code compiles fine but crashes while running (e.g. 10/0 -> DivideByZeroException)
// - Logical error: code runs without crashing but gives a wrong result
//   (e.g. writing "x - y" when you meant "x + y")
void Problem3_FixErrors()
{
    Console.WriteLine("\n---- Problem 3: Fixed Errors ----");
    int x = 10;
    int y = 20;
    Console.WriteLine(x + y);
}

// ---------------- Problem 5 ----------------
// Q: Why follow naming conventions like PascalCase in C#?
// - Improves readability/consistency, matches .NET standard, helps identify
//   identifier purpose at a glance, avoids analyzer warnings.
void Problem5_VariablesNaming()
{
    Console.WriteLine("\n---- Problem 5: Variable Naming ----");
    string fullName = "Ahmed Ali";
    int age = 22;
    decimal monthlySalary = 15000.50M;
    bool isStudent = true;

    Console.WriteLine($"Full Name: {fullName}");
    Console.WriteLine($"Age: {age}");
    Console.WriteLine($"Monthly Salary: {monthlySalary}");
    Console.WriteLine($"Is Student: {isStudent}");
}

// ---------------- Problem 7 ----------------
// Q: Value type vs Reference type memory allocation
// - Value types store the actual data, allocated on the stack, copied by value.
// - Reference types store an address to the data on the heap; copying the
//   variable copies the reference, so both variables point to the same object.
void Problem7_ReferenceType()
{
    Console.WriteLine("\n---- Problem 7: Reference Type Behavior ----");

    Point p1 = new Point();
    p1.X = 5;
    p1.Y = 5;

    Point p2 = p1; // p2 points to the SAME object as p1

    Console.WriteLine($"Before change -> p1.X = {p1.X}, p2.X = {p2.X}");

    p2.X = 100;

    Console.WriteLine($"After change  -> p1.X = {p1.X}, p2.X = {p2.X}");
}

// ---------------- Problem 9 ----------------
void Problem9_ArithmeticOperators()
{
    Console.WriteLine("\n---- Problem 9: Arithmetic Operators ----");
    int x = 15, y = 4;
    Console.WriteLine($"Sum: {x + y}");
    Console.WriteLine($"Difference: {x - y}");
    Console.WriteLine($"Product: {x * y}");
    Console.WriteLine($"Division: {x / y}");
    Console.WriteLine($"Remainder: {x % y}");
}

// ---------------- Problem 10 ----------------
// Q: a=2, b=7 -> a % b = 2, because 7 doesn't fit into 2 at all,
// so the whole 2 remains as the remainder.
void Problem10_ModuloQuestion()
{
    Console.WriteLine("\n---- Problem 10: a % b ----");
    int a = 2, b = 7;
    Console.WriteLine(a % b);
}

// ---------------- Problem 11 ----------------
// Q: && (logical AND) vs & (bitwise AND)
// - && works on bools and short-circuits (stops if left side is false).
// - & works bit by bit and always evaluates both sides.
void Problem11_GreaterThanTenAndEven()
{
    Console.WriteLine("\n---- Problem 11: > 10 and Even ----");
    int number = 24;

    bool isGreaterThanTen = number > 10;
    bool isEven = number % 2 == 0;

    if (isGreaterThanTen && isEven)
        Console.WriteLine($"{number} is greater than 10 AND even.");
    else
        Console.WriteLine($"{number} does not satisfy both conditions.");
}

// ---------------- Problem 13 ----------------
// Q: Why is explicit casting required for double -> int?
// Because it's a narrowing conversion that can lose the fractional part
// (data loss), so C# forces you to confirm it explicitly.
void Problem13_Casting()
{
    Console.WriteLine("\n---- Problem 13: Casting double <-> int ----");

    Console.Write("Enter a decimal number: ");
    double userInput = double.Parse(Console.ReadLine());

    int wholeNumber = 50;
    double implicitResult = wholeNumber; // implicit: int -> double
    Console.WriteLine($"Implicit (int -> double): {implicitResult}");

    int explicitResult = (int)userInput; // explicit: double -> int
    Console.WriteLine($"Explicit (double -> int): {explicitResult}");
}

// ---------------- Problem 15 ----------------
// Q: Invalid Parse input throws FormatException.
// Handle it with try/catch, or better, use int.TryParse.
void Problem15_ParseAge()
{
    Console.WriteLine("\n---- Problem 15: Parse Age ----");
    Console.Write("Enter your age: ");
    string input = Console.ReadLine();

    try
    {
        int age = int.Parse(input);
        if (age > 0)
            Console.WriteLine($"Valid age: {age}");
        else
            Console.WriteLine("Age must be greater than 0.");
    }
    catch (FormatException)
    {
        Console.WriteLine("Invalid input: please enter numeric digits only.");
    }
}

// ---------------- Problem 16 ----------------
// x=5; y = ++x + x++;
// ++x -> x becomes 6, uses 6
// x++ -> uses current x (6), then x becomes 7
// y = 6 + 6 = 12, final x = 7
void Problem16_PrefixPostfix()
{
    Console.WriteLine("\n---- Problem 16: Prefix vs Postfix ----");
    int x = 5;
    int y = ++x + x++;
    Console.WriteLine($"y = {y}");
    Console.WriteLine($"x = {x}");
}