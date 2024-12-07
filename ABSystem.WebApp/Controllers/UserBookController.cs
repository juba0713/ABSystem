using ABSystem.Data.Models;
using ABSystem.Resources.Constants;
using ABSystem.Services.Dto;
using ABSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ABSystem.WebApp.Controllers
{
    [Authorize(Roles = CommonConstant.User)]
    public class UserBookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly INotificationService _notificationService;
        private readonly ILogger<UserBookController> _logger;

        public UserBookController (ILogger<UserBookController> logger, IBookService bookService, INotificationService notificationService)
        {
            _bookService = bookService;
            _logger = logger;
            _notificationService = notificationService; 
        }

        [HttpGet]
        [Route("/books-list")]
        public IActionResult BookingListScreen()
        {
            ViewDto viewDto = new ViewDto();

            try
            {
                var books = this._bookService.GetBooksByUserId();

                viewDto.UserBooks = books;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, MessageConstant.BOOK_RETRIEVAL_ERROR);
            }

            return PartialView(CommonConstant.U_BOOKS_LIST_HTML, viewDto);
        }

        [HttpPost]
        [Route("/book")]
        public IActionResult AddBook(ViewDto dto)
        {

            var userBookDto = dto.UserBookDto;
            var context = new ValidationContext(userBookDto);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(userBookDto, context, results, true))
            {
                TempData["ErrorBooking"] = true;
                return RedirectToAction("RoomDetailsScreen", "UserRoom", new { roomId = userBookDto.RoomId });
            }

            try
            {
                this._bookService.AddBook(userBookDto);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, MessageConstant.BOOK_ADDED_ERROR);
            }


            Console.WriteLine("First Name: " +  userBookDto.FirstName);
            Console.WriteLine("Last Name: " +  userBookDto.LastName);
            Console.WriteLine("Email: " +  userBookDto.Email);
            Console.WriteLine("Phone: " +  userBookDto.PhoneNumber);
            Console.WriteLine("Address: " +  userBookDto.Address);
            Console.WriteLine("StartTime: " +  userBookDto.StartTime);
            Console.WriteLine("EndTime: " +  userBookDto.EndTime);
            Console.WriteLine("Is it currence: " +  userBookDto.IsRecurrence);
            Console.WriteLine("Recurrence Type: " +  userBookDto.RecurrenceType);
            Console.WriteLine("Recurrence Repeat: " +  userBookDto.RecurrenceRepeat);
            Console.WriteLine("Request: " +  userBookDto.Request);
            Console.WriteLine("BookDate: " +  userBookDto.BookDate);
            foreach(var date in userBookDto.BookDates)
            {
                Console.WriteLine("BookDates: " + date);
            }
            

            return RedirectToAction("RoomsListScreen", "UserRoom");
        }

        [HttpGet]
        [Route("/books-list/book-details")]
        public IActionResult BookingDetailsScreen(int bookId, int notifyId = 0, bool read = false)
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
            catch (Exception ex)
            {
                _logger.LogError(ex, MessageConstant.BOOK_RETRIEVAL_ERROR);
            }


            return PartialView(CommonConstant.U_BOOK_DETAILS_HTML, book);
        }

        [HttpPost]
        [Route("/book/cancel")]
        public IActionResult CancelBook(int bookId)
        {
            Console.WriteLine("Book Id: " + bookId);
            try
            {
                this._bookService.UpdateBookStatus(bookId, CommonConstant.CANCELED);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MessageConstant.BOOK_CANCELED_ERROR);
            }
            TempData["SuccessMessage"] = MessageConstant.BOOK_REJECTED;
            return RedirectToAction("BookingListScreen");
        }

    }
}
