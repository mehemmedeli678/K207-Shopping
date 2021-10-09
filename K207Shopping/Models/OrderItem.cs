using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace K207Shopping.Models
{
    public class OrderItem:BaseEntity
    {
        public int OrderID { get; set; }
        [NotMapped]
        public string ProductName { get; set; }
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
        public decimal ItemPrice { get; set; }
        public int Quantity { get; set; }
    }
}
