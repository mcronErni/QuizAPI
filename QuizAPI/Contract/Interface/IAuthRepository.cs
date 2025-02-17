using QuizAPI.Model;

namespace QuizAPI.Contract.Interface
{
    public interface IAuthRepository
    {
        Task<Account> Register(Account account, string password);
        Task<Account> Login(string usernameOrEmail, string password);
        Task<bool> UserExists(string username, string email);
        Task Logout();
    }

}
