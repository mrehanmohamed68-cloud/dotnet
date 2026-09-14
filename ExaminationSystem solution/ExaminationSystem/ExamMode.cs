namespace ExaminationSystem
{
    // Queued -> created but not opened yet
    // Starting -> just opened; this is the moment students get notified
    // Finished -> student submitted / time ended
    public enum ExamMode
    {
        Queued,
        Starting,
        Finished
    }
}
