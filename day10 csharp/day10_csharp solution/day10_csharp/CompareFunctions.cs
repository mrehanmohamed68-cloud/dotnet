using System;

namespace day10_csharp
{
    public static class CompareFunctions
    {
        // Problem 3: sort strings by length ascending.
        public static bool CompareStringLengthAsc(string x, string y)
        {
            return x?.Length > y?.Length;
        }

        // Problem 5: sort Employees by Name length.
        public static bool CompareEmployeeByNameLength(Employee x, Employee y)
        {
            return x.Name.Length > y.Name.Length;
        }

        // Problem 8: multi-criteria — Salary first, Name as the tiebreaker.
        public static bool CompareEmployeeSalaryThenName(Employee x, Employee y)
        {
            if (x.Salary != y.Salary)
                return x.Salary > y.Salary;

            return string.Compare(x.Name, y.Name, StringComparison.Ordinal) > 0;
        }
    }
}
