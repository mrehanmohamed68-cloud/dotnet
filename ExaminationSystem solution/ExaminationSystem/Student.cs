using System;

namespace ExaminationSystem
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // constructor chaining
        public Student(string name) : this(0, name)
        {
        }

        public Student(int id, string name)
        {
            Id = id;
            Name = name;
        }

        // matches the ExamStartingEventHandler delegate signature
        public void OnExamStarting(object sender, ExamStartingEventArgs e)
        {
            Console.WriteLine($"   [Notification -> {Name}] {e.Message}");
        }

        public override string ToString()
        {
            return $"{Name} (#{Id})";
        }
    }
}
