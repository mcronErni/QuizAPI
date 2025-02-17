using QuizAPI.Model;

namespace QuizAPI.Contract.Interface
{
    public interface IAuthRepository
    {
        Task<Bootcamper> Register(Bootcamper bootcamper);
        Task<Bootcamper> Login(string username, string password);
        Task<bool>  UserExists(string username);
    }
}
