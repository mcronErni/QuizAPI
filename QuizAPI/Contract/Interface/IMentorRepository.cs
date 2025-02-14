using QuizAPI.Model;

namespace QuizAPI.Contract.Interface
{
    public interface IMentorRepository
    {
        public Task<ICollection<Mentor>?> Get();
        public Task<Mentor?> GetById(int id);
        public Task<Mentor?> CreateMentor(Mentor mentor);
        public Task<Mentor?> UpdateMentor(int id, Mentor mentor);
        public Task<Mentor?> DeleteMentor(int id);
    }
}
