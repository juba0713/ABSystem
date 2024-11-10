using ABSystem.Services.Dto;
using ABSystem.Services.Interfaces;
using ABSystem.Services.Services;
using Microsoft.AspNetCore.Mvc;

/**
 * @Author Julius.B
 * @Added 09/11/2024
 */
namespace ABSystem.WebApp.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        public UserController(IUserService userService) {
            _userService = userService;
        }

        [HttpGet]
        [Route("/admin/users-list")]
        public IActionResult UsersList()
        {

            var users = _userService.GetUsers();

            return PartialView("~/Views/Admin/UsersList.cshtml", users);
        }

        [HttpGet]
        [Route("/admin/users-list/add-user")]
        public IActionResult AddUser()
        {
            return PartialView("~/Views/Admin/AddUser.cshtml");
        }

        [HttpPost]
        [Route("/admin/users-list/add-user")]
        public IActionResult AddUser(UserDto dto)
        {

            _userService.AddUser(dto);

            return RedirectToAction("UsersList");
        }
    }
}
