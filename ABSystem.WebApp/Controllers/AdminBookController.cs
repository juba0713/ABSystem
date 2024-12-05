using ABSystem.Resources.Constants;
using ABSystem.Services.Dto;
using ABSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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

        [HttpPost]
        [Route("/book/approve")]
        public IActionResult ApproveBook(int bookId)
        {
            Console.WriteLine("Book Id: " + bookId);
            try
            {
                this._bookService.UpdateBookStatus(bookId, CommonConstant.ACCEPTED);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MessageConstant.BOOK_APPROVED_ERROR);
            }

            return RedirectToAction("BookingsListScreen");
        }

        [HttpPost]
        [Route("/book/reject")]
        public IActionResult RejectBook(int bookId)
        {
            Console.WriteLine("Book Id: " + bookId);
            try
            {
                this._bookService.UpdateBookStatus(bookId, CommonConstant.REJECTED);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MessageConstant.BOOK_REJECTED_ERROR);
            }

            return RedirectToAction("BookingsListScreen");
        }
    }
}
