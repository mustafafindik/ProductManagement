using System;
using System.Collections.Generic;
using System.Text;
using ProductManagement.Core.DataAccess;
using ProductManagement.Core.Entities.Concrete;
using ProductManagement.Entities.Concrete;

namespace ProductManagement.DataAccess.Abstract
{
    public interface IUserRepository : IEntityRepository<User>
    {
        List<Role> GetRoles(User user);
    }
}
