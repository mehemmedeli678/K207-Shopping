using K207Shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K207Shopping.VM
{
    public class ProductDetailVM
    {
        public Product Product { get; set; }
        public List<Product> FeaturedProduct { get; set; }
        public List<Product> SameCategoryPro { get; set; }

    }
}
