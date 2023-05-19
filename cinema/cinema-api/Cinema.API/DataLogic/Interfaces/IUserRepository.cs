using DataLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> UserExists(string email);

        Task<User> CreateNewUser(User user, string password);

        Task<User> Login(string email, string password);

        Task<User?> GetUserByEmailAsync(string email);
    }

}
