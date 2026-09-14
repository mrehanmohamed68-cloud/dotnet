using System.Collections.Generic;

namespace ExaminationSystem
{
    // Plain specialized list -> no extra logging requirement was asked for
    // Answers (only QuestionList needs the file-logging behavior), so this
    // stays a simple List<Answer> wrapper that reads nicely in code
    // (new AnswerList() instead of new List<Answer>()).
    public class AnswerList : List<Answer>
    {
    }
}
