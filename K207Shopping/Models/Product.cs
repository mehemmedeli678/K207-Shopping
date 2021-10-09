using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace K207Shopping.Models
{
    public class Product
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(300)]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public bool isNew { get; set; }
        public bool IsFeatured { get; set; }
        public DateTime PublishDate { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public List<ProductPicture> ProductPicture { get; set; }
        public List<ProductColor> ProductColors { get; set; }
        public List<ProductSize> ProductSizes { get; set; }
    }
}
