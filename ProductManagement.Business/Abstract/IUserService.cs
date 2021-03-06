using System;
using System.Collections.Generic;
using System.Text;
using ProductManagement.Entities.Concrete;

namespace ProductManagement.Business.Abstract
{
    public interface IUserService
    {
        List<Role> GetRoles(User user);
        void Add(User user);
        User GetByMail(string email);
    }
}
