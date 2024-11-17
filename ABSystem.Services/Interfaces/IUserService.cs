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

        public Task RegisterUser(RegisterDto dto);

        public Task<bool> AddUser(UserDto dto);

        public void EditUser(UserDto dto);

        public IEnumerable<UserObj> GetUsers();

        public void DeleteUser(int userId);

        public UserDto? GetUserById(int userId);
    }
}
