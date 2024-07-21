using BookManager_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager_DAL.Repositories
{
    public class BookCategoryRepository
    {
        private BookManagementDbContext _context;

        public List<BookCategory> GetAll()
        {
            //_context = new BookManagementDbContext();
            _context = new();
            return _context.BookCategories.ToList();
        }
    }
}
