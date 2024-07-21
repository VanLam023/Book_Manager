using BookManager_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager_DAL.Repositories
{
    public class UserAccountRepository
    {
        private BookManagementDbContext _context;

        public UserAccount? GetAccount(string email, string password)
        {
            _context = new BookManagementDbContext();
            return _context.UserAccounts.FirstOrDefault(x => x.Email.ToLower() == email.ToLower() && x.Password == password);
        }
    }
}
