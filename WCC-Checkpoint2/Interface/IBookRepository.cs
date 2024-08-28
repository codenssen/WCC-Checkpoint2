using WCC_Checkpoint2.Models;

namespace WCC_Checkpoint2.Interface
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);

        IEnumerable<Book> SearchBooks(string searchQuery);
    }
}
