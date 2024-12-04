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
        private readonly ILogger<UserBookController> _logger;

        public UserBookController (ILogger<UserBookController> logger, IBookService bookService) {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet]
        [Route("/book")]
        public IActionResult BookingListScreen()
        {
            return PartialView("~/Views/User/RoomList.cshtml");
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
            Console.WriteLine("Request: " +  userBookDto.Request);
            Console.WriteLine("BookDate: " +  userBookDto.BookDate);

            return RedirectToAction("RoomsListScreen", "UserRoom");
        }
    }
}
