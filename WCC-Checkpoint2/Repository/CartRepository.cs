using Microsoft.EntityFrameworkCore;
using WCC_Checkpoint2.Data;
using WCC_Checkpoint2.Interface;
using WCC_Checkpoint2.Models;

namespace WCC_Checkpoint2.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetCartIdAsync()
        {
            var cart = await _context.Carts.FirstAsync();
            return cart.Id;
        }

        public async Task<Cart> GetCartAsync(int id)
        {
            return await _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Book)  // Inclure le livre lié à chaque CartItem
            .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> RemoveFromCartAsync(int bookId)
        {
            // Récupérer l'élément du panier qui correspond à l'ID du livre
            var cartItem = await _context.CartItems
                                         .Where(c => c.BookId == bookId)
                                         .FirstOrDefaultAsync();

            // Si l'élément n'existe pas, retourner faux
            if (cartItem == null)
            {
                return false;
            }

            // Supprimer l'élément du panier
            _context.CartItems.Remove(cartItem);

            // Sauvegarder les changements dans la base de données
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Book> AddToCart(int bookId, int quantity)
        {
            // Récupérer l'élément du panier correspondant
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.BookId == bookId); // currentCartId doit être déterminé en fonction de votre contexte

            // Si l'élément existe déjà, on augmente la quantité
            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                // Si l'élément n'existe pas, on récupère le livre et on crée un nouvel élément de panier
                var book = await _context.Books.FindAsync(bookId);
                if (book == null)
                {
                    throw new Exception("Livre non trouvé");
                }

                cartItem = new CartItem
                {
                    CartId = 1,
                    BookId = bookId,
                    Quantity = quantity,
                    Price = book.Price, // Supposons que vous ayez un champ Price dans votre modèle Book
                    Book = book
                };

                _context.CartItems.Add(cartItem);
            }

            // Enregistrer les modifications
            await _context.SaveChangesAsync();

            // Retourner le livre ajouté ou mis à jour
            return cartItem.Book;
        }



    }
}
