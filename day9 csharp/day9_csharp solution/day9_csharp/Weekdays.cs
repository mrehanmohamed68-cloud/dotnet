using System;

namespace day9_csharp
{
    // Explicitly assigning Monday = 1 makes the rest (Tuesday..Friday) auto-increment
    // starting from 2, so the values map to real-world day numbers instead of
    // the default 0-based sequence.
    public enum Weekdays
    {
        Monday = 1,
        Tuesday,
        Wednesday,
        Thursday,
        Friday
    }
}
