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

        [HttpGet]
        public async Task<IActionResult> RemoveFromCart(int bookId)
        {
            var RemovedItem = await _cartRepository.RemoveFromCartAsync(bookId);
            return RedirectToAction("ViewCart", "Cart");
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int bookId, int quantity)
        {
            try
            {
                var book = await _cartRepository.AddToCart(bookId, quantity);
                return RedirectToAction("ViewCart");
            }
            catch (Exception ex)
            {
                // Gérer l'exception, par exemple en ajoutant un message d'erreur à la vue
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("ViewCart");
            }
        }

    }
}
