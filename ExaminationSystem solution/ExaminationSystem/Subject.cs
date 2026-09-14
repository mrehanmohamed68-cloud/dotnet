using System;
using System.Collections.Generic;

namespace ExaminationSystem
{
    // delegate signature for the notification
    public delegate void ExamStartingEventHandler(object sender, ExamStartingEventArgs e);

    public class ExamStartingEventArgs : EventArgs
    {
        public string SubjectName { get; }
        public string Message { get; }

        public ExamStartingEventArgs(string subjectName, string message)
        {
            SubjectName = subjectName;
            Message = message;
        }
    }

    // "Every Exam object is Associated to a Subject Object"
    public class Subject
    {
        public string Name { get; set; }
        public string Code { get; set; }

        private readonly List<Student> enrolledStudents = new();

        // constructor chaining
        public Subject(string name) : this(name, "N/A")
        {
        }

        public Subject(string name, string code)
        {
            Name = name;
            Code = code;
        }

        // event that Student objects subscribe to
        public event ExamStartingEventHandler ExamStarting;

        public void Enroll(Student student)
        {
            enrolledStudents.Add(student);
            ExamStarting += student.OnExamStarting; // wire the student up
        }

        // called by Exam when its Mode flips to Starting
        public void NotifyExamStarting()
        {
            var args = new ExamStartingEventArgs(Name, $"The exam for \"{Name}\" has started. Good luck!");
            ExamStarting?.Invoke(this, args);
        }

        public override string ToString()
        {
            return $"{Name} ({Code})";
        }
    }
}
