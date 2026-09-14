using System;

namespace ExaminationSystem
{
    // "Practice exam shows the right answer after finishing taking the Exam"
    public class PracticeExam<TQuestion> : Exam<TQuestion>
        where TQuestion : Question
    {
        // constructor chaining down to the base
        public PracticeExam(Subject subject, TimeSpan duration) : base(subject, duration)
        {
        }

        public override void ShowExam()
        {
            Mode = ExamMode.Starting; // triggers the student notification

            Console.WriteLine($"--- Practice Exam: {Subject} ---");
            foreach (var pair in QuestionAnswerDictionary)
            {
                pair.Key.Represent();
                Console.WriteLine($"   Your answer: {pair.Value}");
            }

            Mode = ExamMode.Finished;

            // reveal the correct answer for every question -> practice-only behavior
            Console.WriteLine("\n--- Correct Answers ---");
            foreach (var pair in QuestionAnswerDictionary)
            {
                foreach (var possible in pair.Key.PossibleAnswers)
                {
                    if (possible.IsCorrect)
                        Console.WriteLine($"   {pair.Key.Header} -> {possible.Text}");
                }
            }
        }
    }
}
