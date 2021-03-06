using System;
using System.Collections.Generic;
using System.Text;
using ProductManagement.Business.Abstract;
using ProductManagement.Business.Constants;
using ProductManagement.Core.Entities.Concrete;
using ProductManagement.Core.Utilities.Results;
using ProductManagement.Core.Utilities.Security.Hashing;
using ProductManagement.Core.Utilities.Security.Jwt;
using ProductManagement.Entities.Dtos;

namespace ProductManagement.Business.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;

        public AuthService(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<User> Register(UserRegister registerDto)
        {
            HashingAndVerifyPasswordHelper.CreatePasswordHash(registerDto.Password, out var passwordHash, out var passwordKey);
            var user = new User
            {
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                PasswordHash = passwordHash,
                PasswordKey = passwordKey,
                IsActive = true
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegisteredSuccessfully);
        }

        public IDataResult<User> Login(UserLogin loginDto)
        {
            var userToCheck = _userService.GetByMail(loginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingAndVerifyPasswordHelper.VerifyPasswordHash(loginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordKey))
            {
                return new ErrorDataResult<User>(Messages.PasswordIncorrect);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.UserLoginSuccessfully);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExist);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetRoles(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }
    }
}
