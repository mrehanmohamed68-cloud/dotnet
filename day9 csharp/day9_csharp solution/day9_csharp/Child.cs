using System;

namespace day9_csharp
{
    public class Child : Parent
    {
        // sealed: locks this override so no further class in the hierarchy
        // (e.g. a GrandChild) can override Salary again — protects the
        // business rule below from being changed further down the chain.
        public sealed override int Salary
        {
            get { return base.Salary; }
            set { base.Salary = value + 1000; } // e.g. fixed bonus applied on set
        }

        // Extended method that uses the sealed Salary property.
        public void DisplaySalary()
        {
            Console.WriteLine($"Child Salary (after bonus): {Salary}");
        }
    }
}
