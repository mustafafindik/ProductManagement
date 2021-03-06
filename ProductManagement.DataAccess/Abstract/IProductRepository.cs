using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProductManagement.Core.DataAccess;
using ProductManagement.Entities.Concrete;

namespace ProductManagement.DataAccess.Abstract
{
    public interface IProductRepository: IEntityRepository<Product>
    {
       
    }
}
