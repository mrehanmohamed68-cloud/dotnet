using System;
using System.Collections.Generic;
using System.Linq;
using day10_G01;
using static day10_G01.ListGenerators;

namespace day02_csharp
{
    // NOTE: ProductList and CustomerList (with their Orders) already come
    // hardcoded inside ListGenerators.cs — no XML file is needed for any of
    // this. The 5 problems that required reading "dictionary_english.txt"
    // (Aggregate #5/#7/#10/#13 and Quantifiers #1) are intentionally SKIPPED
    // below, as requested.
    internal class Program
    {
        static void Main()
        {
            #region LINQ - Restriction Operators

            Console.WriteLine("==== Restriction Operators ====");

            // 1. Find all products that are out of stock.
            var outOfStockProducts = ProductList.Where(p => p.UnitsInStock == 0);
            Console.WriteLine("-- 1) Out of stock --");
            foreach (var p in outOfStockProducts) Console.WriteLine(p);

            // 2. Find all products that are in stock and cost more than 3.00 per unit.
            var inStockExpensive = ProductList.Where(p => p.UnitsInStock > 0 && p.UnitPrice > 3.00M);
            Console.WriteLine("-- 2) In stock & price > 3.00 --");
            foreach (var p in inStockExpensive) Console.WriteLine(p);

            // 3. Digits whose name is shorter than their value.
            string[] digitNames = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var shortNameDigits = digitNames.Where((name, value) => name.Length < value);
            Console.WriteLine("-- 3) Digit names shorter than their value --");
            foreach (var d in shortNameDigits) Console.WriteLine(d);

            #endregion

            Console.WriteLine();

            #region LINQ - Element Operators

            Console.WriteLine("==== Element Operators ====");

            // 1. Get first Product out of Stock.
            var firstOutOfStock = ProductList.First(p => p.UnitsInStock == 0);
            Console.WriteLine($"-- 1) First out of stock: {firstOutOfStock}");

            // 2. First product whose Price > 1000, or null if there's no match.
            var firstExpensiveOrNull = ProductList.FirstOrDefault(p => p.UnitPrice > 1000);
            Console.WriteLine($"-- 2) First price > 1000: {(firstExpensiveOrNull?.ToString() ?? "null (no match)")}");

            // 3. Retrieve the second number greater than 5.
            int[] arrElement = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var secondGreaterThan5 = arrElement.Where(n => n > 5).ElementAt(1);
            Console.WriteLine($"-- 3) Second number > 5: {secondGreaterThan5}");

            #endregion

            Console.WriteLine();

            #region LINQ - Aggregate Operators

            Console.WriteLine("==== Aggregate Operators ====");

            // 1. Count of odd numbers in the array.
            int[] arrAgg = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            int oddCount = arrAgg.Count(n => n % 2 != 0);
            Console.WriteLine($"-- 1) Odd count: {oddCount}");

            // 2. List of customers and how many orders each has.
            var customerOrderCounts = CustomerList.Select(c => new { c.Name, OrderCount = c.Orders.Length });
            Console.WriteLine("-- 2) Orders per customer --");
            foreach (var c in customerOrderCounts) Console.WriteLine($"{c.Name}: {c.OrderCount}");

            // 3. List of categories and how many products each.
            var categoryProductCounts = ProductList.GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, Count = g.Count() });
            Console.WriteLine("-- 3) Products per category --");
            foreach (var c in categoryProductCounts) Console.WriteLine($"{c.Category}: {c.Count}");

            // 4. Total of the numbers in an array.
            int total = arrAgg.Sum();
            Console.WriteLine($"-- 4) Total: {total}");

            // 5. SKIPPED — required reading dictionary_english.txt.

            // 6. Total units in stock for each product category.
            var unitsPerCategory = ProductList.GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, TotalUnits = g.Sum(p => p.UnitsInStock) });
            Console.WriteLine("-- 6) Units in stock per category --");
            foreach (var c in unitsPerCategory) Console.WriteLine($"{c.Category}: {c.TotalUnits}");

            // 7. SKIPPED — required reading dictionary_english.txt.

            // 8. Cheapest price among each category's products.
            var cheapestPerCategory = ProductList.GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, CheapestPrice = g.Min(p => p.UnitPrice) });
            Console.WriteLine("-- 8) Cheapest price per category --");
            foreach (var c in cheapestPerCategory) Console.WriteLine($"{c.Category}: {c.CheapestPrice}");

            // 9. Products with the cheapest price in each category (uses "let").
            var cheapestProductsPerCategory =
                from p in ProductList
                group p by p.Category into g
                let minPrice = g.Min(x => x.UnitPrice)
                select new { Category = g.Key, CheapestProducts = g.Where(x => x.UnitPrice == minPrice) };
            Console.WriteLine("-- 9) Cheapest product(s) per category --");
            foreach (var g in cheapestProductsPerCategory)
            {
                Console.WriteLine($"{g.Category}:");
                foreach (var p in g.CheapestProducts) Console.WriteLine($"   {p.ProductName} ({p.UnitPrice})");
            }

            // 10. SKIPPED — required reading dictionary_english.txt.

            // 11. Most expensive price among each category's products.
            var mostExpensivePerCategory = ProductList.GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, MostExpensivePrice = g.Max(p => p.UnitPrice) });
            Console.WriteLine("-- 11) Most expensive price per category --");
            foreach (var c in mostExpensivePerCategory) Console.WriteLine($"{c.Category}: {c.MostExpensivePrice}");

            // 12. Products with the most expensive price in each category.
            var mostExpensiveProductsPerCategory =
                from p in ProductList
                group p by p.Category into g
                let maxPrice = g.Max(x => x.UnitPrice)
                select new { Category = g.Key, MostExpensiveProducts = g.Where(x => x.UnitPrice == maxPrice) };
            Console.WriteLine("-- 12) Most expensive product(s) per category --");
            foreach (var g in mostExpensiveProductsPerCategory)
            {
                Console.WriteLine($"{g.Category}:");
                foreach (var p in g.MostExpensiveProducts) Console.WriteLine($"   {p.ProductName} ({p.UnitPrice})");
            }

            // 13. SKIPPED — required reading dictionary_english.txt.

            // 14. Average price of each category's products.
            var avgPricePerCategory = ProductList.GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, AveragePrice = g.Average(p => p.UnitPrice) });
            Console.WriteLine("-- 14) Average price per category --");
            foreach (var c in avgPricePerCategory) Console.WriteLine($"{c.Category}: {c.AveragePrice:F2}");

            #endregion

            Console.WriteLine();

            #region LINQ - Ordering Operators

            Console.WriteLine("==== Ordering Operators ====");

            // 1. Sort a list of products by name.
            var sortedByName = ProductList.OrderBy(p => p.ProductName);
            Console.WriteLine("-- 1) Products sorted by name (first 5 shown) --");
            foreach (var p in sortedByName.Take(5)) Console.WriteLine(p.ProductName);

            // 2. Custom comparer: case-insensitive sort of words.
            string[] wordsOrd = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
            var caseInsensitiveSorted = wordsOrd.OrderBy(w => w, StringComparer.OrdinalIgnoreCase);
            Console.WriteLine("-- 2) Case-insensitive sort --");
            foreach (var w in caseInsensitiveSorted) Console.WriteLine(w);

            // 3. Sort products by units in stock, highest to lowest.
            var byStockDesc = ProductList.OrderByDescending(p => p.UnitsInStock);
            Console.WriteLine("-- 3) Products by stock desc (first 5 shown) --");
            foreach (var p in byStockDesc.Take(5)) Console.WriteLine($"{p.ProductName}: {p.UnitsInStock}");

            // 4. Sort digits by name length, then alphabetically.
            string[] digitsOrd = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var digitsByLengthThenName = digitsOrd.OrderBy(d => d.Length).ThenBy(d => d);
            Console.WriteLine("-- 4) Digits by length then name --");
            foreach (var d in digitsByLengthThenName) Console.WriteLine(d);

            // 5. Sort first by word length, then case-insensitive alphabetically.
            var wordsByLengthThenCI = wordsOrd.OrderBy(w => w.Length).ThenBy(w => w, StringComparer.OrdinalIgnoreCase);
            Console.WriteLine("-- 5) Words by length then case-insensitive name --");
            foreach (var w in wordsByLengthThenCI) Console.WriteLine(w);

            // 6. Sort products by category, then by unit price, highest to lowest.
            var productsByCategoryThenPriceDesc = ProductList.OrderBy(p => p.Category).ThenByDescending(p => p.UnitPrice);
            Console.WriteLine("-- 6) Products by category then price desc (first 5 shown) --");
            foreach (var p in productsByCategoryThenPriceDesc.Take(5)) Console.WriteLine($"{p.Category} | {p.ProductName} | {p.UnitPrice}");

            // 7. Sort first by word length, then case-insensitive DESCENDING.
            var wordsByLengthThenCIDesc = wordsOrd.OrderBy(w => w.Length).ThenByDescending(w => w, StringComparer.OrdinalIgnoreCase);
            Console.WriteLine("-- 7) Words by length then case-insensitive name desc --");
            foreach (var w in wordsByLengthThenCIDesc) Console.WriteLine(w);

            // 8. Digits whose second letter is 'i', reversed from the original array order.
            var secondLetterIReversed = digitsOrd.Where(d => d.Length > 1 && d[1] == 'i').Reverse();
            Console.WriteLine("-- 8) Digits with 2nd letter 'i', reversed --");
            foreach (var d in secondLetterIReversed) Console.WriteLine(d);

            #endregion

            Console.WriteLine();

            #region LINQ - Transformation (Projection) Operators

            Console.WriteLine("==== Transformation Operators ====");

            // 1. Sequence of just the product names.
            var productNames = ProductList.Select(p => p.ProductName);
            Console.WriteLine("-- 1) Product names (first 5 shown) --");
            foreach (var name in productNames.Take(5)) Console.WriteLine(name);

            // 2. Uppercase and lowercase versions of each word (anonymous types).
            string[] wordsCase = { "aPPLE", "BlUeBeRrY", "cHeRry" };
            var upperLowerWords = wordsCase.Select(w => new { Upper = w.ToUpper(), Lower = w.ToLower() });
            Console.WriteLine("-- 2) Upper/Lower versions --");
            foreach (var w in upperLowerWords) Console.WriteLine($"{w.Upper} / {w.Lower}");

            // 3. Some Product properties, with UnitPrice renamed to Price.
            var productPriceView = ProductList.Select(p => new { p.ProductName, p.Category, Price = p.UnitPrice });
            Console.WriteLine("-- 3) Product projection (Price renamed, first 5 shown) --");
            foreach (var p in productPriceView.Take(5)) Console.WriteLine($"{p.ProductName} | {p.Category} | {p.Price}");

            // 4. Determine if the value of ints in an array matches their position.
            int[] arrInPlace = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var inPlaceCheck = arrInPlace.Select((num, index) => new { Number = num, InPlace = num == index });
            Console.WriteLine("-- 4) Number : In-place? --");
            foreach (var item in inPlaceCheck) Console.WriteLine($"{item.Number}: {item.InPlace}");

            // 5. All pairs where numbersA value < numbersB value.
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };
            var lessThanPairs = from a in numbersA
                                 from b in numbersB
                                 where a < b
                                 select new { a, b };
            Console.WriteLine("-- 5) Pairs where a < b --");
            foreach (var pair in lessThanPairs) Console.WriteLine($"{pair.a} is less than {pair.b}");

            // 6. All orders where the order total is less than 500.00.
            var smallOrders = from c in CustomerList
                               from o in c.Orders
                               where o.Total < 500.00
                               select new { c.Name, o.Id, o.Total };
            Console.WriteLine("-- 6) Orders with total < 500 --");
            foreach (var o in smallOrders) Console.WriteLine($"{o.Name} | Order {o.Id} | {o.Total}");

            // 7. All orders where the order was made in 1998 or later.
            var recentOrders = from c in CustomerList
                                from o in c.Orders
                                where o.OrderDate.Year >= 1998
                                select new { c.Name, o.Id, o.OrderDate };
            Console.WriteLine("-- 7) Orders from 1998 or later --");
            foreach (var o in recentOrders) Console.WriteLine($"{o.Name} | Order {o.Id} | {o.OrderDate.ToShortDateString()}");

            #endregion

            Console.WriteLine();

            #region LINQ - Partitioning Operators

            Console.WriteLine("==== Partitioning Operators ====");
            // NOTE: none of the sample customers in ListGenerators.cs have
            // City == "Washington", so problems 1 & 2 below correctly run —
            // they just return an empty sequence with this sample data.

            // 1. First 3 orders from customers in Washington.
            var first3WashingtonOrders = (from c in CustomerList
                                           where c.City == "Washington"
                                           from o in c.Orders
                                           select o).Take(3);
            Console.WriteLine("-- 1) First 3 Washington orders --");
            foreach (var o in first3WashingtonOrders) Console.WriteLine(o);

            // 2. All but the first 2 orders from customers in Washington.
            var skip2WashingtonOrders = (from c in CustomerList
                                          where c.City == "Washington"
                                          from o in c.Orders
                                          select o).Skip(2);
            Console.WriteLine("-- 2) Washington orders, skipping the first 2 --");
            foreach (var o in skip2WashingtonOrders) Console.WriteLine(o);

            // 3. Elements from the start until a number less than its position is hit.
            int[] numbersPart = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var takeWhileResult = numbersPart.TakeWhile((n, index) => n >= index);
            Console.WriteLine("-- 3) TakeWhile(n >= index) --");
            foreach (var n in takeWhileResult) Console.WriteLine(n);

            // 4. Elements starting from the first element divisible by 3.
            var skipWhileDivisibleBy3 = numbersPart.SkipWhile(n => n % 3 != 0);
            Console.WriteLine("-- 4) SkipWhile(not divisible by 3) --");
            foreach (var n in skipWhileDivisibleBy3) Console.WriteLine(n);

            // 5. Elements starting from the first element less than its position.
            var skipWhileLessThanPosition = numbersPart.SkipWhile((n, index) => n >= index);
            Console.WriteLine("-- 5) SkipWhile(n >= index) --");
            foreach (var n in skipWhileLessThanPosition) Console.WriteLine(n);

            #endregion

            Console.WriteLine();

            #region LINQ - Quantifiers

            Console.WriteLine("==== Quantifiers ====");

            // 1. SKIPPED — required reading dictionary_english.txt.

            // 2. Grouped products only for categories with at least one out-of-stock product.
            var categoriesWithOutOfStock = ProductList.GroupBy(p => p.Category)
                .Where(g => g.Any(p => p.UnitsInStock == 0));
            Console.WriteLine("-- 2) Categories with >= 1 out-of-stock product --");
            foreach (var g in categoriesWithOutOfStock)
            {
                Console.WriteLine($"{g.Key}:");
                foreach (var p in g) Console.WriteLine($"   {p.ProductName} (Stock: {p.UnitsInStock})");
            }

            // 3. Grouped products only for categories where ALL products are in stock.
            var categoriesAllInStock = ProductList.GroupBy(p => p.Category)
                .Where(g => g.All(p => p.UnitsInStock > 0));
            Console.WriteLine("-- 3) Categories fully in stock --");
            foreach (var g in categoriesAllInStock)
            {
                Console.WriteLine($"{g.Key}:");
                foreach (var p in g) Console.WriteLine($"   {p.ProductName} (Stock: {p.UnitsInStock})");
            }

            #endregion

            Console.WriteLine("\nDone.");
        }
    }
}
