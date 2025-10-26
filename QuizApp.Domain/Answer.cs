namespace QuizApp.Domain;

public class Answer
//en modellklasse for svar til quiz
{
    public int Id { get; set; }
    public int QuizAttemptId { get; set; }
    public int QuestionId { get; set; }
    public string SelectedOptionIdsCsv { get; set; } = "";
    public int EarnedPoints { get; set; }
}