using ABSystem.Data.Interfaces;
using ABSystem.Data.Models;
using ABSystem.Services.Dto;
using ABSystem.Services.Interfaces;
using ABSystem.Services.Objects;
using AutoMapper;
using System;
using System.Collections.Generic;
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

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public void AddUser(UserDto dto)
        {
            var user = new User();

            _mapper.Map(dto, user);
            user.CreatedDate = DateTime.Now;
            user.UpdatedDate = DateTime.Now;
            user.IsDeleted = 0;

            _userRepository.AddUser(user);
        }

        public void DeleteUser(int userId)
        {
            _userRepository.DeleteUser(userId);
        }

        public void EditUser(UserDto dto)
        {
            var user = new User();

            _mapper.Map(dto, user);
            user.UpdatedDate = DateTime.Now;

            _userRepository.EditUser(user);
        }

        public UserDto? GetUserById(int userId)
        {
            var user = _userRepository.GetUserById(userId);

            if(user == null)
            {
                return null;
            }

            var userDto = new UserDto();

             _mapper.Map(user, userDto);

            return userDto;
        }

        public IEnumerable<UserObj> GetUsers()
        {
            var users = _userRepository.GetUsers();

            if (!users.Any())
            {
                return new List<UserObj>();
            }

            var usersList = users.Select(user => new UserObj
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                EmailAddress = user.EmailAddress,
                CreatedDate = user.CreatedDate,
                UpdatedDate = user.UpdatedDate
            });

            return usersList;
        }
    }
}
