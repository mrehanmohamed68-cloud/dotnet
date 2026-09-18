using System;

namespace Part04_CopyConstructor
{
    // A reference-type field inside Student, used to show the
    // shallow-vs-deep copy difference.
    public class Address
    {
        public string City { get; set; }

        public Address(string city)
        {
            City = city;
        }

        // Deep-copy helper for Address itself.
        public Address(Address other)
        {
            City = other.City;
        }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Grade { get; set; }
        public Address HomeAddress { get; set; }

        public Student(int id, string name, double grade, Address address)
        {
            Id = id;
            Name = name;
            Grade = grade;
            HomeAddress = address;
        }

        // SHALLOW copy constructor: value types (Id, Grade) are copied by
        // value automatically, but the reference type (HomeAddress) is
        // copied by reference - both students end up pointing at the SAME
        // Address object.
        public Student(Student other)
        {
            Id = other.Id;
            Name = other.Name;
            Grade = other.Grade;
            HomeAddress = other.HomeAddress; // same reference, not a new object!
        }

        // DEEP copy: explicitly creates a brand-new Address, so the two
        // Student objects are fully independent.
        public static Student DeepCopy(Student other)
        {
            return new Student(
                other.Id,
                other.Name,
                other.Grade,
                new Address(other.HomeAddress) // new object with the same data
            );
        }

        public override string ToString()
        {
            return $"[{Id}] {Name}, Grade={Grade}, City={HomeAddress.City}";
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Student original = new Student(1, "Aya", 88.5, new Address("Cairo"));

            // --- Shallow copy ---
            Student shallow = new Student(original);
            shallow.Name = "Aya (shallow copy)";
            shallow.HomeAddress.City = "Alexandria"; // mutates the SHARED Address

            Console.WriteLine("-- Shallow copy demo --");
            Console.WriteLine("Original: " + original); // City changed too!
            Console.WriteLine("Shallow : " + shallow);

            Console.WriteLine();

            // --- Deep copy ---
            Student original2 = new Student(2, "Omar", 91.0, new Address("Giza"));
            Student deep = Student.DeepCopy(original2);
            deep.Name = "Omar (deep copy)";
            deep.HomeAddress.City = "Luxor"; // mutates only the deep copy's own Address

            Console.WriteLine("-- Deep copy demo --");
            Console.WriteLine("Original: " + original2); // unaffected
            Console.WriteLine("Deep    : " + deep);
        }
    }
}

/*
QUESTION: What is the primary purpose of a copy constructor in C#?

The primary purpose of a copy constructor is to create a new object whose
initial state is a copy of an existing object's state, so the two objects
can afterwards be modified independently (at least for value-type/immutable
data). It gives a controlled, explicit way to duplicate an object instead
of relying on the default reference-copy behavior of `=` on reference
types (which just makes a second variable point at the SAME object).

As shown above, a naive copy constructor produces a SHALLOW copy: value
type fields are duplicated, but reference-type fields still point at the
original nested objects, so changes through one instance leak into the
other. When true independence is required, the copy constructor (or a
dedicated factory method) must also create new copies of any nested
reference-type members - that's a DEEP copy.
*/
