using System;

namespace day9_csharp
{
    public static class ArrayHelper
    {
        // Problem 1: Generic method to reverse an array.
        // Works for any type (int, string, custom objects, ...) because it
        // never inspects the element values — it just repositions them.
        public static T[] Reverse<T>(T[] array)
        {
            if (array == null) return null;

            T[] result = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                result[i] = array[array.Length - 1 - i];
            }
            return result;
        }

        // Problem 3: Generic method to swap two elements in an array by index.
        public static void Swap<T>(T[] array, int indexA, int indexB)
        {
            if (array == null) return;
            if (indexA < 0 || indexA >= array.Length || indexB < 0 || indexB >= array.Length)
                throw new IndexOutOfRangeException("Index out of array bounds.");

            T temp = array[indexA];
            array[indexA] = array[indexB];
            array[indexB] = temp;
        }

        // Problem 4: Generic method to find the maximum element in an array.
        // Constrained to IComparable so CompareTo is guaranteed to exist.
        public static T FindMax<T>(T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length == 0)
                throw new ArgumentException("Array must not be null or empty.");

            T max = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i].CompareTo(max) > 0)
                    max = array[i];
            }
            return max;
        }
    }
}
