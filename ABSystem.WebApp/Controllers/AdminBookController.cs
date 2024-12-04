using ABSystem.Resources.Constants;
using ABSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABSystem.WebApp.Controllers
{
    [Authorize(Roles = CommonConstant.Admin + "," + CommonConstant.Super)]
    public class AdminBookController : Controller
    {

        private readonly IBookService _bookService;
        private readonly ILogger<AdminBookController> _logger;

        public AdminBookController(IBookService bookService, ILogger<AdminBookController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet]
        [Route("/admin/books-list")]
        public IActionResult BookingsListScreen()
        {
            var books = this._bookService.GetBooks();

            return PartialView(CommonConstant.A_BOOKS_LIST_HTML, books);
        }
    }
}
