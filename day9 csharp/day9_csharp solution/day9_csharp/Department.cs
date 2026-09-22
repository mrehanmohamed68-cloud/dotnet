using System;

namespace day9_csharp
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        // Overriding Equals so two Department objects with the same data
        // are considered equal, instead of comparing references.
        public override bool Equals(object obj)
        {
            if (obj is not Department other) return false;
            return Id == other.Id && Name == other.Name;
        }

        public override int GetHashCode() => (Id, Name).GetHashCode();

        public override string ToString() => $"Dept: {Name} (Id={Id})";
    }
}
