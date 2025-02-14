using QuizAPI.Model;

namespace QuizAPI.Contract.Interface
{
    public interface IBootcamperQuizRepository
    {
        public Task<ICollection<BootcamperQuiz>> GetAllBootcamperQuiz();
        public Task<BootcamperQuiz> GetBootcamperQuiz(int bootcamperId, int quizId);
        public Task<BootcamperQuiz> CreateBootcamperQuiz(BootcamperQuiz bq);
    }
}
