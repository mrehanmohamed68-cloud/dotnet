using System;

namespace Day4CSharp
{
    internal class Program
    {
        static void Main()
        {
            Problem1_ArrayInitAndOutOfRange();
            Problem2_ShallowVsDeepCopy();
            Problem3_TwoDimArrayGrades();
            Problem4_ArrayMethods();
            Problem5_ForForeachWhile();
            Problem6_DefensiveOddNumber();
            Problem7_MatrixFormatting();
            Problem8_MonthIfElseVsSwitch();
            Problem9_SortAndSearch();
            Problem10_SumForVsForeach();

            Console.WriteLine("\n=== End of Assignment ===");
        }

        // ------------------------------------------------------------
        // Problem 1: Array initialization (3 ways) + IndexOutOfRangeException
        // ------------------------------------------------------------
        static void Problem1_ArrayInitAndOutOfRange()
        {
            Console.WriteLine("---- Problem 1: Array initialization ----");

            // 1) new int[size]
            int[] arr1 = new int[3];
            arr1[0] = 10; arr1[1] = 20; arr1[2] = 30;

            // 2) initializer list
            int[] arr2 = new int[] { 1, 2, 3 };

            // 3) array syntax sugar (no "new int[]" at all)
            int[] arr3 = { 100, 200, 300 };

            Console.WriteLine("arr1: " + string.Join(", ", arr1));
            Console.WriteLine("arr2: " + string.Join(", ", arr2));
            Console.WriteLine("arr3: " + string.Join(", ", arr3));

            try
            {
                Console.WriteLine(arr1[5]); // out of bounds on purpose
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Caught IndexOutOfRangeException: index 5 does not exist in arr1.");
            }
        }

        // Question (Problem 1): Default value assigned to array elements in C#?
        // When an array is created with `new`, every element is automatically initialized
        // to the default value of its data type: 0 for numeric types (int, double...),
        // false for bool, '\0' for char, and null for any reference type (string, class...).

        // ------------------------------------------------------------
        // Problem 2: Shallow copy vs Deep copy
        // ------------------------------------------------------------
        static void Problem2_ShallowVsDeepCopy()
        {
            Console.WriteLine("\n---- Problem 2: Shallow vs Deep copy ----");

            int[] arr01 = { 1, 2, 3 };
            int[] arr02 = { 4, 5, 6 };

            // Shallow copy: just copies the reference, both variables point to the SAME array
            arr02 = arr01;
            arr02[0] = 99; // modifying through arr02 also changes arr01

            Console.WriteLine($"Shallow copy -> arr01[0]: {arr01[0]}, arr02[0]: {arr02[0]}");
            // Both print 99, proving they share the same underlying array.

            // Deep copy: Clone() creates a new, independent array with the same values
            int[] arr03 = { 1, 2, 3 };
            int[] arr04 = (int[])arr03.Clone();
            arr04[0] = 500; // modifying arr04 does NOT affect arr03

            Console.WriteLine($"Deep copy    -> arr03[0]: {arr03[0]}, arr04[0]: {arr04[0]}");
        }

        // Question (Problem 2): Difference between Array.Clone() and Array.Copy()?
        // - Array.Clone() is an instance method that creates and RETURNS a brand-new array
        //   (a shallow copy) with the same length and elements as the original. You don't
        //   need a destination array beforehand.
        // - Array.Copy() is a static method that copies a specified number of elements
        //   FROM an existing source array INTO an existing destination array (which you
        //   must create first, and which can even be a different size). It doesn't create
        //   a new array itself - it just fills one you already have.

        // ------------------------------------------------------------
        // Problem 3: 2D array of student grades (3 students x 3 subjects)
        // ------------------------------------------------------------
        static void Problem3_TwoDimArrayGrades()
        {
            Console.WriteLine("\n---- Problem 3: 2D array of grades ----");

            int[,] grades = new int[3, 3];

            for (int i = 0; i < grades.GetLength(0); i++)
            {
                for (int j = 0; j < grades.GetLength(1); j++)
                {
                    Console.Write($"Enter grade for student {i + 1}, subject {j + 1}: ");
                    grades[i, j] = int.Parse(Console.ReadLine());
                }
            }

            Console.WriteLine("\nGrades table:");
            for (int i = 0; i < grades.GetLength(0); i++)
            {
                Console.WriteLine($"Student {i + 1}:");
                for (int j = 0; j < grades.GetLength(1); j++)
                {
                    Console.WriteLine($"  Subject {j + 1}: {grades[i, j]}");
                }
            }
        }

        // Question (Problem 3): Difference between GetLength() and Length for
        // multidimensional arrays?
        // - Length returns the TOTAL number of elements across ALL dimensions combined
        //   (e.g. for a 3x3 array, Length = 9).
        // - GetLength(dimension) returns the number of elements in ONE specific dimension
        //   only (e.g. GetLength(0) = 3 rows, GetLength(1) = 3 columns for a 3x3 array).
        //   You need GetLength when looping dimension-by-dimension in a nested loop.

        // ------------------------------------------------------------
        // Problem 4: Array methods - Sort, Reverse, IndexOf, Copy, Clear
        // ------------------------------------------------------------
        static void Problem4_ArrayMethods()
        {
            Console.WriteLine("\n---- Problem 4: Array methods ----");

            int[] numbers = { 9, 3, 10, 2, 8, 10, 6, 5, 1 };
            Console.WriteLine("Before Sort:  " + string.Join(", ", numbers));

            Array.Sort(numbers);
            Console.WriteLine("After Sort:   " + string.Join(", ", numbers));
            // Sort rearranges elements in ascending order.

            Array.Reverse(numbers);
            Console.WriteLine("After Reverse:" + string.Join(", ", numbers));
            // Reverse flips the entire order of elements.

            int index = Array.IndexOf(numbers, 10);
            Console.WriteLine($"IndexOf(10):  {index}");
            // IndexOf returns the position of the FIRST occurrence of the value (-1 if not found).

            int[] destination = new int[5];
            Array.Copy(numbers, destination, 5);
            Console.WriteLine("After Copy (first 5 into new array): " + string.Join(", ", destination));
            // Copy transfers a given number of elements from a source array into a destination array.

            Array.Clear(numbers, 2, 4);
            Console.WriteLine("After Clear (positions 2..5 reset): " + string.Join(", ", numbers));
            // Clear resets a range of elements back to their default value (0 for int), without resizing the array.
        }

        // Question (Problem 4): Difference between Array.Copy() and Array.ConstrainedCopy()?
        // Both copy elements from one array to another, but Array.ConstrainedCopy provides
        // a stronger guarantee: it either completes the ENTIRE copy successfully, or - if
        // any problem would occur (e.g. a type mismatch partway through) - it throws an
        // exception WITHOUT having modified the destination array at all (an "all-or-nothing"
        // / atomic operation). Array.Copy offers no such guarantee: if it fails partway
        // through, the destination array may be left partially modified.

        // ------------------------------------------------------------
        // Problem 5: for / foreach / while (reverse)
        // ------------------------------------------------------------
        static void Problem5_ForForeachWhile()
        {
            Console.WriteLine("\n---- Problem 5: for / foreach / while ----");

            int[] arr = { 10, 20, 30, 40, 50 };

            Console.WriteLine("for loop:");
            for (int i = 0; i < arr.Length; i++)
                Console.WriteLine(arr[i]);

            Console.WriteLine("foreach loop:");
            foreach (int item in arr)
                Console.WriteLine(item);

            Console.WriteLine("while loop (reverse order):");
            int idx = arr.Length - 1;
            while (idx >= 0)
            {
                Console.WriteLine(arr[idx]);
                idx--;
            }
        }

        // Question (Problem 5): Why is foreach preferred for read-only operations on arrays?
        // - No manual index management: you can't accidentally go out of bounds or write
        //   an off-by-one bug, since foreach handles the iteration internally.
        // - Cleaner, more readable code, especially for nested or complex structures
        //   (like a list of lists).
        // It is restricted to read-only access precisely because the iteration variable is
        // a COPY taken from the collection, not the original slot - so foreach cannot be
        // used to modify the source array's elements (the for loop can, since it accesses
        // the original array directly by index).

        // ------------------------------------------------------------
        // Problem 6: Defensive coding - positive odd number via TryParse + do-while
        // ------------------------------------------------------------
        static void Problem6_DefensiveOddNumber()
        {
            Console.WriteLine("\n---- Problem 6: Positive odd number (defensive) ----");

            int number;
            bool isValid;

            do
            {
                Console.Write("Enter a positive odd number: ");
                isValid = int.TryParse(Console.ReadLine(), out number);
            }
            while (!isValid || number <= 0 || number % 2 == 0);

            Console.WriteLine($"Accepted valid positive odd number: {number}");
        }

        // Question (Problem 6): Why is input validation important when working with user inputs?
        // User input can never be trusted to match what the program expects - people
        // mistype, paste the wrong thing, or the field is left empty. Without validation,
        // invalid input can crash the program (unhandled exceptions like FormatException),
        // corrupt business logic (e.g. negative quantities, invalid dates), or in more
        // serious contexts open the door to security vulnerabilities. Validating input
        // (with TryParse, range checks, etc.) keeps the program stable and its data correct.

        // ------------------------------------------------------------
        // Problem 7: 2D array printed as a matrix (rows and columns)
        // ------------------------------------------------------------
        static void Problem7_MatrixFormatting()
        {
            Console.WriteLine("\n---- Problem 7: Matrix formatting ----");

            int[,] matrix = { { 45, 78, 98 }, { 43, 76, 90 }, { 12, 34, 56 } };

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j].ToString().PadLeft(5)); // fixed-width column
                }
                Console.WriteLine();
            }
        }

        // Question (Problem 7): How can you format the output of a 2D array for better readability?
        // Pad each value to a fixed width (e.g. ToString().PadLeft(5)) so columns line up
        // visually regardless of digit count, print a newline after each row so rows don't
        // run together, and optionally add a separator (like " | ") between columns or a
        // header row with column labels for extra clarity.

        // ------------------------------------------------------------
        // Problem 8: Month number - if-else vs switch
        // ------------------------------------------------------------
        static void Problem8_MonthIfElseVsSwitch()
        {
            Console.WriteLine("\n---- Problem 8: Month name (if-else vs switch) ----");
            Console.Write("Enter month number: ");
            int month = int.Parse(Console.ReadLine());

            // if-else version
            if (month == 1) Console.WriteLine("if-else: Jan");
            else if (month == 2) Console.WriteLine("if-else: Feb");
            else if (month == 3) Console.WriteLine("if-else: Mar");
            else Console.WriteLine("if-else: Not in Q1");

            // switch version
            switch (month)
            {
                case 1: Console.WriteLine("switch: Jan"); break;
                case 2: Console.WriteLine("switch: Feb"); break;
                case 3: Console.WriteLine("switch: Mar"); break;
                default: Console.WriteLine("switch: Not in Q1"); break;
            }
        }

        // Question (Problem 8): When should you prefer a switch statement over if-else?
        // When checking a SINGLE variable against many possible discrete, constant values
        // (numbers, strings, enum members). Switch reads more cleanly than a long
        // if/else-if chain, and for many cases (especially numeric/string), the compiler
        // can generate a jump table internally, giving faster average lookup than
        // sequentially testing each condition in an if-else chain. Prefer if-else instead
        // when conditions involve ranges, multiple variables, or complex boolean logic
        // that switch patterns can't express as cleanly.

        // ------------------------------------------------------------
        // Problem 9: Sort + Search (IndexOf / LastIndexOf)
        // ------------------------------------------------------------
        static void Problem9_SortAndSearch()
        {
            Console.WriteLine("\n---- Problem 9: Sort and search ----");

            int[] numbers = { 9, 3, 10, 2, 8, 10, 6, 5, 1 };
            Array.Sort(numbers);
            Console.WriteLine("Sorted: " + string.Join(", ", numbers));

            int first = Array.IndexOf(numbers, 10);
            int last = Array.LastIndexOf(numbers, 10);
            Console.WriteLine($"First index of 10: {first}, Last index of 10: {last}");
        }

        // Question (Problem 9): What is the time complexity of Array.Sort()?
        // Array.Sort() uses an introspective sort (a hybrid of quicksort, heapsort, and
        // insertion sort for small partitions) under the hood. Its average and typical
        // case time complexity is O(n log n); its worst case is also O(n log n) thanks to
        // the fallback to heapsort when quicksort's recursion gets too deep (avoiding
        // quicksort's naive O(n^2) worst case).

        // ------------------------------------------------------------
        // Problem 10: Sum via for vs foreach
        // ------------------------------------------------------------
        static void Problem10_SumForVsForeach()
        {
            Console.WriteLine("\n---- Problem 10: Sum (for vs foreach) ----");

            int[] numbers = { 5, 10, 15, 20, 25 };

            int sumFor = 0;
            for (int i = 0; i < numbers.Length; i++)
                sumFor += numbers[i];

            int sumForeach = 0;
            foreach (int n in numbers)
                sumForeach += n;

            Console.WriteLine($"Sum via for: {sumFor}");
            Console.WriteLine($"Sum via foreach: {sumForeach}");
        }

        // Question (Problem 10): Which loop (for or foreach) is more efficient for
        // calculating the sum of an array, and why?
        // For plain arrays specifically, they are essentially equivalent in practice: the
        // JIT compiler recognizes the foreach pattern over an array (T[]) and optimizes it
        // down to the same index-based loop as a for statement, including the same bounds-
        // check elimination. So for arrays, there is no meaningful performance difference.
        // The "for is faster" rule of thumb applies more to other collection types (like
        // List<T> accessed through IEnumerable<T>), where foreach carries a small overhead
        // from calling MoveNext()/Current on an enumerator versus direct index access.
    }
}
