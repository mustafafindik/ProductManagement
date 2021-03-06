using System.Collections.Generic;

namespace ProductManagement.Core.Entities.Concrete
{
    public class Role:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual IEnumerable<UserRole> UserRoles { get; set; }
    }
}
