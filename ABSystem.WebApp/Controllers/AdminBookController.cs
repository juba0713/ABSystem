using ABSystem.Data.Models;
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
        private readonly INotificationService _notificationService;
        private readonly ILogger<AdminBookController> _logger;

        public AdminBookController(IBookService bookService, 
            ILogger<AdminBookController> logger,
            INotificationService notificationService)
        {
            _bookService = bookService;
            _logger = logger;
            _notificationService = notificationService;
        }

        [HttpGet]
        [Route("/admin/books-list")]
        public IActionResult BookingsListScreen()
        {
            var books = this._bookService.GetBooks();

            return PartialView(CommonConstant.A_BOOKS_LIST_HTML, books);
        }

        [HttpPost]
        [Route("/admin/book/approve")]
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

            TempData["SuccessMessage"] = MessageConstant.BOOK_APPROVED;
            return RedirectToAction("BookingsListScreen");
        }

        [HttpPost]
        [Route("/admin/book/reject")]
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
            TempData["SuccessMessage"] = MessageConstant.BOOK_REJECTED;
            return RedirectToAction("BookingsListScreen");
        }

        [HttpGet]
        [Route("/admin/books-list/book-details")]
        public IActionResult BookingDetailsScreen(int bookId, int notifyId = 0,  bool read = false)
        {

            UserBookDto book = null!;

            try
            {
                book = this._bookService.GetBookById(bookId);

                if (read)
                {
                    this._notificationService.UpdateNotificationRead(notifyId);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, MessageConstant.BOOK_RETRIEVAL_ERROR);
            }
            

            return PartialView(CommonConstant.A_BOOK_DETAILS_HTML, book);
        }
    }
}
