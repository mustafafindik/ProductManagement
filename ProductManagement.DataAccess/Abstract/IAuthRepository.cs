using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProductManagement.Core.Entities.Concrete;

namespace ProductManagement.DataAccess.Abstract
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string userName, string password);
        Task<bool> UserExists(string userName);
    }
}
