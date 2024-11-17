using ABSystem.Resources.Constants;
using ABSystem.Services.Dto;
using ABSystem.Services.Interfaces;
using ABSystem.Services.Objects;
using ABSystem.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/**
 * @Author Julius.B
 * @Added 09/11/2024
 */
namespace ABSystem.WebApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        public UserController(IUserService userService) {
            _userService = userService;
        }

        [HttpGet]
        [Route("/admin/users-list")]
        public async Task<IActionResult> UsersListScreen()
        {

            var users = await this._userService.GetUsers();

            return PartialView("~/Views/Admin/UsersList.cshtml", users);
        }

        [HttpGet]
        [Route("/admin/users-list/add-user")]
        public IActionResult AddUserScreen()
        {
            return PartialView("~/Views/Admin/AddUser.cshtml");
        }

        [HttpPost]
        [Route("/admin/users-list/add-user")]
        public async Task<IActionResult> AddUser(UserDto dto)
        {

            if (!ModelState.IsValid)
            {

                return PartialView("~/Views/Admin/AddUser.cshtml", dto);
            }

            try
            {
                await _userService.AddUser(dto);
            }
            catch (Exception ex)
            {
                ViewData["Error"] = MessageConstant.ERROR;
                return PartialView("~/Views/Admin/AddUser.cshtml", dto);
            }
            TempData["SuccessMessage"] = MessageConstant.ADDED_USER;
            return RedirectToAction("UsersListScreen");


        }

        [HttpGet]
        [Route("/admin/users-list/edit-user")]
        public async Task<IActionResult> EditUserScreen([FromQuery] string userId)
        {

            UserDto? userDto = await this._userService.GetUserById(userId);

            if(userDto == null)
            {
                return RedirectToAction("UsersListScreen");
            }

            return PartialView("~/Views/Admin/EditUser.cshtml", userDto);
        }

        [HttpPost]
        [Route("/admin/users-list/edit-user")]
        public async Task<IActionResult> EditUser(UserDto dto)
        {

            if (!ModelState.IsValid)
            {

                return PartialView("~/Views/Admin/EditUser.cshtml", dto);
            }

            try
            {
                await _userService.EditUser(dto);
            }
            catch (Exception ex)
            {
                ViewData["Error"] = MessageConstant.ERROR;
                return PartialView("~/Views/Admin/EditUser.cshtml", dto);
            }
            TempData["SuccessMessage"] = MessageConstant.EDITED_USER;
            return RedirectToAction("UsersListScreen");
        }

        [HttpPost]
        [Route("/admin/users-list/delete-user")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            await this._userService.DeleteUser(userId);

            TempData["SuccessMessage"] = MessageConstant.DELETED_USER;

            return RedirectToAction("UsersListScreen");
        }
    }
}
