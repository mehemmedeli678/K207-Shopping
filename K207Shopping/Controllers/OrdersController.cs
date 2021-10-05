using K207Shopping.Data;
using K207Shopping.Models;
using K207Shopping.VM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K207Shopping.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ShoppingContext _context;
        private readonly UserManager<k207User> _userManager;

        public OrdersController(ShoppingContext context, UserManager<k207User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpPost]
        public IActionResult AddToCart()
        {
            var productCookie = Request.Cookies["k207Cart"];
            if (productCookie != null && productCookie.Length > 0)
            {
                List<int> productIds = productCookie.Split('-').Select(p => int.Parse(p)).ToList();
                List<Product> products = _context.Products.Include("ProductPicture.Picture").Where(x => productIds.Contains(x.ID)).ToList();
                return PartialView("BasketProduct", products);
            }
            return PartialView("BasketProduct", null);
        }
        public IActionResult Checkout()
        {
               var productCookie = Request.Cookies["k207Cart"];
            if (productCookie != null && productCookie.Length > 0)
            {
                List<int> productIds = productCookie.Split('-').Select(p => int.Parse(p)).ToList();
                List<Product> products = _context.Products.Where(p => productIds.Contains(p.ID)).ToList();
                CheckoutVM vm = new CheckoutVM() {
                    products = products,
                    productIds = productIds,
                    K207User = _userManager.GetUserAsync(User)
                };
            return View(vm);
            }
            return View ();
        }
    }
}
