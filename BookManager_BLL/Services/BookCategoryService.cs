using BookManager_DAL.Entities;
using BookManager_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager_BLL.Services
{
    public class BookCategoryService
    {
        private BookCategoryRepository _repo;

        public List<BookCategory> GetAllCategory()
        {
            return _repo.GetAll();
        }
    }
}
