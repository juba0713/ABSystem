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
        public void AddUser(UserDto dto);

        public IEnumerable<UserObj> GetUsers();
    }
}
