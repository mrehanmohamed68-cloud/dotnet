using System;

namespace ExaminationSystem
{
    // Single-correct-answer MCQ (radio-button style)
    internal class ChooseOneQuestion : Question
    {
        public ChooseOneQuestion(string header, string body, int marks, AnswerList options)
            : base(header, body, marks, options)
        {
        }

        public override void Represent()
        {
            Console.WriteLine(this);
            int i = 1;
            foreach (var option in PossibleAnswers)
            {
                Console.WriteLine($"   {i}) ( ) {option.Text}");
                i++;
            }
            Console.WriteLine("   (choose only ONE)");
        }
    }
}
