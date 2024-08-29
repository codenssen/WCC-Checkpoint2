using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using WCC_Checkpoint2.Interface;
using WCC_Checkpoint2.Models;

namespace WCC_Checkpoint2.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly IStripeClient _stripeClient;
        private readonly IConfiguration _configuration;
        public CartController(ICartRepository cartRepository, IStripeClient stripeClient, IConfiguration configuration)
        {
            _cartRepository = cartRepository;
            _stripeClient = stripeClient;
            _configuration = configuration;
        }

        [Authorize]
        public async Task<IActionResult> ViewCart()
        {
            try
            {
                var cart = await GetCartAsync();
                ViewData["StripePublishableKey"] = _configuration["Stripe:PublishableKey"]; // Assurez-vous que _configuration est injecté
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProcessPayment(string stripeToken)
        {
            try
            {
                var cart = await GetCartAsync();
                if (cart == null || !cart.CartItems.Any())
                {
                    ModelState.AddModelError("", "Le panier est vide.");
                    return View("ViewCart", cart);
                }

                var totalAmount = cart.CartItems.Sum(item => item.Price * item.Quantity) * 100; // Conversion en cents

                var options = new ChargeCreateOptions
                {
                    Amount = (long)totalAmount,
                    Currency = "eur",
                    Description = "Achat dans la boutique",
                    Source = stripeToken,
                };

                var service = new ChargeService(_stripeClient);
                Charge charge = service.Create(options);

                if (charge.Status == "succeeded")
                {
                    ViewBag.Message = "Paiement réussi. Merci pour votre achat!";
                    // Ici, tu peux vider le panier ou enregistrer l'achat dans ta base de données
                    //await _cartRepository.ClearCartAsync();
                }
                else
                {
                    ModelState.AddModelError("", "Le paiement a échoué.");
                }

                return View("ViewCart", cart);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var cart = await GetCartAsync();
                return View("ViewCart", cart);
            }
        }
    }
}
