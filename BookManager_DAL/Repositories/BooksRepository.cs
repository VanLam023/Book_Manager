using BookManager_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager_DAL.Repositories
{
    public class BooksRepository
    {
        private BookManagementDbContext _context;

        public List<Book> GetAll()
        {
            _context = new BookManagementDbContext();
            return _context.Books.Include("BookCategory").ToList();
        }

        public void Create(Book x)
        {//insert into Book values(...)
            _context = new(); //nhớ new
            _context.Books.Add(x); //_arr.Add(x)
            _context.SaveChanges();
        }

        public void Update(Book x)
        {//UPDATE Book SET CỘT = VALUE, CỘT = VALUE WHERE...
            _context = new(); //nhớ new
            _context.Books.Update(x); //CUỐN SÁCH X phải tồn tại PK TRONG TABLE RỒI, CUỐN SÁCH/ROW TRONG TABLE CÓ SẴN RỒI 
            _context.SaveChanges();
        }

        public void Delete(Book book)
        {
            _context = new();
            _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }
}
