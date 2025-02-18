using QuizAPI.Model;

namespace QuizAPI.Contract.Interface
{
    public interface IQuizRepository
    {
        public Task<ICollection<Quiz>> Get();
        public Task<Quiz?> GetById(int id);

        public Task<ICollection<Quiz>?> GetByMentorId(int mentorId);
        public Task<Quiz?> CreateQuiz(Quiz quiz);
        public Task<Quiz?> UpdateQuiz(int id, Quiz quiz);
        public Task<Quiz?> DeleteQuiz(int id);
    }
}
