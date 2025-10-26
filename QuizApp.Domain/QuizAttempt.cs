namespace QuizApp.Domain;

public class QuizAttempt
//en modellklasse for forsøk til quiz
{
    public int Id { get; set; }
    public int QuizId { get; set; }
    public DateTime TakenUtc { get; set; } = DateTime.UtcNow;
    public int TotalScore { get; set; }
    public List<Answer> Answers { get; set; } = new();
}
