using ABSystem.Resources.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABSystem.WebApp.Controllers
{
    public class SettingsController : Controller
    {
        [HttpGet]
        [Authorize(Roles = CommonConstant.Admin + "," + CommonConstant.Super)]
        public IActionResult AdminSettings()
        {
            return PartialView(CommonConstant.A_SETTINGS_HTML);
        }
    }
}
