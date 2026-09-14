using System;

namespace ExaminationSystem
{
    internal class TrueFalseQuestion : Question
    {
        public TrueFalseQuestion(string header, string body, int marks, bool correctAnswer)
            : base(header, body, marks, BuildAnswers(correctAnswer))
        {
        }

        private static AnswerList BuildAnswers(bool correctAnswer)
        {
            var answers = new AnswerList();
            answers.Add(new Answer("True", correctAnswer));
            answers.Add(new Answer("False", !correctAnswer));
            return answers;
        }

        public override void Represent()
        {
            Console.WriteLine(this);
            Console.WriteLine("   ( ) True     ( ) False");
        }
    }
}
