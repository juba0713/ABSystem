using ABSystem.Resources.Constants;
using Microsoft.AspNetCore.Mvc;

namespace ABSystem.WebApp.Controllers
{
    public class CalendarController : Controller
    {
        [HttpGet]
        [Route("/calendar")]
        public IActionResult CalendarScreen()
        {
            return PartialView(CommonConstant.U_CALENDAR_HTML);
        }
    }
}
