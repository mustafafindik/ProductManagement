using System;
using System.Collections.Generic;
using System.Text;
using ProductManagement.Core.Entities;

namespace ProductManagement.Entities.Concrete
{
    public class ProductImage: IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string ImagePath { get; set; }
    }
}
