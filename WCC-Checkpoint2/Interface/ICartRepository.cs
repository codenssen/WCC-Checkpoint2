using WCC_Checkpoint2.Models;

namespace WCC_Checkpoint2.Interface
{
    public interface ICartRepository
    {
        Task<int> GetCartIdAsync();
        Task<Cart> GetCartAsync(int id);

        Task<bool> RemoveFromCartAsync(int bookId);
    }
}
