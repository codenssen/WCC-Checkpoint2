using Microsoft.AspNetCore.Mvc;
using WCC_Checkpoint2.Interface;

namespace WCC_Checkpoint2.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<IActionResult> ViewCart()
        {
            var cartId = await _cartRepository.GetCartIdAsync();
            var cart = await _cartRepository.GetCartAsync(cartId);
            return View(cart);
        }

        [HttpPost]
        public async Task<bool> RemoveFromCart(int bookId)
        {
            return await _cartRepository.RemoveFromCartAsync(bookId);

        }

    }
}
