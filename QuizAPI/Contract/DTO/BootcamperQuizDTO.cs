namespace QuizAPI.Contract.DTO
{
    public class BootcamperQuizDTO
    {
        public int QuizId { get; set; }
        public int BootcamperId { get; set; }
        public int Score { get; set; }
        public string QuizTitle { get; set; }
        public string BootcamperName { get; set; }
        public int TotalScore {  get; set; }
    }
}
