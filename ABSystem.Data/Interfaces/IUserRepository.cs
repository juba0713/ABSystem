using ABSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * @Author Julius.B
 * @Added 09/11/2024
 */
namespace ABSystem.Data.Interfaces
{
    public interface IUserRepository
    {
        public void AddUser(User user);

        public void EditUser(User user);

        public IEnumerable<User> GetUsers();

        public void DeleteUser(int userId);

        public User? GetUserById(int userId);

        public User? GetUserByEmail(string email);
    }
}
