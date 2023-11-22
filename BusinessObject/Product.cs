using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BusinessObject
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        [DisplayName("ID")]
        public int ProductId { get; set; }
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        [DisplayName("Name")]
        public string ProductName { get; set; } = null!;
        public string Weight { get; set; } = null!;
        [DisplayName("Unit Price")]
        public decimal UnitPrice { get; set; }
        [DisplayName("Unit In Stock")]
        public int UnitInStock { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
