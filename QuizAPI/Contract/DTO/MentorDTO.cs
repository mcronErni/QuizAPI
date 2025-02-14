using QuizAPI.Model;
using System.ComponentModel.DataAnnotations;

namespace QuizAPI.Contract.DTO
{
    public class MentorDTO
    {
        public required string MentorName { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        [EmailAddress]
        public required string MentorEmail { get; set; }
    }

    public class ListMentorDTO
    {
        public required string MentorName { get; set; }
        [EmailAddress]
        public string Username { get; set; }
        public required string MentorEmail { get; set; }
        public ICollection<Quiz>? Quizzes { get; set; }
    }
}
