using System;

namespace day6_csharp.Interfaces
{
    // named "MyFile" instead of "File" to avoid clashing with System.IO.File
    // Part9: a single class can implement MULTIPLE interfaces ->
    // this is how C# overcomes the "single inheritance" limit of classes
    internal class MyFile : IReadable, IWritable
    {
        public string Name { get; set; }

        public MyFile(string name)
        {
            Name = name;
        }

        public void Read()
        {
            Console.WriteLine($"Reading from {Name}...");
        }

        public void Write()
        {
            Console.WriteLine($"Writing to {Name}...");
        }
    }
}
