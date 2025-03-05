using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuizAPI.Model
{
    public class BootcamperQuiz
    {
        [ForeignKey(nameof(Bootcamper))]
        public int BootcamperId { get; set; }
        [ForeignKey(nameof(Quiz))]
        public int QuizId { get; set; }
        public int Score { get; set; }

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Quiz Quiz { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Bootcamper Bootcampers { get; set; }
    }
}
