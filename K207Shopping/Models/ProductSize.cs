using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K207Shopping.Models
{
    public class ProductSize
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int SizeID { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public Size Size { get; set; }
    }
}
