using System;

namespace day9_csharp
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public Department Department { get; set; }

        public Employee(int id, string name, decimal salary, Department department)
        {
            Id = id;
            Name = name;
            Salary = salary;
            Department = department;
        }

        // Business rule for equality: same Id + same Name = same employee.
        // This is what lets Helper2<Employee>.SearchArray find the right item
        // instead of relying on default reference equality.
        public override bool Equals(object obj)
        {
            if (obj is not Employee other) return false;
            return Id == other.Id && Name == other.Name;
        }

        public override int GetHashCode() => (Id, Name).GetHashCode();

        public override string ToString() =>
            $"Emp Id={Id}, Name={Name}, Salary={Salary}, Dept={Department?.Name}";
    }
}
