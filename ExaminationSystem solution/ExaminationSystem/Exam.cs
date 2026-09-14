using System;
using System.Collections.Generic;

namespace ExaminationSystem
{
    // Generic constraint: "where TQuestion : Question"
    // Why this constraint is needed:
    // 1) QuestionAnswerDictionary must only ever hold real Question objects
    //    (and their subtypes), so the generic parameter is restricted to
    //    Question or anything derived from it -> compile-time type safety,
    //    no casting/boxing of unrelated types is possible.
    // 2) Because Question overrides Equals/GetHashCode, any TQuestion used
    //    as a dictionary key behaves correctly (same Header+Body -> same
    //    key), which plain object.Equals would not guarantee.
    public abstract class Exam<TQuestion> : ICloneable, IComparable<Exam<TQuestion>>
        where TQuestion : Question
    {
        public TimeSpan Duration { get; set; }
        public int NumberOfQuestions => QuestionAnswerDictionary.Count;

        // "Question Answer Dictionary (used for Exam Correction)"
        public Dictionary<TQuestion, Answer> QuestionAnswerDictionary { get; set; }

        public Subject Subject { get; set; }

        private ExamMode mode;
        public ExamMode Mode
        {
            get => mode;
            set
            {
                mode = value;
                // "When the exam in Starting Mode, every Student taking
                // this subject should be notified"
                if (mode == ExamMode.Starting)
                {
                    Subject?.NotifyExamStarting();
                }
            }
        }

        // constructor chaining
        protected Exam(Subject subject, TimeSpan duration)
            : this(subject, duration, new Dictionary<TQuestion, Answer>())
        {
        }

        protected Exam(Subject subject, TimeSpan duration, Dictionary<TQuestion, Answer> questionAnswerDictionary)
        {
            Subject = subject;
            Duration = duration;
            QuestionAnswerDictionary = questionAnswerDictionary;
            mode = ExamMode.Queued; // every exam starts life "Queued"
        }

        // implementation differs per exam type (Practice reveals the
        // correct answer at the end, Final does not)
        public abstract void ShowExam();

        // ICloneable -> shallow copy is enough here: Subject/Questions are
        // shared references on purpose (same exam definition, new attempt);
        // if you need independent copies of the dictionary itself, clone
        // QuestionAnswerDictionary explicitly in the override.
        public virtual object Clone()
        {
            var clone = (Exam<TQuestion>)MemberwiseClone();
            clone.QuestionAnswerDictionary = new Dictionary<TQuestion, Answer>(QuestionAnswerDictionary);
            return clone;
        }

        // order exams by how long they take, then by question count
        public int CompareTo(Exam<TQuestion> other)
        {
            if (other is null) return 1;
            int byDuration = Duration.CompareTo(other.Duration);
            return byDuration != 0 ? byDuration : NumberOfQuestions.CompareTo(other.NumberOfQuestions);
        }

        public override bool Equals(object obj)
        {
            if (obj is not Exam<TQuestion> other) return false;
            return Subject?.Name == other.Subject?.Name
                && Duration == other.Duration
                && NumberOfQuestions == other.NumberOfQuestions;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Subject?.Name, Duration, NumberOfQuestions);
        }

        public override string ToString()
        {
            return $"{GetType().Name} | Subject: {Subject} | Duration: {Duration} | Questions: {NumberOfQuestions} | Mode: {Mode}";
        }
    }
}
