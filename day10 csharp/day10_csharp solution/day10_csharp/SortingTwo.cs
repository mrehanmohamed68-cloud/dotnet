using System;

namespace day10_csharp
{
    // Generic + DYNAMIC: unlike SortingAlgorithm<T>, this class carries no
    // IComparable constraint at all — the caller supplies the comparison
    // logic itself via a delegate, so the same Sort works for ints, strings,
    // Employees, or any custom ordering rule (ascending, descending, by
    // length, by multiple fields...) without changing this class.
    public static class SortingTwo<T>
    {
        // shouldSwap(a, b) returns true when a and b must be swapped.
        public static void Sort(T[] items, Func<T, T, bool> shouldSwap)
        {
            for (int i = 0; i < items.Length - 1; i++)
            {
                for (int j = 0; j < items.Length - 1 - i; j++)
                {
                    if (shouldSwap(items[j], items[j + 1]))
                    {
                        T temp = items[j];
                        items[j] = items[j + 1];
                        items[j + 1] = temp;
                    }
                }
            }
        }
    }
}
