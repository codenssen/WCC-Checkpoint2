using Microsoft.EntityFrameworkCore;
using WCC_Checkpoint2.Data;
using WCC_Checkpoint2.Interface;
using WCC_Checkpoint2.Models;

namespace WCC_Checkpoint2.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Books
                          .Include(b => b.Author)  // Inclut les données de l'auteur
                          .ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _context.Books.Include(b => b.Author).Include(b => b.Category).FirstAsync(b => b.Id == id);
        }

        public IEnumerable<Book> SearchBooks(string searchQuery)
        {
            var books = _context.Books.Include(b => b.Author).Include(b => b.Category).AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                books = books.Include(b => b.Author).Include(b => b.Category).Where(b => b.Title.Contains(searchQuery) ||
                                         b.Author.Name.Contains(searchQuery) ||
                                         b.Category.Name.Contains(searchQuery));
            }

            return books.ToList();
        }
    }
}
