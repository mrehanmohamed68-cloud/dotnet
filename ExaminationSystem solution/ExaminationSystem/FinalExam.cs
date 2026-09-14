using System;

namespace ExaminationSystem
{
    // "Final Exam Only Shows The Question and Answers" (never the right answer)
    public class FinalExam<TQuestion> : Exam<TQuestion>
        where TQuestion : Question
    {
        public FinalExam(Subject subject, TimeSpan duration) : base(subject, duration)
        {
        }

        public override void ShowExam()
        {
            Mode = ExamMode.Starting; // triggers the student notification

            Console.WriteLine($"--- Final Exam: {Subject} ---");
            foreach (var pair in QuestionAnswerDictionary)
            {
                pair.Key.Represent();
                Console.WriteLine($"   Your answer: {pair.Value}");
            }

            Mode = ExamMode.Finished;
            // note: on purpose, no "correct answer" section here
        }
    }
}
