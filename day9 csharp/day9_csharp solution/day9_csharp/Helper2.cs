using System;

namespace day9_csharp
{
    public static class Helper2<T>
    {
        public static int SearchArray(T[] array, T value)
        {
            for (int i = 0; i < array?.Length; i++)
            {
                if (value.Equals(array[i]))
                    return i;
            }
            return -1;
        }

        // Replaces every occurrence of oldValue with newValue in the given array.
        public static void ReplaceArray(T[] array, T oldValue, T newValue)
        {
            for (int i = 0; i < array?.Length; i++)
            {
                if (array[i].Equals(oldValue))
                    array[i] = newValue;
            }
        }
    }
}
