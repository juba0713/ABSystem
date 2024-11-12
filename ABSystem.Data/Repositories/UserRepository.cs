﻿using ABSystem.Data.Interfaces;
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
namespace ABSystem.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ABSystemDbContext _context;

        public UserRepository(ABSystemDbContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void DeleteUser(int userId)
        {
            var user = _context.Users.Find(userId);

            if (user != null) { 
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public User? GetUserById(int userId)
        {

            var user = _context.Users.Find(userId);

            Console.WriteLine("EMAIL EMAIL: " + user?.EmailAddress);

            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }
    }
}
