using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizAPI.Model
{
    public class Bootcamper
    {
        [Key]
        public int Id { get; set; }

        public ICollection<BootcamperQuiz>? BootcamperQuizzes { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
