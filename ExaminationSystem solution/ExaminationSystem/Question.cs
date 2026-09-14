using System;

namespace ExaminationSystem
{
    // Base class for every question type.
    // abstract -> a "generic Question" on its own doesn't make sense,
    // it must always be a TrueFalse / ChooseOne / ChooseAll concrete question.
    public abstract class Question
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public int Marks { get; set; }

        // Part: "Question Object is associated with an Object of AnswerList"
        public AnswerList PossibleAnswers { get; set; }

        // constructor chaining: no-answers ctor -> full ctor
        protected Question(string header, string body, int marks)
            : this(header, body, marks, new AnswerList())
        {
        }

        protected Question(string header, string body, int marks, AnswerList possibleAnswers)
        {
            Header = header;
            Body = body;
            Marks = marks;
            PossibleAnswers = possibleAnswers;
        }

        // each concrete type prints itself in its own shape
        // (true/false box, numbered choices, checkboxes, ...)
        public abstract void Represent();

        public override string ToString()
        {
            return $"[{Header}] ({Marks} marks): {Body}";
        }

        // Important: Question is later used as a Dictionary key
        // (Exam.QuestionAnswerDictionary), so Equals/GetHashCode MUST be
        // overridden and kept consistent, otherwise Dictionary lookups by
        // "equal" questions would silently fail (this is the generic-class
        // constraint concern mentioned in the assignment).
        public override bool Equals(object obj)
        {
            if (obj is not Question other) return false;
            return Header == other.Header && Body == other.Body;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Header, Body);
        }
    }
}
