using System;

namespace Problem04_SelectionSort
{
    public class Program
    {
        // Selection sort: repeatedly find the minimum of the unsorted
        // remainder and swap it into place.
        public static void SelectionSort(int[] numbers)
        {
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                int minIndex = i;

                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if (numbers[j] < numbers[minIndex])
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    int temp = numbers[i];
                    numbers[i] = numbers[minIndex];
                    numbers[minIndex] = temp;
                }
            }
        }

        public static void Main(string[] args)
        {
            // Same shape areas used in Problem02_ShapeSorting (as ints,
            // since SelectionSort works on int[]).
            int[] areas = { 16, 79, 24, 4, 13, 50 };

            Console.WriteLine("Before sorting: " + string.Join(", ", areas));

            SelectionSort(areas);

            Console.WriteLine("After sorting:  " + string.Join(", ", areas));
        }
    }
}
