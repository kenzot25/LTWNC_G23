using G23NHNT.Models;
using System.Threading.Tasks;

namespace G23NHNT.Repositories
{
    public interface IAccountRepository
    {
        Task<Account?> LoginAsync(string username, string password);
        Task<bool> RegisterAsync(Account account);
        Task<bool> IsUserNameExistAsync(string username);
    }
}
