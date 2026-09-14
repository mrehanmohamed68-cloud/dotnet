using System.Collections.Generic;
using System.IO;

namespace ExaminationSystem
{
    // "Design a class to represent the Question list by inheriting the
    // List<> class. Override the Add Method, keep the default behavior
    // for the Add Method and add logic to open a file and Log the
    // Questions in it, every Question Object of Question List will be
    // logged to a Same file. each Question List has Different File"
    //
    // IMPORTANT note (same idea as the "new vs override" demo you already
    // have): List<T>.Add() is NOT declared virtual in the framework, so it
    // can't truly be overridden through polymorphism. The correct way to
    // "keep the default behavior AND add logic" is to hide it with "new",
    // explicitly call base.Add() first (so nothing about the original
    // behavior is lost), then add our own logging on top.
    public class QuestionList : List<Question>
    {
        // each QuestionList instance -> its own log file
        private readonly string logFilePath;

        public QuestionList(string logFilePath)
        {
            this.logFilePath = logFilePath;
        }

        public new void Add(Question item)
        {
            base.Add(item);      // default List<Question> behavior, untouched
            LogQuestion(item);   // extra behavior on top
        }

        private void LogQuestion(Question item)
        {
            // StreamWriter derives from TextWriter.
            // append:true -> every Question in this list keeps landing in
            // the SAME file, one line appended per Add() call.
            using TextWriter writer = new StreamWriter(logFilePath, append: true);
            writer.WriteLine(item.ToString());
        }

        // Bonus (uses TextReader, as hinted in the assignment) -> lets you
        // verify what got logged without opening the file manually.
        public string ReadLog()
        {
            if (!File.Exists(logFilePath)) return string.Empty;

            using TextReader reader = new StreamReader(logFilePath);
            return reader.ReadToEnd();
        }
    }
}
