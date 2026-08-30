using System;
using System.Drawing;
using System.Text;

namespace CsharpDay03Giza01
{
    class Program
    {
        // generic-ish print helper (uses object, so it accepts any type - boxing happens for value types)
        public static void Print(object item)
        {
            Console.WriteLine(item);
        }

        static void Main(string[] args)
        {
            Problem1_ParseVsConvert();
            Problem3_TryParse();
            Problem5_ObjectHashCode();
            Problem7_ReferenceEquality();
            Problem9_StringImmutability();
            Problem11_StringBuilderMutability();
            Problem13_SumFormatting();
            Problem15_StringBuilderOperations();

            Console.WriteLine("\n=== End of Assignment ===");
        }

        // ------------------------------------------------------------
        // Problem 1: string -> int with int.Parse and Convert.ToInt32
        // ------------------------------------------------------------
        static void Problem1_ParseVsConvert()
        {
            Console.WriteLine("---- Problem 1: Parse vs Convert.ToInt32 ----");
            Console.Write("Enter a number: ");
            string input = Console.ReadLine();

            try
            {
                int viaParse = int.Parse(input);
                int viaConvert = Convert.ToInt32(input);

                Console.WriteLine($"int.Parse result: {viaParse}");
                Console.WriteLine($"Convert.ToInt32 result: {viaConvert}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input: not a valid number.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Invalid input: number out of int range.");
            }
        }

        // Question (Problem 1): int.Parse vs Convert.ToInt32 with null input
        // - int.Parse(null) throws an ArgumentNullException - it does not tolerate null at all.
        // - Convert.ToInt32(null) does NOT throw; it returns 0 (Convert treats null as the
        //   type's default value for numeric conversions). This is the key practical
        //   difference: Convert is more forgiving of null, Parse is strict.

        // ------------------------------------------------------------
        // Problem 3: TryParse
        // ------------------------------------------------------------
        static void Problem3_TryParse()
        {
            Console.WriteLine("\n---- Problem 3: TryParse ----");
            Console.Write("Enter a number: ");
            string input = Console.ReadLine();

            bool isValid = int.TryParse(input, out int number);

            if (isValid)
                Console.WriteLine($"Valid number: {number}");
            else
                Console.WriteLine("Error: input is not a valid integer.");
        }

        // Question (Problem 3): Why is TryParse recommended over Parse in user-facing apps?
        // - No exceptions for bad input: TryParse returns a bool instead of throwing,
        //   which is both cleaner control flow and cheaper (exceptions are expensive).
        // - User input is inherently untrustworthy - TryParse lets you handle invalid
        //   input gracefully (show a message, ask again) instead of crashing the flow
        //   with a try/catch for something that is an EXPECTED case, not an exceptional one.

        // ------------------------------------------------------------
        // Problem 5: object variable holding different types + GetHashCode
        // ------------------------------------------------------------
        static void Problem5_ObjectHashCode()
        {
            Console.WriteLine("\n---- Problem 5: object + GetHashCode ----");

            object obj = 10; // boxing: int value copied onto the heap
            Console.WriteLine($"int -> {obj}, hash: {obj.GetHashCode()}");

            obj = "Ali"; // now holds a string reference
            Console.WriteLine($"string -> {obj}, hash: {obj.GetHashCode()}");

            obj = 3.14; // boxing: double value copied onto the heap
            Console.WriteLine($"double -> {obj}, hash: {obj.GetHashCode()}");
        }

        // Question (Problem 5): The real purpose of GetHashCode()
        // GetHashCode() returns an int used to place an object into a "bucket" inside
        // hash-based collections like Dictionary<TKey,TValue> and HashSet<T>. It is NOT
        // meant to be a unique identifier for the object - different objects can share
        // the same hash code (a "collision"), and equal objects (by Equals) MUST return
        // the same hash code. Its real purpose is fast lookup: instead of scanning every
        // item (O(n)), the collection jumps straight to the right bucket and only
        // compares items within it, giving near O(1) average lookup time.

        // ------------------------------------------------------------
        // Problem 7: Reference equality - two references, same object
        // ------------------------------------------------------------
        static void Problem7_ReferenceEquality()
        {
            Console.WriteLine("\n---- Problem 7: Reference equality ----");

            Point p1 = new Point();
            p1.X = 5;

            Point p2 = p1; // p2 is a second reference to the SAME object

            Console.WriteLine($"Before -> p1.X: {p1.X}, p2.X: {p2.X}");

            p2.X = 50; // modify through p2

            Console.WriteLine($"After  -> p1.X: {p1.X}, p2.X: {p2.X}");
            // Both print 50, because p1 and p2 point to the exact same object.

            Console.WriteLine($"ReferenceEquals(p1, p2): {object.ReferenceEquals(p1, p2)}"); // True
        }

        // Question (Problem 7): Significance of reference equality in .NET
        // Reference equality answers "are these two variables pointing to the exact same
        // object in memory?" - as opposed to value equality, which asks "do these two
        // objects represent the same data?" (checked via Equals() / ==, which can be
        // overridden). This distinction matters for:
        // - Correctly reasoning about shared mutable state (like p1/p2 above).
        // - Caching/identity scenarios (e.g. singleton checks, object pools).
        // - Avoiding subtle bugs where a developer assumes a "copy" was made when in
        //   fact only a reference was copied.
        // Object.ReferenceEquals(a, b) explicitly checks this, bypassing any custom
        // Equals() override a class might define.

        // ------------------------------------------------------------
        // Problem 9: String immutability - concatenation + hash codes
        // ------------------------------------------------------------
        static void Problem9_StringImmutability()
        {
            Console.WriteLine("\n---- Problem 9: String immutability ----");

            string s = "Hi";
            Console.WriteLine($"Before -> value: \"{s}\", hash: {s.GetHashCode()}");

            s = s + " Willy"; // this does NOT modify the original string;
                              // it creates a brand new string object and reassigns s to it.
            Console.WriteLine($"After  -> value: \"{s}\", hash: {s.GetHashCode()}");
            // The hash code changes, proving a new object was created, not the old one mutated.
        }

        // Question (Problem 9): Why is string immutable in C#?
        // - Safety & predictability: once created, a string's value can never change
        //   underneath you, even if multiple references point to it - no defensive
        //   copying needed when passing strings around.
        // - Thread safety: immutable objects can be freely shared across threads with
        //   no risk of one thread's changes corrupting another's view of the data.
        // - Enables string interning (the string pool): since strings never change,
        //   the CLR can safely have many variables share one underlying string object
        //   in memory, saving space.
        // - Security: strings are used extensively as dictionary keys and in security
        //   contexts (e.g., file paths, connection strings); immutability prevents
        //   them from being altered after validation.

        // ------------------------------------------------------------
        // Problem 11: StringBuilder mutability
        // ------------------------------------------------------------
        static void Problem11_StringBuilderMutability()
        {
            Console.WriteLine("\n---- Problem 11: StringBuilder mutability ----");

            StringBuilder sb = new StringBuilder("Hi");
            Console.WriteLine($"Before -> value: \"{sb}\", hash: {sb.GetHashCode()}");

            sb.Append(" Willy"); // modifies the SAME underlying object in place

            Console.WriteLine($"After  -> value: \"{sb}\", hash: {sb.GetHashCode()}");
            // The hash code stays the same, proving it's still the same object,
            // just with different internal content.
        }

        // Question: How does StringBuilder address the inefficiencies of string concatenation?
        // Every time you concatenate strings with "+", C# creates a brand new string
        // object (since strings are immutable) and copies both old strings' characters
        // into it - repeated concatenation in a loop means repeated full copies, which
        // is O(n^2) overall for n concatenations. StringBuilder instead keeps an
        // internal, resizable character buffer and appends/inserts/removes directly in
        // that buffer, only reallocating (doubling capacity) when the buffer is full -
        // avoiding a full copy on every single modification.

        // Question: Why is StringBuilder faster for large-scale string modifications?
        // Because its amortized cost per append is close to O(1) (occasional buffer
        // resizes aside), versus string concatenation's O(n) cost per operation (a full
        // copy each time) - which compounds to O(n^2) across many operations. For a
        // handful of concatenations the difference is negligible; for loops doing
        // hundreds/thousands of modifications, StringBuilder is dramatically faster.

        // ------------------------------------------------------------
        // Problem 13: Sum via 3 formatting approaches
        // ------------------------------------------------------------
        static void Problem13_SumFormatting()
        {
            Console.WriteLine("\n---- Problem 13: Sum with 3 formatting styles ----");
            Console.Write("Enter first number: ");
            int input1 = int.Parse(Console.ReadLine());
            Console.Write("Enter second number: ");
            int input2 = int.Parse(Console.ReadLine());

            int sum = input1 + input2;

            // 1) Concatenation (+)
            Console.WriteLine("Sum is " + input1 + "+" + input2 + "=" + sum);

            // 2) Composite formatting
            Console.WriteLine(string.Format("Sum is {0}+{1}={2}", input1, input2, sum));

            // 3) String interpolation
            Console.WriteLine($"Sum is {input1}+{input2}={sum}");
        }

        // Question (Problem 13): Which formatting method is most used, and why?
        // String interpolation ($"...") is the most commonly used and recommended
        // approach in modern C#. It reads almost like natural text (values are written
        // inline where they appear in the string), is far less error-prone than manual
        // concatenation (no juggling + and quotes), and is more concise than composite
        // formatting's {0}/{1} placeholders which require matching indices to a separate
        // argument list. Under the hood, the compiler actually translates interpolated
        // strings into a similar mechanism to string.Format, so there's no performance
        // penalty for the improved readability.

        // ------------------------------------------------------------
        // Problem 15: StringBuilder append/replace/insert/remove
        // ------------------------------------------------------------
        static void Problem15_StringBuilderOperations()
        {
            Console.WriteLine("\n---- Problem 15: StringBuilder operations ----");

            StringBuilder sb = new StringBuilder("Hello");
            Console.WriteLine($"Start:   {sb}");

            sb.Append(" World");
            Console.WriteLine($"Append:  {sb}");

            sb.Replace("World", "C#");
            Console.WriteLine($"Replace: {sb}");

            sb.Insert(0, "Say: ");
            Console.WriteLine($"Insert:  {sb}");

            sb.Remove(0, 5); // removes "Say: "
            Console.WriteLine($"Remove:  {sb}");
        }

        // Question: Explain how StringBuilder is designed to handle frequent
        // modifications compared to strings.
        // StringBuilder is backed by an internal, mutable array of characters with
        // spare capacity beyond its current length. Append/Insert/Remove/Replace
        // operate directly on this buffer, shifting characters in place when needed,
        // without allocating a brand-new object for every single change. Only when the
        // buffer's capacity is exceeded does it allocate a larger internal array
        // (typically doubling), copying the existing content once. This design trades
        // a small amount of extra memory (unused capacity) for far fewer allocations
        // and copies overall - exactly the opposite trade-off of the immutable string,
        // which guarantees safety/sharing at the cost of a new allocation per change.
    }
}