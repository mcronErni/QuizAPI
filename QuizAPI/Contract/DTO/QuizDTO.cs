using System.ComponentModel.DataAnnotations;
using QuizAPI.Model;

namespace QuizAPI.Domain.DTO
{
    public class QuizDTO
    {
        public int QuizId { get; set; }
        public string QuizTitle { get; set; }
        public int TotalScore { get; set; }
        public string MentorName { get; set; }
        public required List<Question> Questions { get; set; }

    }

    public class ListQuizDTO
    {
        public int QuizId { get; set; }
        public string QuizTitle { get; set; }
        public int TotalScore { get; set; }
        public string MentorName { get; set; }
    }
}
