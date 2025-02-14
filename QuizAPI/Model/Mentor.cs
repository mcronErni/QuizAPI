using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace QuizAPI.Model
{
    public class Mentor
    {
        [Key]
        public required int MentorId { get; set; }
        [Required]
        public required string MentorName { get; set; }
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        [EmailAddress]
        public required string MentorEmail { get; set; }

        public ICollection<Quiz>? Quizzes { get; set; }
    }
}
