using BookStoreBE.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreBE.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly MyDbContext _dbContext;
        public BookRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void DeleteBook(int id)
        {
            var book = _dbContext.books.Find(id);
            _dbContext.books.Remove(book);
            Save();
        }

        public Books GetBookById(int id)
        {
            return _dbContext.books.Find(id);
        }

        public IEnumerable<Books> GetBooks()
        {
            return _dbContext.books.ToList();
        }

        public void InsertBook(Books book)
        {
            _dbContext.Add(book);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateBook(Books book)
        {
            _dbContext.Entry(book).State = EntityState.Modified;
            Save();
        }
    }
}
