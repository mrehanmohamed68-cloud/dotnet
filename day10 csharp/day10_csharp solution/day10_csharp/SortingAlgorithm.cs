using System;

namespace day10_csharp
{
    // Generic + constrained: T must only be comparable (for sorting).
    // NOTE: ICloneable is intentionally NOT a class-level constraint — int
    // (and several other built-in types) do NOT implement ICloneable in
    // modern .NET, so requiring it here would make SortingAlgorithm<int>
    // fail to compile. Instead, Problem 10's clone requirement is scoped to
    // its own method below, with its own, narrower generic type parameter.
    public static class SortingAlgorithm<T> where T : IComparable<T>
    {
        // Problem 1: generic ascending bubble sort — works for ANY T that fits the constraint.
        public static void Sort(T[] items)
        {
            for (int i = 0; i < items.Length - 1; i++)
            {
                for (int j = 0; j < items.Length - 1 - i; j++)
                {
                    if (items[j].CompareTo(items[j + 1]) > 0)
                    {
                        T temp = items[j];
                        items[j] = items[j + 1];
                        items[j + 1] = temp;
                    }
                }
            }
        }

        // Problem 9: default(T) — returns 0/false/default-struct or null depending
        // on whether T is a value type or a reference type; you can't just write
        // "null" or "0" generically because the compiler doesn't know which T
        // will be used. Return type is T? since that result CAN legitimately be null.
        public static T? GetDefault()
        {
            return default(T);
        }

        // Problem 7: standalone generic Swap method. It uses its OWN type
        // parameter (TItem) instead of the class's T, so it has no
        // IComparable requirement at all — it can swap literally anything, unconstrained.
        public static void Swap<TItem>(ref TItem a, ref TItem b)
        {
            TItem temp = a;
            a = b;
            b = temp;
        }

        // Problem 10: clone every element (via ICloneable) into a new array
        // before sorting, so the original array/objects are left untouched.
        // TC gets its OWN "where TC : ICloneable" constraint instead of adding
        // ICloneable to the whole class — so SortingAlgorithm<int> still
        // compiles fine, and only types that actually implement ICloneable
        // (like Employee/Manager) can be passed here.
        public static TC[] CloneArray<TC>(TC[] items) where TC : ICloneable
        {
            TC[] clone = new TC[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                clone[i] = (TC)items[i].Clone();
            }
            return clone;
        }
    }
}
