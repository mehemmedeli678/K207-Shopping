using K207Shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K207Shopping.VM
{
    public class CreateVM
    {
        public List<Category> categories { get; set; }
        public List<Color> Colors { get; set; }
        public List<Size> sizes { get; set; }
    }
}
