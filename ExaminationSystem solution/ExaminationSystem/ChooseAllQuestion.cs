using System;

namespace ExaminationSystem
{
    // Multiple-correct-answers MCQ (checkbox style)
    internal class ChooseAllQuestion : Question
    {
        public ChooseAllQuestion(string header, string body, int marks, AnswerList options)
            : base(header, body, marks, options)
        {
        }

        public override void Represent()
        {
            Console.WriteLine(this);
            int i = 1;
            foreach (var option in PossibleAnswers)
            {
                Console.WriteLine($"   {i}) [ ] {option.Text}");
                i++;
            }
            Console.WriteLine("   (choose ALL that apply)");
        }
    }
}
