using ABSystem.Resources.Constants;
using ABSystem.Services.Dto;
using ABSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ABSystem.WebApp.Controllers
{
    public class CalendarController : Controller
    {

		private readonly IBookService _bookService;
		private readonly ILogger<CalendarController> _logger;

		public CalendarController(IBookService bookService, ILogger<CalendarController> logger)
		{
			_bookService = bookService;	
			_logger = logger;
		}

        [HttpGet]
        [Route("/calendar")]
        public IActionResult CalendarScreen()
        {
			ViewDto viewDto = new ViewDto();

			try
			{
				var books = this._bookService.GetCalendarBooks();

				viewDto.UserBooks = books;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, MessageConstant.BOOK_RETRIEVAL_ERROR);
			}

			return PartialView(CommonConstant.U_CALENDAR_HTML, viewDto);
        }
    }
}
