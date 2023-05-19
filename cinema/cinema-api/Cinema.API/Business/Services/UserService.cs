using AutoMapper;
using Business.DTOs;
using Business.Interfaces;
using DataLogic.Entities;
using DataLogic.Interfaces;
using DatingApp.Common.Errors;
using Microsoft.AspNetCore.Http;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, ITokenService tokenService, IUserRepository userRepository)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        public async Task<UserDto> Register(RegisterDto registerData)
        {
            if (await _userRepository.UserExists(registerData.Email))
            {
                throw new ApiException
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Email already exists"
                };
            }

            var userData = _mapper.Map<User>(registerData);
            var user = await _userRepository.CreateNewUser(userData, registerData.Password);

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                Token = _tokenService.CreateToken(user)
            };
        }

        public async Task<UserDto> Login(LoginDto loginData)
        {
            var user = await _userRepository.Login(loginData.Email, loginData.Password);

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}
