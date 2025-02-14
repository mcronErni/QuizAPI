using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizAPI.Model
{
    public class BootcamperQuiz
    {
        [ForeignKey(nameof(Bootcamper))]
        [Required]
        public int BootcamperId { get; set; }
        [ForeignKey(nameof(Quiz))]
        [Required]
        public int QuizId { get; set; }
        public int Score { get; set; }

        public Quiz Quizzes { get; set; }
        public Bootcamper Bootcampers { get; set; }
    }
}
