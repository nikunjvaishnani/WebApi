using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class BookRepository : BaseRepository
    {
        public ListResponse<Book> GetBooks(int pageIndex, int pageSize, string keyword)
        {
            keyword = keyword?.ToLower().Trim();
            var query = _context.Books.Where(u => keyword == null || u.Name.ToLower().Contains(keyword)).AsQueryable();

            int totalRecords = query.Count();
            IEnumerable<Book> books = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return new ListResponse<Book>()
            {
                Results = books,
                TotalRecords = totalRecords
            };
        }

        public Book AddBook(Book book)
        {
            var entry = _context.Books.Add(book);
            _context.SaveChanges();
            return entry.Entity;
        }

        public Book GetBook(int id)
        {
            return _context.Books.Where(b => b.BookId == id).FirstOrDefault();
        }

        public Book UpdateBook(Book book)
        {
            var entry = _context.Books.Update(book);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool DeleteBook(int id)
        {
            var book = _context.Books.FirstOrDefault(c => c.BookId == id);
            if (book == null)
                return false;

            _context.Books.Remove(book);
            _context.SaveChanges();
            return true;
        }
    }
}
