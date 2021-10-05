using K207Shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K207Shopping.VM
{
    public class HomeVM
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public Product singleProduct { get; set; }
        public List<Slider> Sliders { get; set; }
    }
}
