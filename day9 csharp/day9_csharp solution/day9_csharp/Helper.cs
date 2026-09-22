using System;

namespace day9_csharp
{
    public static class Helper
    {
        // Constrained generic: T must implement IComparable<T> so CompareTo is guaranteed.
        public static T Max<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) >= 0 ? a : b;
        }
    }
}
