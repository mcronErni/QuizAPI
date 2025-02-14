using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizAPI.Model
{
    public class Bootcamper
    {
        [Key]
        public int BootcamperId { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        [EmailAddress]
        public required string BootcamperEmail { get; set; }
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }

        public ICollection<BootcamperQuiz>? BootcamperQuizzes { get; set; }
    }
}
