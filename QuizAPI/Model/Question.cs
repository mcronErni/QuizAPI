using System.ComponentModel.DataAnnotations;

namespace QuizAPI.Model
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string MQuestion { get; set; }
        [Required]
        public required string Answer { get; set; }
        [Required]
        public required List<string> Choices { get; set; }
    }
}
