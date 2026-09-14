using System;

namespace ExaminationSystem
{
    internal class Program
    {
        static void Main()
        {
            #region Setup: Subject + Students (event subscribers)
            var subject = new Subject("Data Structures", "CS201");

            var student1 = new Student(1, "Ahmed");
            var student2 = new Student(2, "Sara");
            subject.Enroll(student1); // wires OnExamStarting to Subject.ExamStarting
            subject.Enroll(student2);
            #endregion

            #region Setup: Questions -> logged via QuestionList to its own file
            var questionList = new QuestionList("questions_log.txt");

            var q1 = new TrueFalseQuestion("Q1", "A Stack is FIFO.", 5, correctAnswer: false);
            var q2 = new ChooseOneQuestion("Q2", "Which structure is LIFO?", 5, new AnswerList
            {
                new Answer("Queue", false),
                new Answer("Stack", true),
                new Answer("Array", false)
            });
            var q3 = new ChooseAllQuestion("Q3", "Which of these are linear structures?", 10, new AnswerList
            {
                new Answer("Array", true),
                new Answer("Tree", false),
                new Answer("LinkedList", true)
            });

            questionList.Add(q1); // new Add(): logs to questions_log.txt, keeps default List behavior
            questionList.Add(q2);
            questionList.Add(q3);
            #endregion

            #region Build both exam objects (as required)
            var practiceExam = new PracticeExam<Question>(subject, TimeSpan.FromMinutes(30));
            foreach (var q in questionList)
                practiceExam.QuestionAnswerDictionary[q] = new Answer("(not answered yet)");

            var finalExam = new FinalExam<Question>(subject, TimeSpan.FromMinutes(60));
            foreach (var q in questionList)
                finalExam.QuestionAnswerDictionary[q] = new Answer("(not answered yet)");
            #endregion

            #region Let the end user pick which exam to take
            Console.WriteLine("Select exam type: 1) Practice  2) Final");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                practiceExam.ShowExam();
            }
            else if (choice == "2")
            {
                finalExam.ShowExam();
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
            #endregion

            #region Quick look at the logged questions file (TextReader demo)
            Console.WriteLine("\n--- questions_log.txt content ---");
            Console.WriteLine(questionList.ReadLog());
            #endregion
        }
    }
}
