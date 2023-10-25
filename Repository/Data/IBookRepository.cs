using Repository.Models;

namespace Repository.Data
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book> GetBookDetail(int id);
        Task InsertBook(Book book);
        Task EditBook(Book book);
        Task DeleteBook(int id);

    }
}
