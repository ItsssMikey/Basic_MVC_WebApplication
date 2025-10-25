using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Domain
{
    public class Question
    {
        public int Id { get; set; }

        [Required, StringLength(300)]
        public string Text { get; set; } = string.Empty;

        [Range(1, 100)]
        public int Points { get; set; } = 1;

        public int QuizId { get; set; }
        public Quiz? Quiz { get; set; }

        // IMPORTANT: this property must exist to fix your error
        public ICollection<Option> Options { get; set; } = new List<Option>();
    }
}
