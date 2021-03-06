using System;
using System.Collections.Generic;
using System.Text;
using ProductManagement.Core.Entities;

namespace ProductManagement.Entities.Concrete
{
    public class Product: IEntity
    {
        public int Id  { get; set; }
        public string ProductName { get; set; }
        public long BarcodeNumber { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }


        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
