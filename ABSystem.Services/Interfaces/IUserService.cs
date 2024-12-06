using ABSystem.Data.Models;
using ABSystem.Services.Dto;
using ABSystem.Services.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * @Author Julius.B
 * @Added 09/11/2024
 */
namespace ABSystem.Services.Interfaces
{
    public interface IUserService
    {

        public string GetLoggedInUserId();

        public Task RegisterUser(RegisterDto dto);

        public Task AddUser(UserDto dto);

        public Task EditUser(UserDto dto);

        public Task<IEnumerable<UserObj>> GetUsers();

        public Task DeleteUser(string userId);

        public Task<UserDto> GetUserById(string userId);

        public Task<bool> IsEmailUniqueAsync(string email);
    }
}
