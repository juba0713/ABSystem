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
    [Authorize(Roles = CommonConstant.Super + "," + CommonConstant.Admin)]
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

            return PartialView(CommonConstant.A_USERS_LIST_HTML, users);
        }

        [HttpGet]
        [Route("/admin/users-list/add-user")]
        public IActionResult AddUserScreen()
        {
            return PartialView(CommonConstant.A_USERS_ADD_HTML);
        }

        [HttpPost]
        [Route("/admin/users-list/add-user")]
        public async Task<IActionResult> AddUser(UserDto dto)
        {

            if (!ModelState.IsValid)
            {

                return PartialView(CommonConstant.A_USERS_ADD_HTML, dto);
            }

            try
            {
                await _userService.AddUser(dto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewData["Error"] = MessageConstant.ERROR;
                return PartialView(CommonConstant.A_USERS_ADD_HTML, dto);
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

            return PartialView(CommonConstant.A_USERS_EDIT_HTML, userDto);
        }

        [HttpPost]
        [Route("/admin/users-list/edit-user")]
        public async Task<IActionResult> EditUser(UserDto dto)
        {

            if (!ModelState.IsValid)
            {

                return PartialView(CommonConstant.A_USERS_EDIT_HTML, dto);
            }

            try
            {
                await _userService.EditUser(dto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewData["Error"] = MessageConstant.ERROR;
                return PartialView(CommonConstant.A_USERS_EDIT_HTML, dto);
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
