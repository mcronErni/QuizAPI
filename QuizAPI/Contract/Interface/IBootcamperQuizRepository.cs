using QuizAPI.Model;

namespace QuizAPI.Contract.Interface
{
    public interface IBootcamperQuizRepository
    {
        public Task<ICollection<BootcamperQuiz>> GetAllBootcamperQuizByQuizId(int quizId);
        public Task<BootcamperQuiz> GetBootcamperQuizForBootcamper(int bootcamperId, int quizId);
        public Task<ICollection<BootcamperQuiz>> GetBootcamperQuizForBootcamperAllQuiz(int bootcamperId);
        public Task<BootcamperQuiz> CreateBootcamperQuiz(BootcamperQuiz bq);
    }
}
