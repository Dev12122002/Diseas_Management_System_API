using Diseas_Management_System.Models;

namespace Diseas_Management_System.Data
{
    public interface IAuthRepo
    {
        Task<int> Register(Admin admin, string password);

        Task<string> Login(string email, string password);

        Task<bool> AdminExists(string email);
    }
}
