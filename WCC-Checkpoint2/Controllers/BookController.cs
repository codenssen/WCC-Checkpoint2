using Microsoft.AspNetCore.Mvc;
using WCC_Checkpoint2.Interface;
using WCC_Checkpoint2.Models;

namespace WCC_Checkpoint2.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var books = await _bookRepository.GetAllAsync();
                return View(books);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index");
            }
        }

        // Action pour gérer la recherche de livres
        [HttpGet]
        public ActionResult SearchBooks(string searchQuery)
        {
            var books = _bookRepository.SearchBooks(searchQuery);
            return PartialView("_BookListPartial", books);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var book = await _bookRepository.GetByIdAsync(id);
                return View(book);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var emptyBook = new Book() { };
                return View(emptyBook);
            }
        }
    }
}
