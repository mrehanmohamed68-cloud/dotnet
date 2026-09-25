using System;
using System.Collections.Generic;
using System.Linq;

namespace day10_csharp
{
    internal class Program
    {
        static void Main()
        {
            #region 1) Generic sort of Employee[] by Salary ascending
            Console.WriteLine("---- 1) SortingAlgorithm<Employee>.Sort ----");
            Employee[] emps =
            {
                new Employee(1, "Ali", 4567),
                new Employee(2, "Abdo", 9567),
                new Employee(3, "Ismail", 3567)
            };
            SortingAlgorithm<Employee>.Sort(emps);
            foreach (Employee e in emps) Console.WriteLine(e);
            // Q: Benefits of a generic sorting algorithm over a non-generic one?
            // - One implementation works for int, Employee, or any IComparable<T>
            //   type, instead of duplicating the same loop for every type.
            // - Compile-time type safety (no casting, no InvalidCastException).
            // - Easier to maintain: fix/optimize the algorithm once, everyone benefits.
            #endregion

            Console.WriteLine();

            #region 2) SortingTwo<int>.Sort descending using a lambda
            Console.WriteLine("---- 2) SortingTwo<int> descending (lambda) ----");
            int[] nums = { 1, 5, 3, 8, 2 };
            SortingTwo<int>.Sort(nums, (x, y) => x < y); // swap when x < y -> descending
            Console.WriteLine(string.Join(", ", nums));
            // Q: How do lambdas improve readability/flexibility of sorting methods?
            // - The comparison rule is written right where it's used (no separate
            //   named method to jump to), which reads almost like plain English.
            // - Flexibility: swapping to a different rule is a one-line change,
            //   no new method needed, and Sort itself never changes.
            #endregion

            Console.WriteLine();

            #region 3) SortingTwo<string>.Sort by length ascending
            Console.WriteLine("---- 3) SortingTwo<string> by length ascending ----");
            string[] words = { "banana", "fig", "apple", "kiwi" };
            SortingTwo<string>.Sort(words, CompareFunctions.CompareStringLengthAsc);
            Console.WriteLine(string.Join(", ", words));
            // Q: Why use a dynamic comparer function when sorting different data types?
            // - The sorting algorithm itself has no idea what "correct order" means
            //   for a given type (numeric value? string length? object field?).
            //   A dynamic comparer lets the CALLER define "correct order" per call,
            //   so one Sort method serves every use case instead of one per rule.
            #endregion

            Console.WriteLine();

            #region 4) Manager : Employee, IComparable<Manager>
            Console.WriteLine("---- 4) Manager sorting ----");
            Manager[] managers =
            {
                new Manager(10, "Mona", 12000, "HR"),
                new Manager(11, "Karim", 9000, "IT"),
                new Manager(12, "Laila", 15000, "Finance")
            };
            SortingAlgorithm<Manager>.Sort(managers);
            foreach (Manager m in managers) Console.WriteLine(m);
            // Q: How does implementing IComparable<T> in derived classes enable custom sorting?
            // - It gives the derived type its OWN explicit ordering rule (here,
            //   IComparable<Manager>) separate from the base type's rule
            //   (IComparable<Employee>), so generic sorting code
            //   (SortingAlgorithm<Manager>) knows exactly how to order Managers
            //   without any special-casing.
            #endregion

            Console.WriteLine();

            #region 5) Func<Employee,Employee,bool> comparing by Name length
            Console.WriteLine("---- 5) SortingTwo<Employee> by Name length ----");
            Employee[] empsByName =
            {
                new Employee(1, "Al", 1000),
                new Employee(2, "Mohammed", 2000),
                new Employee(3, "Sara", 3000)
            };
            Func<Employee, Employee, bool> byNameLength = CompareFunctions.CompareEmployeeByNameLength;
            SortingTwo<Employee>.Sort(empsByName, byNameLength);
            foreach (Employee e in empsByName) Console.WriteLine(e);
            // Q: Advantage of built-in delegates like Func<T,T,TResult> in generic programming?
            // - No need to declare a custom delegate type for every signature you
            //   need — Func/Action/Predicate already cover almost all shapes, which
            //   means less boilerplate and instantly recognizable, standard APIs.
            #endregion

            Console.WriteLine();

            #region 6) Anonymous function vs lambda expression (sort int ascending)
            Console.WriteLine("---- 6) Anonymous function vs lambda ----");
            int[] numsA = { 4, 1, 3, 2 };
            SortingTwo<int>.Sort(numsA, delegate (int x, int y) { return x > y; }); // anonymous function
            Console.WriteLine("Anonymous: " + string.Join(", ", numsA));

            int[] numsB = { 4, 1, 3, 2 };
            SortingTwo<int>.Sort(numsB, (x, y) => x > y); // lambda expression, same logic
            Console.WriteLine("Lambda:    " + string.Join(", ", numsB));
            // Q: How do anonymous functions differ from lambdas in readability/efficiency?
            // - Readability: lambdas are shorter and read more like a plain
            //   expression ("x, y => x > y") vs the more verbose "delegate(...) { return ...; }".
            // - Efficiency: functionally equivalent — a lambda is really just
            //   syntactic sugar compiled down to the same delegate machinery as
            //   an anonymous method, so there's no runtime performance difference.
            #endregion

            Console.WriteLine();

            #region 7) Standalone generic Swap<T> (unconstrained)
            Console.WriteLine("---- 7) SortingAlgorithm<T>.Swap<T> ----");
            int[] swapArr = { 10, 20 };
            Console.WriteLine("Before: " + string.Join(", ", swapArr));
            SortingAlgorithm<int>.Swap(ref swapArr[0], ref swapArr[1]);
            Console.WriteLine("After:  " + string.Join(", ", swapArr));
            // Q: Why are generic methods beneficial for utility functions like Swap?
            // - One Swap<TItem> works for int, string, Employee, or anything else,
            //   instead of writing/maintaining a near-duplicate Swap per type —
            //   less code, one place to fix bugs, works with brand-new types too.
            #endregion

            Console.WriteLine();

            #region 8) Multi-criteria sort: Salary then Name
            Console.WriteLine("---- 8) Multi-criteria sort (Salary, then Name) ----");
            Employee[] tieBreak =
            {
                new Employee(1, "Zeyad", 5000),
                new Employee(2, "Amir", 5000),
                new Employee(3, "Nour", 3000)
            };
            SortingTwo<Employee>.Sort(tieBreak, CompareFunctions.CompareEmployeeSalaryThenName);
            foreach (Employee e in tieBreak) Console.WriteLine(e);
            // Q: Challenges and benefits of multi-criteria sorting logic in generic methods?
            // - Challenge: the comparer function grows more complex (nested
            //   conditions per tiebreak level) and must be written carefully so
            //   ties are broken consistently (stable, unambiguous ordering).
            // - Benefit: the generic Sort method itself never has to change —
            //   all the extra complexity lives in one small, testable comparer
            //   function, keeping the sorting algorithm reusable and simple.
            #endregion

            Console.WriteLine();

            #region 9) GetDefault<T> — value type vs reference type
            Console.WriteLine("---- 9) GetDefault<T> ----");
            Console.WriteLine($"default(int)     = {SortingAlgorithm<int>.GetDefault()}");
            Console.WriteLine($"default(string)  = {SortingAlgorithm<string>.GetDefault() ?? "null"}");
            // Q: Why is default(T) crucial, and how does it differ for value vs reference types?
            // - Generic code can't hardcode "0" or "null" because it doesn't know
            //   what T will be at compile time. default(T) lets the compiler pick
            //   the right "empty" value automatically for whatever T turns out to be.
            // - Value types (int, double, structs) -> their zero-equivalent (0, 0.0,
            //   all-fields-zeroed struct). Reference types (string, Employee) -> null.
            #endregion

            Console.WriteLine();

            #region 10) ICloneable constraint + clone before sort
            Console.WriteLine("---- 10) Clone before sort (ICloneable constraint) ----");
            Employee[] original =
            {
                new Employee(1, "Hana", 7000),
                new Employee(2, "Youssef", 2000)
            };
            Employee[] cloned = SortingAlgorithm<Employee>.CloneArray(original);
            SortingAlgorithm<Employee>.Sort(cloned);
            Console.WriteLine("Original (untouched): " + string.Join(" | ", original.Select(e => e.ToString())));
            Console.WriteLine("Cloned (sorted):       " + string.Join(" | ", cloned.Select(e => e.ToString())));
            // Q: How do constraints ensure type safety and reliability in generics?
            // - "where T : IComparable<T>, ICloneable" is checked at COMPILE time:
            //   any T that doesn't implement both is rejected before the code even
            //   runs, so SortingAlgorithm<T> can safely call .CompareTo() and
            //   .Clone() on any T without runtime type checks or casting risk.
            #endregion

            Console.WriteLine();

            #region 11) Custom delegate: string -> string
            Console.WriteLine("---- 11) StringTransformDelegate ----");
            List<string> names = new List<string> { "ali", "sara", "omar" };
            Console.WriteLine("Upper:   " + string.Join(", ", DelegateHelpers.ApplyTransform(names, s => s.ToUpper())));
            Console.WriteLine("Reverse: " + string.Join(", ", DelegateHelpers.ApplyTransform(names, s => new string(s.Reverse().ToArray()))));
            // Q: Benefits of using delegates for string transformations in a functional style?
            // - The transformation logic is injected from outside, so
            //   ApplyTransform stays generic/reusable for ANY string->string rule
            //   (uppercase, reverse, trim, encode...) without ever being modified.
            #endregion

            Console.WriteLine();

            #region 12) Custom delegate: (int,int) -> int
            Console.WriteLine("---- 12) MathOperationDelegate ----");
            Console.WriteLine("Add: " + DelegateHelpers.PerformOperation(6, 3, (a, b) => a + b));
            Console.WriteLine("Sub: " + DelegateHelpers.PerformOperation(6, 3, (a, b) => a - b));
            Console.WriteLine("Mul: " + DelegateHelpers.PerformOperation(6, 3, (a, b) => a * b));
            Console.WriteLine("Div: " + DelegateHelpers.PerformOperation(6, 3, (a, b) => a / b));
            // Q: How does using delegates promote code reusability/flexibility for math ops?
            // - PerformOperation doesn't know or care WHICH operation runs — it
            //   just invokes whatever delegate it's handed, so adding a brand-new
            //   operation (e.g. modulo) needs zero changes to PerformOperation itself.
            #endregion

            Console.WriteLine();

            #region 13) Generic delegate: TransformDelegate<T,TResult>
            Console.WriteLine("---- 13) TransformDelegate<T,TResult> ----");
            List<int> intList = new List<int> { 1, 2, 3 };
            List<string> asStrings = DelegateHelpers.TransformList<int, string>(intList, n => $"#{n}");
            Console.WriteLine(string.Join(", ", asStrings));
            // Q: Advantages of generic delegates in transforming data structures?
            // - A single TransformDelegate<T,TResult> covers int->string,
            //   Employee->string, double->int, or any T->TResult shape you need,
            //   instead of declaring a brand-new non-generic delegate per pair of types.
            #endregion

            Console.WriteLine();

            #region 14) Func<int,int> to square numbers
            Console.WriteLine("---- 14) Func<int,int> square ----");
            List<int> squares = DelegateHelpers.ApplyFunc(new List<int> { 1, 2, 3, 4 }, x => x * x);
            Console.WriteLine(string.Join(", ", squares));
            // Q: How does Func simplify creating/using delegates in C#?
            // - No delegate declaration needed at all — Func<int,int> (and its
            //   many generic overloads) already exists in the framework, so you
            //   go straight to writing the logic (x => x * x) instead of ceremony.
            #endregion

            Console.WriteLine();

            #region 15) Action<string> to print
            Console.WriteLine("---- 15) Action<string> ----");
            DelegateHelpers.ApplyAction(new List<string> { "one", "two", "three" }, s => Console.WriteLine($"Item: {s}"));
            // Q: Why is Action preferred for operations that don't return a value?
            // - Its signature has no TResult slot at all, so the compiler
            //   guarantees at compile time that the delegate can't accidentally
            //   be expected to return something — the intent ("just do this, return nothing") is explicit.
            #endregion

            Console.WriteLine();

            #region 16) Predicate<int> to filter even numbers
            Console.WriteLine("---- 16) Predicate<int> even filter ----");
            List<int> evens = DelegateHelpers.FilterList(new List<int> { 1, 2, 3, 4, 5, 6 }, n => n % 2 == 0);
            Console.WriteLine(string.Join(", ", evens));
            // Q: Role of predicates in functional programming / how do they enhance clarity?
            // - A Predicate<T> is a delegate whose whole job is "does this item
            //   qualify? yes/no" — naming that concept explicitly (rather than a
            //   generic Func<T,bool>) makes filtering code self-documenting.
            #endregion

            Console.WriteLine();

            #region 17) Filter strings with an anonymous function
            Console.WriteLine("---- 17) Filter strings (anonymous function) ----");
            List<string> fruits = new List<string> { "apple", "banana", "avocado", "grape", "apricot" };
            List<string> startsWithA = DelegateHelpers.FilterStrings(fruits, delegate (string s) { return s.StartsWith("a"); });
            List<string> containsAn = DelegateHelpers.FilterStrings(fruits, delegate (string s) { return s.Contains("an"); });
            Console.WriteLine("Starts with 'a': " + string.Join(", ", startsWithA));
            Console.WriteLine("Contains 'an':   " + string.Join(", ", containsAn));
            // Q: How do anonymous functions improve code modularity/customization?
            // - The filtering CONDITION is supplied at the call site instead of
            //   being hardcoded inside FilterStrings, so the same method handles
            //   any condition without being touched — new conditions = new callers, not new code.
            #endregion

            Console.WriteLine();

            #region 18) Math operation on two ints using an anonymous function
            Console.WriteLine("---- 18) Int math (anonymous function) ----");
            Func<int, int, int> addAnon = delegate (int a, int b) { return a + b; };
            Func<int, int, int> subAnon = delegate (int a, int b) { return a - b; };
            Func<int, int, int> mulAnon = delegate (int a, int b) { return a * b; };
            Console.WriteLine($"Add: {DelegateHelpers.PerformIntOperation(8, 3, addAnon)}");
            Console.WriteLine($"Sub: {DelegateHelpers.PerformIntOperation(8, 3, subAnon)}");
            Console.WriteLine($"Mul: {DelegateHelpers.PerformIntOperation(8, 3, mulAnon)}");
            // Q: When should you prefer anonymous functions over named methods for math ops?
            // - When the logic is small, used once (or in very few places), and
            //   doesn't need a memorable name of its own — inlining it avoids
            //   cluttering the class with many tiny single-use methods.
            #endregion

            Console.WriteLine();

            #region 19) Filter strings with a lambda expression
            Console.WriteLine("---- 19) Filter strings (lambda) ----");
            List<string> longerThan3 = DelegateHelpers.FilterStrings(fruits, s => s.Length > 3);
            List<string> containsE = DelegateHelpers.FilterStrings(fruits, s => s.Contains("e"));
            Console.WriteLine("Length > 3:    " + string.Join(", ", longerThan3));
            Console.WriteLine("Contains 'e':  " + string.Join(", ", containsE));
            // Q: What makes lambda expressions an essential feature in modern C#?
            // - They let you pass behavior as data concisely (LINQ, event
            //   handlers, async continuations all lean on them), turning
            //   multi-line delegate boilerplate into a single readable expression.
            #endregion

            Console.WriteLine();

            #region 20) Math operation on two doubles using a lambda
            Console.WriteLine("---- 20) Double math (lambda) ----");
            Console.WriteLine($"Division:      {DelegateHelpers.PerformDoubleOperation(9, 2, (a, b) => a / b)}");
            Console.WriteLine($"Exponentiation: {DelegateHelpers.PerformDoubleOperation(2, 10, (a, b) => Math.Pow(a, b))}");
            // Q: How do lambdas enhance the expressiveness of math computations in C#?
            // - The formula reads almost like real math notation
            //   ("(a, b) => Math.Pow(a, b)") right where it's used, instead of a
            //   named method defined elsewhere that hides what's actually being computed.
            #endregion

            Console.WriteLine("\nDone.");
        }
    }
}
