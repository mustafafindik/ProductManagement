using System;
using System.Collections.Generic;
using System.Text;
using ProductManagement.Business.Abstract;
using ProductManagement.DataAccess.Abstract;
using ProductManagement.Entities.Concrete;

namespace ProductManagement.Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<Role> GetRoles(User user)
        {
            return _userRepository.GetRoles(user);
        }

        public void Add(User user)
        {
            _userRepository.Add(user);
        }

        public User GetByMail(string email)
        {
            return _userRepository.Get(q => q.Email == email);
        }
    }
}
