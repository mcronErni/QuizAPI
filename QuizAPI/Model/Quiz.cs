using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizAPI.Model
{
    public class Quiz
    {
        [Key]
        public int QuizId { get; set; }
        [Required]
        public string QuizTitle { get; set; }
        [Required]
        public required List<Question> Questions { get; set; }
        [Required]
        public required int TotalScore { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        [ForeignKey(nameof(Mentor))]
        public int MentorId { get; set; }
        public required Mentor Mentor { get; set; }

        public ICollection<BootcamperQuiz>? BootcamperQuizzes { get; set; }
    }
}
