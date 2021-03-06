using System;
using System.Collections.Generic;
using System.Text;
using ProductManagement.Core.Entities;

namespace ProductManagement.Entities.Dtos
{
    public class UserLogin : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
