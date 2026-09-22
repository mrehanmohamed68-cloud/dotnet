using System;

namespace day9_csharp
{
    public class Parent
    {
        private int salary;

        public virtual int Salary
        {
            get { return salary; }
            set { salary = value; }
        }
    }
}
