using System.ComponentModel.DataAnnotations;

namespace QuizApp.Domain
{
    //en modellklasse for svaralternativer til quiz
    public class Option
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Text { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }
        public Question? Question { get; set; }
    }
}
