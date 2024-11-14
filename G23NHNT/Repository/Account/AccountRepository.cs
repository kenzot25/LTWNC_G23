using G23NHNT.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace G23NHNT.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly G23_NHNTContext _context;

        public AccountRepository(G23_NHNTContext context)
        {
            _context = context;
        }

        public async Task<Account?> LoginAsync(string username, string password)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.UserName == username && a.Password == password);
        }

        public async Task<bool> RegisterAsync(Account account)
        {
            if (await IsUserNameExistAsync(account.UserName))
            {
                return false;
            }

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsUserNameExistAsync(string username)
        {
            return await _context.Accounts.AnyAsync(a => a.UserName == username);
        }
    }
}
