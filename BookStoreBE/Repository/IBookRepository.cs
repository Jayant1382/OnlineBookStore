using BookStoreBE.Models;

namespace BookStoreBE.Repository
{
    public interface IBookRepository
    {
        IEnumerable<Books> GetBooks();
        Books GetBookById(int id);
        void InsertBook(Books book);
        void DeleteBook(int id);
        void UpdateBook(Books book);
        void Save();
    }
}
