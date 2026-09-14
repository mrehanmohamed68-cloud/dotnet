namespace ExaminationSystem
{
    // A single possible answer for a Question.
    // For TrueFalse -> 2 Answer objects ("True"/"False")
    // For ChooseOne / ChooseAll -> N Answer objects (the choices)
    public class Answer
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        // constructor chaining: 1-param ctor assumes "not correct" by default
        public Answer(string text) : this(text, false)
        {
        }

        public Answer(string text, bool isCorrect)
        {
            Text = text;
            IsCorrect = isCorrect;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
