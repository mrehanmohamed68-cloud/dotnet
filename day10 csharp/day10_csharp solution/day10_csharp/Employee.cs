using System;

namespace day10_csharp
{
    public class Employee : IComparable<Employee>, ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }

        public Employee(int id, string name, double salary)
        {
            Id = id;
            Name = name;
            Salary = salary;
        }

        // Type-safe comparison based on Salary (no casting, no InvalidCastException risk).
        // Parameter is Employee? to match IComparable<T>.CompareTo(T?) exactly
        // (nullable reference types are enabled in this project).
        public int CompareTo(Employee? other)
        {
            if (other == null) return 1; // by convention, any instance sorts after null
            return Salary.CompareTo(other.Salary);
        }

        // Used by the ICloneable-constrained SortingAlgorithm<T> to clone before sorting.
        public object Clone()
        {
            return new Employee(Id, Name, Salary);
        }

        public override string ToString()
        {
            return $"Id={Id}, Name={Name}, Salary={Salary}";
        }
    }
}
