using System;

namespace day01_csharp
{
    // Simple data class for Problem 10 (List<Employee>).
    internal class Employee
    {
        public string Name { get; set; }
        public double Salary { get; set; }

        public Employee(string name, double salary)
        {
            Name = name;
            Salary = salary;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Salary: {Salary}";
        }
    }
}
