using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Core.DataAccess.EntityFrameworkCore;
using ProductManagement.Core.Entities.Concrete;
using ProductManagement.DataAccess.Abstract;
using ProductManagement.DataAccess.Concrete.EntityFrameworkCore.Contexts;
using ProductManagement.Entities.Concrete;

namespace ProductManagement.DataAccess.Concrete.EntityFrameworkCore
{
    public class UserRepository : EntityRepository<User, ApplicationDbContext>, IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Role> GetRoles(User user)
        {
            var result = _context.UserRoles.Include(q => q.Role).Where(q => q.UserId == user.Id)
                .Select(q => new Role()
                {
                    Id = q.Role.Id,
                    Name = q.Role.Name,
                    Description = q.Role.Description

                }).ToList();
            return result;
        }
    }
}
