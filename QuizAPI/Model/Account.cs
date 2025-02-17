using System.ComponentModel.DataAnnotations;

namespace QuizAPI.Model
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }  // Add Email

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        public string Role { get; set; }  // "bootcamper" or "mentor"

        // Navigation properties
        public int? BootcamperId { get; set; }
        public Bootcamper Bootcamper { get; set; }

        public int? MentorId { get; set; }
        public Mentor Mentor { get; set; }
    }


}
