using System;

namespace day01_csharp
{
    internal static class ArrayExtensions
    {
        // Problem 8: Sum() for int[] — manual loop, NOT the built-in LINQ Sum().
        public static int Sum(this int[] numbers)
        {
            int total = 0;

            if (numbers == null) return total;

            for (int i = 0; i < numbers.Length; i++)
            {
                total += numbers[i];
            }

            return total;
        }
    }
}
