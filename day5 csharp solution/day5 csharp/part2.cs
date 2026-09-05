using System;
using System.Text;

namespace day5_csharp
{
    internal partial class Program
    {
        #region Part 02 menu
        private static void RunPart02Menu()
        {
            bool back = false;

            while (!back)
            {
                Console.WriteLine();
                Console.WriteLine("=========================================");
                Console.WriteLine(" Part 02 - Exercises Menu");
                Console.WriteLine("=========================================");
                Console.WriteLine(" 1 - Print numbers in a range (1..N)");
                Console.WriteLine(" 2 - Multiplication table (1..12)");
                Console.WriteLine(" 3 - List even numbers (1..N)");
                Console.WriteLine(" 4 - Exponentiation (Base ^ Power)");
                Console.WriteLine(" 5 - Reverse a text string");
                Console.WriteLine(" 6 - Reverse an integer's digits");
                Console.WriteLine(" 7 - Longest distance between matching elements");
                Console.WriteLine(" 8 - Reverse words in a sentence");
                Console.WriteLine(" 0 - Back to main menu");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": PrintRange(); break;
                    case "2": PrintMultiplicationTable(); break;
                    case "3": PrintEvenNumbers(); break;
                    case "4": ComputeExponentiation(); break;
                    case "5": ReverseTextString(); break;
                    case "6": ReverseInteger(); break;
                    case "7": FindLongestDistance(); break;
                    case "8": ReverseWordsInSentence(); break;
                    case "0": back = true; break;
                    default: Console.WriteLine("Invalid option."); break;
                }
            }
        }
        #endregion

        #region 1 - Print numbers in a range (1..N)
        public static void PrintRange()
        {
            Console.Write("Enter a positive integer: ");
            int n = int.Parse(Console.ReadLine());

            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= n; i++)
            {
                sb.Append(i);
                if (i < n)
                    sb.Append(", ");
            }

            Console.WriteLine(sb.ToString());
        }
        #endregion

        #region 2 - Multiplication table (1..12)
        public static void PrintMultiplicationTable()
        {
            Console.Write("Enter an integer: ");
            int n = int.Parse(Console.ReadLine());

            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= 12; i++)
            {
                sb.Append(n * i);
                if (i < 12)
                    sb.Append(", ");
            }

            Console.WriteLine(sb.ToString());
        }
        #endregion

        #region 3 - List even numbers (1..N)
        public static void PrintEvenNumbers()
        {
            Console.Write("Enter a number: ");
            int n = int.Parse(Console.ReadLine());

            StringBuilder sb = new StringBuilder();
            bool first = true;
            for (int i = 1; i <= n; i++)
            {
                if (i % 2 == 0)
                {
                    if (!first)
                        sb.Append(", ");
                    sb.Append(i);
                    first = false;
                }
            }

            Console.WriteLine(sb.ToString());
        }
        #endregion

        #region 4 - Exponentiation (Base ^ Power)
        public static void ComputeExponentiation()
        {
            Console.Write("Enter the base number: ");
            int baseNum = int.Parse(Console.ReadLine());

            Console.Write("Enter the power (exponent): ");
            int power = int.Parse(Console.ReadLine());

            long result = 1;
            for (int i = 0; i < power; i++)
            {
                result *= baseNum;
            }

            Console.WriteLine($"{baseNum}^{power} = {result}");
        }
        #endregion

        #region 5 - Reverse a text string
        public static void ReverseTextString()
        {
            Console.Write("Enter a string: ");
            string text = Console.ReadLine();

            char[] chars = text.ToCharArray();
            Array.Reverse(chars);
            string reversed = new string(chars);

            Console.WriteLine(reversed);
        }
        #endregion

        #region 6 - Reverse an integer's digits
        public static void ReverseInteger()
        {
            Console.Write("Enter an integer: ");
            int number = int.Parse(Console.ReadLine());

            bool isNegative = number < 0;
            number = Math.Abs(number);

            long reversed = 0;
            while (number > 0)
            {
                int digit = number % 10;
                reversed = reversed * 10 + digit;
                number /= 10;
            }

            if (isNegative)
                reversed = -reversed;

            Console.WriteLine(reversed);
        }
        #endregion

        #region 7 - Longest distance between matching elements
        // Distance = number of cells strictly between the two matching elements
        // (i.e. lastIndex - firstIndex - 1), taken as the maximum over all values
        // that appear more than once in the array.
        public static void FindLongestDistance()
        {
            Console.Write("Enter the number of elements in the array: ");
            int size = int.Parse(Console.ReadLine());

            int[] arr = new int[size];
            Console.WriteLine("Enter the array elements:");
            for (int i = 0; i < size; i++)
            {
                Console.Write($"arr[{i}] = ");
                arr[i] = int.Parse(Console.ReadLine());
            }

            int maxDistance = -1;
            int valueWithMaxDistance = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                int firstIndex = -1;
                int lastIndex = -1;

                for (int j = 0; j < arr.Length; j++)
                {
                    if (arr[j] == arr[i])
                    {
                        if (firstIndex == -1)
                            firstIndex = j;
                        lastIndex = j;
                    }
                }

                if (firstIndex != lastIndex) // value appears more than once
                {
                    int distance = lastIndex - firstIndex - 1;
                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                        valueWithMaxDistance = arr[i];
                    }
                }
            }

            if (maxDistance == -1)
                Console.WriteLine("No repeated elements found in the array.");
            else
                Console.WriteLine($"Longest distance = {maxDistance} (between the two farthest occurrences of {valueWithMaxDistance})");
        }
        #endregion

        #region 8 - Reverse words in a sentence
        public static void ReverseWordsInSentence()
        {
            Console.Write("Enter a sentence: ");
            string sentence = Console.ReadLine();

            string[] words = sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Array.Reverse(words);

            Console.WriteLine(string.Join(" ", words));
        }
        #endregion
    }
}
