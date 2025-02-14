using System.ComponentModel.DataAnnotations;

namespace QuizAPI.Model
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }
        [Required]
        public required string MQuestion { get; set; }
        [Required]
        public required string Answer { get; set; }
        [Required]
        public required List<string> Choices { get; set; }
    }
}
