using System;
using System.Collections.Generic;
using System.Text;
using ProductManagement.Core.Entities.Concrete;

namespace ProductManagement.Core.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<Role> roles);
    }
}
