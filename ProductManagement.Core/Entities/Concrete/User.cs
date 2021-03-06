using System.Collections.Generic;

namespace ProductManagement.Core.Entities.Concrete
{
    public class User:IEntity
    {
       

        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordKey { get; set; }

        public virtual IEnumerable<UserRole> UserRoles { get; set; }
    }
}
