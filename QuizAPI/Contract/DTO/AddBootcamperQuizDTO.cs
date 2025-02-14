using QuizAPI.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizAPI.Contract.DTO
{
    public class AddBootcamperQuizDTO
    {
        public required int BootcamperId { get; set; }
        public required int QuizId { get; set; }
        public required int Score { get; set; }
    }
}
