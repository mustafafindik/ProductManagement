using System;
using System.Collections.Generic;
using System.Text;
using ProductManagement.Core.Entities.Concrete;
using ProductManagement.Core.Utilities.Results;
using ProductManagement.Core.Utilities.Security.Jwt;
using ProductManagement.Entities.Dtos;

namespace ProductManagement.Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserRegister registerDto);
        IDataResult<User> Login(UserLogin loginDto);
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
