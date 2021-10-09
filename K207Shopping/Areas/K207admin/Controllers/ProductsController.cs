using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using K207Shopping.Data;
using K207Shopping.Models;
using K207Shopping.VM;
using iTextSharp.tool.xml.html;

namespace K207Shopping.Areas.K207admin.Controllers
{
    [Area("K207admin")]
    public class ProductsController : Controller
    {
        private readonly ShoppingContext _context;

        public ProductsController(ShoppingContext context)
        {
            _context = context;
        }

        // GET: K207admin/Products
        public async Task<IActionResult> Index()
        {
            var shoppingContext = _context.Products.Include(p => p.Category);
            return View(await shoppingContext.ToListAsync());
        }

        // GET: K207admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: K207admin/Products/Create
        public IActionResult Create()
        {
            CreateVM vm = new CreateVM()
            {
                categories = _context.Categories.ToList(),
                Colors = _context.Color.ToList(),
                sizes=_context.Size.ToList()
            };
            return View(vm);
        }

        // POST: K207admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(string Name, string Detail, int? Discount, int Price, int CategoryID, string SizeIDS, List<string> Photo, string colorIds)
        {
            JsonResult res = new JsonResult(new { });

            if (SizeIDS!=null && colorIds!=null)
            {
                Product newpro = new()
                {
                    Name = Name,
                    Description = Detail,
                    Discount = Discount,
                    Price = Price,
                    CategoryID = CategoryID,
                    isNew = true,
                    IsFeatured = true,
                    PublishDate = DateTime.Now,
                    ProductColors = new List<ProductColor>(),
                    ProductSizes = new List<ProductSize>()
                };

                var SizeID = SizeIDS.Split(",").Select(x => x.Split("-").Select(x => int.Parse(x)));
                var colorID = colorIds.Split(",").Select(x => x.Split("-").Select(x => int.Parse(x)));
                newpro.ProductSizes.AddRange(SizeID.Select(x => new ProductSize()
                {
                    ProductID=newpro.ID,
                    SizeID=x.First(),
                    Quantity=x.Last()
                }));
                newpro.ProductColors.AddRange(colorID.Select(x => new ProductColor()
                {
                    ProductID=newpro.ID,
                    ColorID=x.First(),
                    Quantity=x.Last()
                }));
                res.Value = new
                {
                    succes = true,
                    message = ""
                };
                await _context.AddAsync(newpro);
                await _context.SaveChangesAsync();
            }
            else
            {
                res.Value = new
                {
                    succes=false,
                    message="Size is not pushed"
                };
            }
            return Json(res);
        }

        // GET: K207admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "ID", product.CategoryID);
            return View(product);
        }

        // POST: K207admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,Price,Discount,isNew,IsFeatured,PublishDate,CategoryID")] Product product)
        {
            if (id != product.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "ID", product.CategoryID);
            return View(product);
        }

        // GET: K207admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: K207admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
    }
}
