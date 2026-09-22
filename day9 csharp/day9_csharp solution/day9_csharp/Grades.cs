using System;

namespace day9_csharp
{
    // Underlying type changed from the default int to short.
    // F is explicitly set to -1 to represent "no valid grade".
    public enum Grades : short
    {
        A = 90,
        B = 80,
        C = 70,
        D = 60,
        F = -1
    }
}
