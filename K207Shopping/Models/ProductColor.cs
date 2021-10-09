using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K207Shopping.Models
{
    public class ProductColor
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int ColorID { get; set; }
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }
        public virtual Color Color { get; set; }
    }
}
