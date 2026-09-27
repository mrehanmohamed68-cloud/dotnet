using System;
using System.Collections;
using System.Collections.Generic;

namespace day01_csharp
{
    // NOTE: per Task.pdf instructions, this whole file solves every problem
    // using loops/conditionals only — no LINQ methods (Where, Select,
    // FirstOrDefault, Sum, ...) are used anywhere below, even though this
    // topic (LINQ) is what the course module is building up to.
    internal class Program
    {
        static void Main()
        {
            #region 1) Basic var usage
            Console.WriteLine("---- 1) var basics ----");
            var number = 10;
            var text = "Mrehan";
            var price = 19.99;
            var isActive = true;
            var numbers = new int[] { 1, 2, 3 };

            Console.WriteLine($"number   -> {number.GetType()}");
            Console.WriteLine($"text     -> {text.GetType()}");
            Console.WriteLine($"price    -> {price.GetType()}");
            Console.WriteLine($"isActive -> {isActive.GetType()}");
            Console.WriteLine($"numbers  -> {numbers.GetType()}");
            #endregion

            Console.WriteLine();

            #region 2) var vs explicit type
            // Explicit typing:
            int explicitAge = 21;
            string explicitCity = "Giza";
            double explicitGpa = 3.75;

            // Same declarations using var:
            var inferredAge = 21;
            var inferredCity = "Giza";
            var inferredGpa = 3.75;

            Console.WriteLine("---- 2) var vs explicit ----");
            Console.WriteLine($"{explicitAge} {explicitCity} {explicitGpa}");
            Console.WriteLine($"{inferredAge} {inferredCity} {inferredGpa}");

            // Why the result is exactly the same at compile time:
            // "var" does not create a dynamic/loosely typed variable — it just
            // tells the COMPILER "figure out the type yourself from the value
            // on the right-hand side." The compiler substitutes the real type
            // (int/string/double here) at compile time, so the generated IL
            // (the compiled code) is byte-for-byte identical either way.
            // "var" is only a source-code convenience; it changes nothing
            // about how the program runs.
            #endregion

            Console.WriteLine();

            #region 3) Anonymous type basics
            Console.WriteLine("---- 3) Anonymous Product ----");
            var product = new { Name = "Keyboard", Price = 450.50, Quantity = 3 };
            Console.WriteLine($"Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}");
            #endregion

            Console.WriteLine();

            #region 4) Array of anonymous types
            Console.WriteLine("---- 4) Array of anonymous students ----");
            var students = new[]
            {
                new { Name = "Ali", Grade = "A" },
                new { Name = "Sara", Grade = "B" },
                new { Name = "Omar", Grade = "A" }
            };

            for (int i = 0; i < students.Length; i++)
            {
                Console.WriteLine($"Student: {students[i].Name}, Grade: {students[i].Grade}");
            }
            #endregion

            Console.WriteLine();

            #region 5) Nested anonymous type
            Console.WriteLine("---- 5) Nested anonymous Order/Customer ----");
            var order = new
            {
                OrderId = 501,
                Total = 1250.75,
                Customer = new { Name = "Mrehan Mohamed", City = "Giza" }
            };

            Console.WriteLine($"Order #{order.OrderId}, Total: {order.Total}, " +
                               $"Customer: {order.Customer.Name} ({order.Customer.City})");
            #endregion

            Console.WriteLine();

            #region 6) String extension: IsPalindrome
            Console.WriteLine("---- 6) IsPalindrome ----");
            string[] wordsToCheck = { "level", "hello", "madam", "csharp" };
            for (int i = 0; i < wordsToCheck.Length; i++)
            {
                Console.WriteLine($"{wordsToCheck[i]} -> {wordsToCheck[i].IsPalindrome()}");
            }
            #endregion

            Console.WriteLine();

            #region 7) Int extension: IsPrime
            Console.WriteLine("---- 7) IsPrime ----");
            int[] numsToCheck = { 1, 2, 7, 10, 17, 20 };
            for (int i = 0; i < numsToCheck.Length; i++)
            {
                Console.WriteLine($"{numsToCheck[i]} -> {numsToCheck[i].IsPrime()}");
            }
            #endregion

            Console.WriteLine();

            #region 8) Array extension: Sum
            Console.WriteLine("---- 8) Array Sum (manual, not LINQ Sum) ----");
            int[] valuesToSum = { 5, 10, 15, 20 };
            Console.WriteLine($"Sum = {valuesToSum.Sum()}");
            #endregion

            Console.WriteLine();

            #region 9) List<string> basics (add/remove/search via loop)
            Console.WriteLine("---- 9) List<string> employee names ----");
            List<string> employeeNames = new List<string>();
            employeeNames.Add("Ali");
            employeeNames.Add("Sara");
            employeeNames.Add("Omar");
            employeeNames.Add("Hana");

            employeeNames.Remove("Omar"); // remove by value

            // Manual loop search (no LINQ Contains/FirstOrDefault):
            string searchName = "Sara";
            bool found = false;
            for (int i = 0; i < employeeNames.Count; i++)
            {
                if (employeeNames[i] == searchName)
                {
                    found = true;
                    break;
                }
            }
            Console.WriteLine($"'{searchName}' found? {found}");

            Console.WriteLine("Final list:");
            for (int i = 0; i < employeeNames.Count; i++)
            {
                Console.WriteLine($"- {employeeNames[i]}");
            }
            #endregion

            Console.WriteLine();

            #region 10) List<Employee> filtered by salary (loop, no Where)
            Console.WriteLine("---- 10) Employees with salary above threshold ----");
            List<Employee> employees = new List<Employee>
            {
                new Employee("Ali", 6000),
                new Employee("Sara", 9000),
                new Employee("Omar", 4500),
                new Employee("Hana", 12000)
            };

            double salaryThreshold = 5000;
            foreach (Employee emp in employees)
            {
                if (emp.Salary > salaryThreshold)
                {
                    Console.WriteLine(emp);
                }
            }
            #endregion

            Console.WriteLine();

            #region 11) Dictionary<string,int> basics
            Console.WriteLine("---- 11) Dictionary<string,int> product prices ----");
            Dictionary<string, int> productPrices = new Dictionary<string, int>();
            productPrices.Add("Chai", 18);
            productPrices.Add("Tofu", 23);
            productPrices.Add("Konbu", 6);

            foreach (KeyValuePair<string, int> pair in productPrices)
            {
                Console.WriteLine($"{pair.Key} -> {pair.Value}");
            }
            #endregion

            Console.WriteLine();

            #region 12) Dictionary<int,string> lookup with TryGetValue
            Console.WriteLine("---- 12) Dictionary<int,string> student lookup ----");
            Dictionary<int, string> studentsById = new Dictionary<int, string>
            {
                { 1, "Ali" },
                { 2, "Sara" },
                { 3, "Omar" }
            };

            // Console.ReadLine() would normally read the ID from the user;
            // using a fixed test value here so the demo runs unattended.
            int searchId = 2; // e.g. int.Parse(Console.ReadLine());
            if (studentsById.TryGetValue(searchId, out string studentName))
            {
                Console.WriteLine($"Student with ID {searchId}: {studentName}");
            }
            else
            {
                Console.WriteLine($"No student found with ID {searchId}");
            }
            #endregion

            Console.WriteLine();

            #region 13) Hashtable basics
            Console.WriteLine("---- 13) Hashtable (mixed key/value types) ----");
            Hashtable mixedTable = new Hashtable();
            mixedTable.Add(1, "One");
            mixedTable.Add("two", 2);
            mixedTable.Add(3.0, "Three-Point-Zero");

            foreach (DictionaryEntry entry in mixedTable)
            {
                Console.WriteLine($"Key: {entry.Key} ({entry.Key.GetType().Name}), " +
                                   $"Value: {entry.Value} ({entry.Value.GetType().Name})");
            }
            #endregion

            Console.WriteLine();

            #region 14) Dictionary vs Hashtable
            Console.WriteLine("---- 14) Dictionary<string,string> vs Hashtable ----");
            Dictionary<string, string> typedCapitals = new Dictionary<string, string>
            {
                { "Egypt", "Cairo" },
                { "France", "Paris" }
            };

            Hashtable untypedCapitals = new Hashtable
            {
                { "Egypt", "Cairo" },
                { "France", "Paris" }
            };

            foreach (KeyValuePair<string, string> pair in typedCapitals)
                Console.WriteLine($"[Dictionary] {pair.Key} -> {pair.Value}");

            foreach (DictionaryEntry entry in untypedCapitals)
                Console.WriteLine($"[Hashtable] {entry.Key} -> {entry.Value}");

            // Observed difference:
            // Dictionary<string,string> is TYPED — the compiler enforces that
            // every key/value is a string, so pair.Key/pair.Value come out
            // already as strings (no casting needed) and a wrong type is a
            // compile-time error.
            // Hashtable stores everything as "object" — entry.Key/entry.Value
            // must be cast to the real type before use, there's no compile-time
            // type safety, and value types get boxed on every insert. That's
            // exactly why Dictionary<TKey,TValue> is the modern, preferred choice.
            #endregion

            Console.WriteLine("\nDone.");

            #region Short Write-Ups (No Coding)

            // 1) Difference between a delegate and a lambda expression?
            // A delegate is a TYPE — it defines a method signature (return
            // type + parameters) that any matching method or function can be
            // assigned to, like "public delegate int MathOp(int a, int b);".
            // A lambda expression ("(a, b) => a + b") is not a type at all —
            // it's a compact way to WRITE a method body inline. A lambda's
            // only purpose is to be assigned to something that expects a
            // delegate type (a custom delegate, or a built-in one like
            // Func<T,TResult>/Action<T>/Predicate<T>). In short: delegate =
            // the "shape" a function must match; lambda = one concise way of
            // writing a function that fits that shape.

            // 2) Why is var still statically typed even though the type
            //    isn't written explicitly?
            // Because the compiler determines the exact type from the
            // right-hand side expression AT COMPILE TIME (not at runtime),
            // and that type is then fixed for the rest of the variable's
            // scope — you can never reassign it to a different, incompatible
            // type afterwards (e.g. "var x = 5; x = "hi";" is a compile
            // error). "var" is purely a syntax shortcut for the compiler to
            // infer the type; it is NOT the same as a dynamically typed
            // variable (like "dynamic"), whose type is only resolved at
            // runtime and can change.

            // 3) One real scenario where an anonymous type is more
            //    convenient than a full class?
            // A quick, throwaway projection of a few fields for local use —
            // for example, building a temporary result to print or pass
            // around within a single method (like the Product/Order examples
            // above), where creating and maintaining a whole named class just
            // for that one-off shape would be unnecessary overhead. It's most
            // valuable when the data shape is used briefly in one place and
            // never needs to be passed across method boundaries or reused
            // elsewhere in the codebase.

            #endregion
        }
    }
}
