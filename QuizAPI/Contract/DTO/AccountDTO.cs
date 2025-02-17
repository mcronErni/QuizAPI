using QuizAPI.Model;
using System.ComponentModel.DataAnnotations;

namespace QuizAPI.Contract.DTO
{
    public class AccountDTO
    {
        public string UsernameEmail { get; set; }

        public string Password{ get; set; }
    }

    public class ShowAccountDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }

    public class RegisterAccountDTO
    {

        public string Name { get; set; }
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }  // Add Email

        public string Password { get; set; }

        public string Role { get; set; }  // "bootcamper" or "mentor"
    }
}
