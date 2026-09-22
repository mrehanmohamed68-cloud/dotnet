using System;

namespace day9_csharp
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        // virtual: allows a derived class to provide its own implementation
        // (e.g. validation, formatting) while callers using a Person reference
        // still get the derived behavior at runtime (late binding).
        public virtual string Department { get; set; }

        public Person(string name, int age, string department)
        {
            Name = name;
            Age = age;
            Department = department;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Age: {Age}, Department: {Department}";
        }
    }
}
