using K207Shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K207Shopping.VM
{
    public class CheckoutVM
    {
        public List<Product> products { get; set; }
        public List<int> productIds { get; set; }
        public Task<k207User> K207User { get; set; }
    }
}
