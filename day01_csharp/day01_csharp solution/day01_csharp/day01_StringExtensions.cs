using System;

namespace day01_csharp
{
    internal static class StringExtensions
    {
        // Problem 6: IsPalindrome() — reads the same forwards and backwards.
        // Pure loop, no LINQ: two-pointer comparison from both ends inward.
        public static bool IsPalindrome(this string text)
        {
            if (text == null) return false;

            int left = 0;
            int right = text.Length - 1;

            while (left < right)
            {
                if (text[left] != text[right])
                    return false;

                left++;
                right--;
            }

            return true;
        }
    }
}
