using DataLogic.Data;
using DataLogic.Entities;
using DataLogic.Interfaces;
using DatingApp.Common.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .SingleOrDefaultAsync(user => user.Email == email);
        }

        public async Task<bool> UserExists(string email)
        {
            return await _context.Users.AnyAsync(user => user.Email == email.ToLower());
        }

        public async Task<User> CreateNewUser(User user, string password)
        {
            using var hmac = new HMACSHA512();

            user.Email = user.Email.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            user.PasswordSalt = hmac.Key;
            user.Created = DateTime.UtcNow;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;

        }

        public async Task<User> Login(string email, string password)
        {
            var user = await GetUserByEmailAsync(email);

            if (user == null)
            {
                throw new ApiException
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Message = "Invalid Email"
                };
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    throw new ApiException
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        Message = "Invalid password"
                    };
                }
            }

            return user;
        }
    }
}
