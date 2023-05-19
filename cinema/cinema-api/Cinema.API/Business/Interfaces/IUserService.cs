using Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> Register(RegisterDto registerData);

        Task<UserDto> Login(LoginDto loginData);
    }
}
