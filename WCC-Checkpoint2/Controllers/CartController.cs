using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WCC_Checkpoint2.Interface;
using WCC_Checkpoint2.Models;

namespace WCC_Checkpoint2.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [Authorize]
        public async Task<IActionResult> ViewCart()
        {
            try
            {
                var cart = await GetCartAsync();
                return View(cart);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("ViewCart");
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> RemoveFromCart(int bookId)
        {
            try
            {
                await _cartRepository.RemoveFromCartAsync(bookId);
                return RedirectToAction("ViewCart", "Cart");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var cart = await GetCartAsync();
                return View("ViewCart", cart);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToCart(int bookId, int quantity)
        {
            try
            {
                await _cartRepository.AddToCart(bookId, quantity);
                return RedirectToAction("ViewCart");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var cart = await GetCartAsync();
                return View("ViewCart", cart);
            }
        }

        private async Task<Cart> GetCartAsync()
        {
            var cartId = await _cartRepository.GetCartIdAsync();
            return await _cartRepository.GetCartAsync(cartId);
        }
    }
}
