using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Domain
{
    public class Quiz
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title must be 100 characters or fewer.")]
        public string Title { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description can be 500 characters or fewer.")]
        public string? Description { get; set; }

        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        // Relationship — one quiz can have many questions
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
