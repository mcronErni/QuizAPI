using QuizAPI.Model;

namespace QuizAPI.Contract.Interface
{
    public interface IBootcamperRepository
    {
        public Task<ICollection<Bootcamper>?> Get();
        public Task<Bootcamper?> GetById(int id);
        public Task<Bootcamper?> CreateBootcamper(Bootcamper bootcamper);
        public Task<Bootcamper?> UpdateBootcamper(int id, Bootcamper bootcamper);
        public Task<Bootcamper?> DeleteBootcamper(int id);
    }
}
