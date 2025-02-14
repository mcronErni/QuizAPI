using QuizAPI.Contract.DTO;

namespace QuizAPI.Domain.DTO
{
    public class BootcamperDTO
    {
        //public int BootcamperId { get; set; }
        public required string Name { get; set; }
        public required string BootcamperEmail { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        //public ICollection<BootcamperQuizDTO>? Quizzes { get; set; }
    }

    public class ListAllBootcamperDTO
    {
        public required string Name { get; set; }
        public required string BootcamperEmail { get; set; }
    }

    public class ListOneBootcamperDTO
    {
        public required string Name { get; set; }
        public required string BootcamperEmail { get; set; }
        public ICollection<BootcamperQuizDTO>? Quizzes { get; set; }
    }
}
