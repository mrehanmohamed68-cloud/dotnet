using System;

namespace day5_csharp
{
    internal partial class Program
    {
        static void Main()
        {
            bool exitApp = false;

            while (!exitApp)
            {
                Console.WriteLine();
                Console.WriteLine("#########################################");
                Console.WriteLine(" Day 05 - Main Menu");
                Console.WriteLine("#########################################");
                Console.WriteLine(" 1 - Part 01 exercises");
                Console.WriteLine(" 2 - Part 02 exercises");
                Console.WriteLine(" 0 - Exit");
                Console.Write("Choose a part: ");

                string partChoice = Console.ReadLine();
                Console.WriteLine();

                switch (partChoice)
                {
                    case "1": RunPart01Menu(); break;
                    case "2": RunPart02Menu(); break;
                    case "0": exitApp = true; break;
                    default: Console.WriteLine("Invalid option."); break;
                }
            }
        }

        #region Part 01 menu
        private static void RunPart01Menu()
        {
            bool back = false;

            while (!back)
            {
                Console.WriteLine();
                Console.WriteLine("=========================================");
                Console.WriteLine(" Part 01 - Exercises Menu");
                Console.WriteLine("=========================================");
                Console.WriteLine(" 1  - Divide two numbers (try/catch/finally)");
                Console.WriteLine(" 2  - Defensive input (positive X, Y > 1)");
                Console.WriteLine(" 3  - Nullable int (??, HasValue, Value)");
                Console.WriteLine(" 4  - Array bounds (IndexOutOfRangeException)");
                Console.WriteLine(" 5  - 3x3 array: row/column sums");
                Console.WriteLine(" 6  - Jagged array (3 rows, varying sizes)");
                Console.WriteLine(" 7  - Nullable reference types (string?, !)");
                Console.WriteLine(" 8  - Boxing / Unboxing");
                Console.WriteLine(" 9  - SumAndMultiply (out parameters)");
                Console.WriteLine(" 10 - PrintMessage (optional + named parameters)");
                Console.WriteLine(" 11 - Nullable array + null propagation (?.)");
                Console.WriteLine(" 12 - Day of week -> number (switch expression)");
                Console.WriteLine(" 13 - SumArray (params keyword)");
                Console.WriteLine(" 0  - Back to main menu");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": DivideNumbers(); break;
                    case "2": TestDefensiveCode(); break;
                    case "3": NullableIntDemo(); break;
                    case "4": ArrayBoundsDemo(); break;
                    case "5": Rectangular3x3ArrayDemo(); break;
                    case "6": JaggedArrayDemo(); break;
                    case "7": NullableReferenceTypeDemo(); break;
                    case "8": BoxingUnboxingDemo(); break;
                    case "9":
                        SumAndMultiply(4, 5, out int sum, out int mul);
                        Console.WriteLine($"Sum = {sum}, Multiply = {mul}");
                        break;
                    case "10":
                        PrintMessage("Hello"); // uses default Repeat = 5
                        PrintMessage(repeat: 2, message: "Hi"); // named parameters
                        break;
                    case "11": NullPropagationDemo(); break;
                    case "12": DayOfWeekSwitchExpressionDemo(); break;
                    case "13":
                        // Calling with individual values
                        Console.WriteLine("Sum (1,2,3) = " + SumArray(1, 2, 3));
                        // Calling with an existing array
                        int[] numbers = { 10, 20, 30, 40 };
                        Console.WriteLine("Sum (array) = " + SumArray(numbers));
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
        #endregion

        #region 1 - Divide two numbers (try / catch / finally)
        // Q: What is the purpose of the finally block?
        // A: The finally block always runs after the try (and any matching catch),
        //    whether an exception was thrown or not. It is used for cleanup /
        //    guaranteed actions (closing files, releasing resources, logging,
        //    printing a closing message, etc.) that must happen regardless of
        //    the outcome of the risky code.
        public static void DivideNumbers()
        {
            try
            {
                Console.Write("Enter the first number (numerator): ");
                int x = int.Parse(Console.ReadLine());

                Console.Write("Enter the second number (denominator): ");
                int y = int.Parse(Console.ReadLine());

                int result = x / y;
                Console.WriteLine($"Result = {result}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Error: Cannot divide by zero. " + ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Please enter valid integer values.");
            }
            finally
            {
                Console.WriteLine("Operation complete.");
            }
        }
        #endregion

        #region 2 - Defensive input (positive X, Y > 1)
        // Q: How does int.TryParse() improve program robustness compared to int.Parse()?
        // A: int.Parse() throws a FormatException (or OverflowException) if the input
        //    is not a valid number, which crashes the program unless wrapped in try/catch.
        //    int.TryParse() never throws for bad input: it returns a bool indicating
        //    success/failure and outputs the parsed value via an out parameter
        //    (0 on failure). This lets you validate input in a simple loop/condition
        //    without relying on exception handling for normal control flow.
        public static void TestDefensiveCode()
        {
            int X, Y;

            do
            {
                Console.Write("Enter first Number (positive) : ");
            }
            while (!int.TryParse(Console.ReadLine(), out X) || X <= 0);

            do
            {
                Console.Write("Enter second Number (positive and greater than 1) : ");
            }
            while (!int.TryParse(Console.ReadLine(), out Y) || Y <= 1);

            int Z = X / Y;
            Console.WriteLine($"X = {X}, Y = {Y}, X / Y = {Z}");
        }
        #endregion

        #region 3 - Nullable int (??, HasValue, Value)
        // Q: What exception occurs when trying to access Value on a null Nullable<T>?
        // A: System.InvalidOperationException ("Nullable object must have a value.").
        public static void NullableIntDemo()
        {
            int? number = null;

            // Null-coalescing operator: use 0 as default if number is null
            int result = number ?? 0;
            Console.WriteLine($"Using ?? : result = {result}");

            // HasValue / Value pattern (safe way)
            if (number.HasValue)
            {
                Console.WriteLine($"number has a value: {number.Value}");
            }
            else
            {
                Console.WriteLine("number has no value (HasValue = false).");
            }

            // Demonstrating the exception when accessing .Value on a null Nullable<int>
            try
            {
                int unsafeAccess = number.Value; // throws because number is null
                Console.WriteLine(unsafeAccess);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Exception caught: " + ex.Message);
            }

            // Now give it a value and show the difference
            number = 25;
            Console.WriteLine($"After assignment -> HasValue: {number.HasValue}, Value: {number.Value}");
        }
        #endregion

        #region 4 - Array bounds (IndexOutOfRangeException)
        // Q: Why is it necessary to check array bounds before accessing elements?
        // A: Arrays have a fixed length; accessing an index outside [0, Length-1]
        //    throws an IndexOutOfRangeException and can crash the program if
        //    unhandled. Checking bounds first (or using TryGetValue-style patterns)
        //    prevents runtime crashes, avoids relying on exceptions for normal
        //    control flow, and produces more predictable, defensive code.
        public static void ArrayBoundsDemo()
        {
            int[] numbers = new int[5] { 10, 20, 30, 40, 50 };

            Console.Write("Enter an index to access (0-4 is valid): ");
            int index = int.Parse(Console.ReadLine());

            try
            {
                Console.WriteLine($"Value at index {index} = {numbers[index]}");
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine("Error: Index is out of the array's bounds. " + ex.Message);
            }
        }
        #endregion

        #region 5 - 3x3 array: row/column sums
        // Q: How is the GetLength(dimension) method used in multi-dimensional arrays?
        // A: For a rectangular array, arr.GetLength(0) returns the number of rows
        //    and arr.GetLength(1) returns the number of columns (dimension index
        //    starts at 0). It lets you loop through a multi-dimensional array
        //    generically without hardcoding its size.
        public static void Rectangular3x3ArrayDemo()
        {
            int[,] matrix = new int[3, 3];

            Console.WriteLine("Enter 9 values for the 3x3 matrix:");
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write($"matrix[{row},{col}] = ");
                    matrix[row, col] = int.Parse(Console.ReadLine());
                }
            }

            // Sum of each row
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                int rowSum = 0;
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    rowSum += matrix[row, col];
                }
                Console.WriteLine($"Sum of row {row} = {rowSum}");
            }

            // Sum of each column
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                int colSum = 0;
                for (int row = 0; row < matrix.GetLength(0); row++)
                {
                    colSum += matrix[row, col];
                }
                Console.WriteLine($"Sum of column {col} = {colSum}");
            }
        }
        #endregion

        #region 6 - Jagged array (3 rows, varying sizes)
        // Q: How does the memory allocation differ between jagged arrays and rectangular arrays?
        // A: A rectangular array (e.g. int[,]) is a single block of memory where every
        //    row has the same fixed number of columns. A jagged array (int[][]) is an
        //    array of references, where each element is itself an independently
        //    allocated array that can have a different length. This makes jagged
        //    arrays more memory-flexible (no wasted cells) but adds a level of
        //    indirection (extra reference lookups) compared to rectangular arrays.
        public static void JaggedArrayDemo()
        {
            int[][] jagged = new int[3][];

            for (int row = 0; row < jagged.Length; row++)
            {
                Console.Write($"Enter number of elements for row {row}: ");
                int size = int.Parse(Console.ReadLine());
                jagged[row] = new int[size];

                for (int col = 0; col < size; col++)
                {
                    Console.Write($"  jagged[{row}][{col}] = ");
                    jagged[row][col] = int.Parse(Console.ReadLine());
                }
            }

            Console.WriteLine("\nJagged array contents:");
            for (int row = 0; row < jagged.Length; row++)
            {
                Console.Write($"Row {row}: ");
                for (int col = 0; col < jagged[row].Length; col++)
                {
                    Console.Write(jagged[row][col] + " ");
                }
                Console.WriteLine();
            }
        }
        #endregion

        #region 7 - Nullable reference types (string?, null-forgiveness !)
        // Q: What is the purpose of nullable reference types in C#?
        // A: Nullable reference types (C# 8.0+, enabled via #nullable enable or the
        //    project's <Nullable> setting) let the compiler track, at compile time,
        //    whether a reference type variable is allowed to be null (string?) or
        //    is expected to never be null (string). The compiler then warns you
        //    about possible NullReferenceExceptions, helping catch null-related
        //    bugs earlier without changing runtime behavior.
#nullable enable
        public static void NullableReferenceTypeDemo()
        {
            Console.Write("Enter your name (or leave blank): ");
            string? userInput = Console.ReadLine();

            string? name = string.IsNullOrWhiteSpace(userInput) ? null : userInput;

            if (name != null)
            {
                Console.WriteLine($"Hello, {name}!");
            }
            else
            {
                Console.WriteLine("No name was provided.");
            }

            // Null-forgiveness operator (!): tells the compiler "trust me, this
            // is not null here", suppressing the nullable warning. Use only when
            // you are certain, since it does NOT prevent a runtime exception if
            // you are wrong.
            string? maybeNull = GetOptionalValue();
            string definitelyNotNull = maybeNull!;
            Console.WriteLine("Forced non-null value: " + definitelyNotNull);
        }

        private static string? GetOptionalValue()
        {
            return "some value"; // in real code this could sometimes return null
        }
#nullable disable
        #endregion

        #region 8 - Boxing / Unboxing
        // Q: What is the performance impact of boxing and unboxing in C#?
        // A: Boxing copies a value type into a new object on the managed heap,
        //    and unboxing copies it back to a value type on the stack. Both
        //    involve extra memory allocation, a heap allocation for boxing, a
        //    type check for unboxing, and additional garbage collection
        //    pressure. This makes boxing/unboxing noticeably slower than
        //    working directly with value types, so it should be avoided in
        //    performance-critical code (e.g. by using generics instead of
        //    object-based collections).
        public static void BoxingUnboxingDemo()
        {
            int number = 42;

            // Boxing: value type -> object (heap)
            object boxed = number;
            Console.WriteLine($"Boxed value: {boxed} (type: {boxed.GetType()})");

            // Unboxing: object -> value type (stack)
            int unboxed = (int)boxed;
            Console.WriteLine($"Unboxed value: {unboxed}");

            // Invalid unboxing attempt
            try
            {
                double invalid = (double)boxed; // boxed actually holds an int, not a double
                Console.WriteLine(invalid);
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine("Invalid cast caught: " + ex.Message);
            }
        }
        #endregion

        #region 9 - SumAndMultiply (out parameters)
        // Q: Why must out parameters be initialized inside the method?
        // A: The compiler requires every "out" parameter to be definitely
        //    assigned before the method returns, because the caller passes an
        //    uninitialized variable and relies entirely on the method to give
        //    it a value. Unlike "ref", the caller does not need to initialize
        //    it beforehand, so the method is responsible for guaranteeing a
        //    value; failing to assign it is a compile-time error.
        public static void SumAndMultiply(int n1, int n2, out int sum, out int mul)
        {
            sum = n1 + n2;
            mul = n1 * n2;
        }
        #endregion

        #region 10 - PrintMessage (optional + named parameters)
        // Q: Why must optional parameters always appear at the end of a method's parameter list?
        // A: When calling a method positionally, the compiler matches arguments to
        //    parameters by order. If an optional parameter appeared before a
        //    required one, the compiler couldn't tell whether an omitted argument
        //    means "use the default" for that optional parameter or "shift" the
        //    following arguments. Placing optional parameters last removes this
        //    ambiguity: callers can simply stop supplying arguments once they
        //    want the defaults to kick in (or use named arguments to skip around).
        public static void PrintMessage(string message, int repeat = 5)
        {
            for (int i = 0; i < repeat; i++)
            {
                Console.WriteLine(message);
            }
        }
        #endregion

        #region 11 - Nullable array + null propagation (?.)
        // Q: How does the null propagation operator prevent NullReferenceException?
        // A: The ?. operator only evaluates the member access if the object on its
        //    left side is not null; if it IS null, the whole expression short-
        //    circuits and evaluates to null instead of throwing a
        //    NullReferenceException. This lets you safely chain member/property
        //    access (e.g. arr?.Length, obj?.Prop?.SubProp) without manually
        //    writing nested null checks.
        public static void NullPropagationDemo()
        {
            int[] arr = null;

            // Safe access: since arr is null, this evaluates to null instead of throwing
            int? length = arr?.Length;
            Console.WriteLine("Length of null array (using ?.): " + (length ?? -1));

            arr = new int[] { 1, 2, 3, 4 };
            length = arr?.Length;
            Console.WriteLine("Length of populated array (using ?.): " + length);

            // Combine with ?? to give a friendly default message
            Console.WriteLine("Result with ?? fallback: " + (arr?.Length.ToString() ?? "Array is null"));
        }
        #endregion

        #region 12 - Day of week -> number (switch expression)
        // Q: When is a switch expression preferred over a traditional if statement?
        // A: A switch expression is preferred when you are mapping a single value
        //    to one of several possible results based on discrete cases (an
        //    enum, a string, a pattern, etc.). It's more concise and readable
        //    than a long if/else-if chain, it's an expression (so it directly
        //    produces/returns a value), and the compiler can warn you about
        //    missing cases. A traditional if is usually better for complex
        //    conditions that don't map cleanly to a fixed set of discrete cases.
        public static void DayOfWeekSwitchExpressionDemo()
        {
            Console.Write("Enter a day of the week (e.g. Monday): ");
            string day = Console.ReadLine()?.Trim().ToLower();

            int dayNumber = day switch
            {
                "monday" => 1,
                "tuesday" => 2,
                "wednesday" => 3,
                "thursday" => 4,
                "friday" => 5,
                "saturday" => 6,
                "sunday" => 7,
                _ => -1
            };

            if (dayNumber == -1)
                Console.WriteLine("Unrecognized day.");
            else
                Console.WriteLine($"{day} -> {dayNumber}");
        }
        #endregion

        #region 13 - SumArray (params keyword)
        // Q: What are the limitations of the params keyword in method definitions?
        // A: A method can have only ONE params parameter, and it must be the LAST
        //    parameter in the parameter list. The params array must be a single-
        //    dimensional array type. You cannot combine params with ref/out on
        //    the same parameter. Also, passing many individual arguments this
        //    way can hide a small performance cost, since the compiler
        //    allocates an array behind the scenes for each call.
        public static int SumArray(params int[] arr)
        {
            int sum = 0;
            for (int i = 0; i < arr?.Length; i++)
                sum += arr[i];
            return sum;
        }
        #endregion
    }
}