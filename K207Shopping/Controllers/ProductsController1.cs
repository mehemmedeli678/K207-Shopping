using K207Shopping.Data;
using K207Shopping.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K207Shopping.Controllers
{
    public class ProductsController1 : Controller
    {
        private ShoppingContext _context;
        
        public ProductsController1(ShoppingContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FindProductById(int? proId)
        {
            var productList = _context.Products.Include("ProductPicture.Picture").FirstOrDefault(m => m.ID == proId);
            HomeVM vm = new HomeVM()
            {
                singleProduct = productList
            };
            return PartialView("ProductQuickView",vm);
           
        }
        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();
            var singlepro = _context.Products.Include("ProductPicture.Picture").FirstOrDefault(p => p.ID == id);
            if (singlepro == null) return NotFound();
            ProductDetailVM vm = new ProductDetailVM();
            vm.Product = singlepro;
            vm.FeaturedProduct = _context.Products.Include("ProductPicture.Picture").Where(x => x.IsFeatured).ToList();
            vm.SameCategoryPro = _context.Products.Include("ProductPicture.Picture").Where(x => x.CategoryID==singlepro.CategoryID && x.ID!=singlepro.ID).Take(10).ToList();
            return View(vm);
        }
    }
}
