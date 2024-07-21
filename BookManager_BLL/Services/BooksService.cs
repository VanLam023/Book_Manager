using BookManager_DAL.Entities;
using BookManager_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager_BLL.Services
{
    public class BooksService
    {
        private BooksRepository _repo = new();

        public List<Book> GetAllBooks()
        {
            return _repo.GetAll();
        }

        public void AddBook(Book book)
        {
            _repo.Create(book);
        }

        public void UpdateBook(Book book)
        {
            _repo.Update(book);
        }

        public void DeleteBook(Book book)
        {
            _repo.Delete(book);
        }

        public List<Book> SearchBooksByNameAndDecs(string bookName, string bookDecs)
        {
            // == so sánh chuỗi mặc định có quan tâm đến chữ hoa, chữ thường a # A 
            // ko quan tâm hoa thường ta convert về thường hết rồi mới so sánh 
            // search gần đúng, search có chứa kí tự hay ko 

            return _repo.GetAll().Where(x => x.BookName.ToLower().Contains(bookName.ToLower()) && x.Description.ToLower().Contains(bookDecs.ToLower())).ToList();
        }
    }
}
