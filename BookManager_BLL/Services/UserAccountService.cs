using BookManager_DAL.Entities;
using BookManager_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager_BLL.Services
{
    public class UserAccountService
    {
        private UserAccountRepository _repo = new UserAccountRepository();

        public UserAccount? CheckLogin(string email, string password)
        {
            return _repo.GetAccount(email, password);
        }
    }
}
