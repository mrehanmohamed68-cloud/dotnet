using System;

namespace day10_csharp
{
    // Problem 4: Manager inherits Employee (reuses Id/Name/Salary + IComparable<Employee>)
    // and ALSO implements IComparable<Manager> — a class can implement multiple
    // IComparable<T> interfaces for different comparison "views" of itself.
    public class Manager : Employee, IComparable<Manager>
    {
        public string Department { get; set; }

        public Manager(int id, string name, double salary, string department)
            : base(id, name, salary)
        {
            Department = department;
        }

        // Parameter is Manager? to match IComparable<T>.CompareTo(T?) exactly
        // (nullable reference types are enabled in this project).
        public int CompareTo(Manager? other)
        {
            if (other == null) return 1; // by convention, any instance sorts after null
            return Salary.CompareTo(other.Salary);
        }

        public override string ToString()
        {
            return base.ToString() + $", Department={Department} (Manager)";
        }
    }
}
