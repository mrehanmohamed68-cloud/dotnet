using System;

namespace day10_csharp
{
    // Problem 11: a custom delegate with signature (string) -> string.
    public delegate string StringTransformDelegate(string input);

    // Problem 12: a custom delegate with signature (int, int) -> int.
    public delegate int MathOperationDelegate(int a, int b);

    // Problem 13: a GENERIC delegate — T is the input type, TResult is the
    // output type, decided when the delegate is actually used.
    public delegate TResult TransformDelegate<T, TResult>(T input);
}
