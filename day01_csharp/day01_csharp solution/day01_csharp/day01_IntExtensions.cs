using System;

namespace day01_csharp
{
    internal static class IntExtensions
    {
        // Problem 7: IsPrime() — checks primality with a plain loop.
        public static bool IsPrime(this int number)
        {
            if (number < 2) return false;

            for (int divisor = 2; divisor < number; divisor++)
            {
                if (number % divisor == 0)
                    return false;
            }

            return true;
        }
    }
}
