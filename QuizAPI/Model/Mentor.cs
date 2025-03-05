using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace QuizAPI.Model
{
    public class Mentor
    {
        [Key]
        public int Id { get; set; }

        public ICollection<Quiz>? Quizzes { get; set; }


        [ForeignKey(nameof(Account))]
        public int AccountId { get; set; }

        public Account Account { get; set; }
    }
}
