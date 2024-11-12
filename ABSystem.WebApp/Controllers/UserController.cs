﻿using ABSystem.Resources.Constants;
using ABSystem.Services.Dto;
using ABSystem.Services.Interfaces;
using ABSystem.Services.Objects;
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

        [HttpGet]
        [Route("/admin/users-list/edit-user")]
        public IActionResult EditUser(int userId)
        {

            UserDto? userDto = _userService.GetUserById(userId);

            if(userDto == null)
            {
                return RedirectToAction("UsersList");
            }

            Console.WriteLine("Id: " + userDto.Id);
            Console.WriteLine("Email: " + userDto.EmailAddress);

            return PartialView("~/Views/Admin/EditUser.cshtml", userDto);
        }


        [HttpPost]
        [Route("/admin/users-list/add-user")]
        public IActionResult AddUser(UserDto dto)
        {

            if (!ModelState.IsValid)
            {

                return PartialView("~/Views/Admin/AddUser.cshtml", dto);
            }

            _userService.AddUser(dto);

            TempData["SuccessMessage"] = MessageConstant.ADDED_USER;

            return RedirectToAction("UsersList");
        }

        [HttpPost]
        [Route("/admin/users-list/delete-user")]
        public IActionResult DeleteUser(int userId)
        {
            _userService.DeleteUser(userId);

            TempData["SuccessMessage"] = MessageConstant.DELETED_USER;

            return RedirectToAction("UsersList");
        }
    }
}
