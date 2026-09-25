using System;
using System.Collections.Generic;

namespace day10_csharp
{
    public static class DelegateHelpers
    {
        // Problem 11: applies a custom string->string delegate to every item.
        public static List<string> ApplyTransform(List<string> items, StringTransformDelegate transform)
        {
            List<string> result = new List<string>();
            foreach (string item in items)
                result.Add(transform(item));
            return result;
        }

        // Problem 12: performs whatever math operation the delegate defines.
        public static int PerformOperation(int a, int b, MathOperationDelegate operation)
        {
            return operation(a, b);
        }

        // Problem 13: transforms List<T> into List<TResult> using the generic delegate.
        public static List<TResult> TransformList<T, TResult>(List<T> items, TransformDelegate<T, TResult> transform)
        {
            List<TResult> result = new List<TResult>();
            foreach (T item in items)
                result.Add(transform(item));
            return result;
        }

        // Problem 14: applies a built-in Func<int,int> to every element.
        public static List<int> ApplyFunc(List<int> items, Func<int, int> func)
        {
            List<int> result = new List<int>();
            foreach (int item in items)
                result.Add(func(item));
            return result;
        }

        // Problem 15: applies a built-in Action<string> to every element (no return value).
        public static void ApplyAction(List<string> items, Action<string> action)
        {
            foreach (string item in items)
                action(item);
        }

        // Problem 16: keeps only the items that satisfy the Predicate<int>.
        public static List<int> FilterList(List<int> items, Predicate<int> predicate)
        {
            List<int> result = new List<int>();
            foreach (int item in items)
                if (predicate(item))
                    result.Add(item);
            return result;
        }

        // Problems 17 & 19: filter strings by any condition (anonymous function OR lambda).
        public static List<string> FilterStrings(List<string> items, Func<string, bool> condition)
        {
            List<string> result = new List<string>();
            foreach (string item in items)
                if (condition(item))
                    result.Add(item);
            return result;
        }

        // Problem 18: math operation on two ints (fed an anonymous function).
        public static int PerformIntOperation(int a, int b, Func<int, int, int> operation)
        {
            return operation(a, b);
        }

        // Problem 20: math operation on two doubles (fed a lambda expression).
        public static double PerformDoubleOperation(double a, double b, Func<double, double, double> operation)
        {
            return operation(a, b);
        }
    }
}
