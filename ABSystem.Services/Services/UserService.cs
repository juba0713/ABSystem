using ABSystem.Data.Interfaces;
using ABSystem.Data.Models;
using ABSystem.Resources.Constants;
using ABSystem.Services.Dto;
using ABSystem.Services.Interfaces;
using ABSystem.Services.Objects;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * @Author Julius.B
 * @Added 09/11/2024
 */
namespace ABSystem.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUserRepository userRepository, 
            IMapper mapper, 
            UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetLoggedInUserId()
        {
            // Get the user ID from the HttpContext
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);

            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException(MessageConstant.NO_USER_LOGIN);
            }

            return userId;
        }

        public async Task RegisterUser(RegisterDto dto)
        {
            var user = new User();

            _mapper.Map(dto, user);
            user.CreatedDate = DateTime.Now;
            user.UpdatedDate = DateTime.Now;
            user.IsDeleted = 0;
            user.LockoutEnabled = false;

            Console.WriteLine("-------------Id: " + user.Id);

            var creationResult = await _userManager.CreateAsync(user, dto.Password);

            if(!creationResult.Succeeded)
            {
                throw new Exception($"User creation failed. Errors");
            }

            var roleResult = await _userManager.AddToRoleAsync(user, CommonConstant.User);

            if (!roleResult.Succeeded)
            {
                throw new Exception($"Role creation failed. Errors");
            }

        }

        public async Task AddUser(UserDto dto)
        {
            var user = new User();

            _mapper.Map(dto, user);
            user.CreatedDate = DateTime.Now;
            user.UpdatedDate = DateTime.Now;
            user.IsDeleted = 0;
            user.LockoutEnabled = false;

            Console.WriteLine("-------------Id 1: " + user.Id);
        
            user.Id = Guid.NewGuid().ToString();
           
            Console.WriteLine("-------------Id 2: " + user.Id);

            var creationResult = await _userManager.CreateAsync(user, dto.Password);

            if (!creationResult.Succeeded)
            {
                throw new Exception($"User creation failed. Errors");
            }

            var roleResult = await _userManager.AddToRoleAsync(user, dto.Role);

            if (!roleResult.Succeeded)
            {
                throw new Exception($"Role creation failed. Errors");
            }
        }

        public async Task DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("No user found");
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                throw new Exception("Failed to delete user");
            }
        }


        public async Task EditUser(UserDto dto)
        {
            // Fetch the existing user by ID
            var user = await _userManager.FindByIdAsync(dto.Id);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            _mapper.Map(dto, user);
            user.UpdatedDate = DateTime.Now;

            // Optionally update the password
            if (!string.IsNullOrEmpty(dto.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResult = await _userManager.ResetPasswordAsync(user, token, dto.Password);

                if (!passwordResult.Succeeded)
                {
                    throw new Exception("Failed to update the password.");
                }
            }

            // Update the user in the database
            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                throw new Exception("Failed to update the user.");
            }

            if (!string.IsNullOrEmpty(dto.Role))
            {
                var currentRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                var roleResult = await _userManager.AddToRoleAsync(user, dto.Role);

                if (!roleResult.Succeeded)
                {
                    throw new Exception("Failed to update the role.");
                }
            }
        }

        public async Task<UserDto> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if(user == null)
            {
                return null;
            }

            var userDto = new UserDto();

             _mapper.Map(user, userDto);

            var roles = await _userManager.GetRolesAsync(user);

            userDto.Role = string.Join(", ", roles);

            return userDto;
        }

        public async Task<IEnumerable<UserObj>> GetUsers()
        {
            var users = _userRepository.GetUsers(); // Assuming this fetches all users from the database.

            if (!users.Any())
            {
                return new List<UserObj>();
            }

            var usersList = new List<UserObj>();

            foreach (var user in users)
            {
                // Retrieve roles for the user
                var roles = await _userManager.GetRolesAsync(user);

                // Map user and roles to UserObj
                var userObj = new UserObj
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = string.Join(", ", roles), // Concatenate roles if there are multiple
                    Email = user.Email,
                    CreatedDate = user.CreatedDate,
                    UpdatedDate = user.UpdatedDate
                };

                usersList.Add(userObj);
            }

            return usersList;
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user == null;
        }
    }
}
