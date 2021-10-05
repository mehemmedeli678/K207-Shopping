using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using K207Shopping.Data;
using K207Shopping.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace K207Shopping.Areas.K207admin.Controllers
{
    [Area("K207admin")]
    [Authorize(Roles = "Admin")]
    public class AdminCategoriesController : Controller
    {
        private readonly ShoppingContext _context;
        private IWebHostEnvironment _environment;

        public AdminCategoriesController(ShoppingContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: K207admin/AdminCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: K207admin/AdminCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.ID == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: K207admin/AdminCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: K207admin/AdminCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,PictureUrl")] Category category,IFormFile PictureUrl)
        {
            if (ModelState.IsValid)
            {
                if (PictureUrl != null)
                {
                    string filename = Guid.NewGuid() + PictureUrl.FileName;
                    string uploadfolder = Path.Combine(_environment.WebRootPath, "uploads");
                    string imageFolder = Path.Combine(uploadfolder, filename);
                    using FileStream filestream = new FileStream(imageFolder, FileMode.Create);
                    await PictureUrl.CopyToAsync(filestream);
                    category.PictureUrl = filename;
                }
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: K207admin/AdminCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: K207admin/AdminCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,PictureUrl")] Category category,IFormFile PictureUrl)
        {
            if (id != category.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (PictureUrl != null)
                    {
                        string filename = Guid.NewGuid() + PictureUrl.FileName;
                        string uploadfolder = Path.Combine(_environment.WebRootPath, "uploads");
                        string imageFolder = Path.Combine(uploadfolder, filename);
                        using FileStream filestream = new FileStream(imageFolder, FileMode.Create);
                        await PictureUrl.CopyToAsync(filestream);
                        var oldPicture = Path.Combine(uploadfolder, category.PictureUrl);
                        if (System.IO.File.Exists(Path.Combine(oldPicture)))
                        {
                            System.IO.File.Delete(oldPicture);
                        }
                        category.PictureUrl = filename;
                    }
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.ID))
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
            return View(category);
        }

        // GET: K207admin/AdminCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.ID == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: K207admin/AdminCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.ID == id);
        }
    }
}
