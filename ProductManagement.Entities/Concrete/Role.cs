using System;
using System.Collections.Generic;
using System.Text;
using ProductManagement.Core.Entities;

namespace ProductManagement.Entities.Concrete
{
    public class Role:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual IEnumerable<UserRole> UserRoles { get; set; }
    }
}
