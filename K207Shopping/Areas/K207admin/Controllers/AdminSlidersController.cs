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
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace K207Shopping.Areas.K207admin.Controllers
{
    [Area("K207admin")]

    [Authorize(Roles = "Admin")]
    public class AdminSlidersController : Controller
    {
        private readonly ShoppingContext _context;
        private IWebHostEnvironment _environment;


        public AdminSlidersController(ShoppingContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: K207admin/AdminSliders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sliders.ToListAsync());
        }

        // GET: K207admin/AdminSliders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Sliders
                .FirstOrDefaultAsync(m => m.ID == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // GET: K207admin/AdminSliders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: K207admin/AdminSliders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PhoroUrl,SubHeader,Header,Description")] Slider slider,IFormFile PhoroUrl)
        {
            if (ModelState.IsValid)
            {
                if (PhoroUrl != null)
                {
                    string filename = Guid.NewGuid() + PhoroUrl.FileName;
                    string uploadfolder = Path.Combine(_environment.WebRootPath, "uploads");
                    string imageFolder = Path.Combine(uploadfolder, filename);
                    using FileStream filestream = new FileStream(imageFolder, FileMode.Create);
                    await PhoroUrl.CopyToAsync(filestream);
                    slider.PhoroUrl = filename;
                }
                _context.Add(slider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }

        // GET: K207admin/AdminSliders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }

        // POST: K207admin/AdminSliders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PhoroUrl,SubHeader,Header,Description")] Slider slider,IFormFile PhoroUrl)
        {
            if (id != slider.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (PhoroUrl != null)
                    {
                        string filename = Guid.NewGuid() + PhoroUrl.FileName;
                        string uploadfolder = Path.Combine(_environment.WebRootPath, "uploads");
                        string imageFolder = Path.Combine(uploadfolder, filename);
                        using FileStream filestream = new FileStream(imageFolder, FileMode.Create);
                        await PhoroUrl.CopyToAsync(filestream);
                        var oldPicture = Path.Combine(uploadfolder, slider.PhoroUrl);
                        if (System.IO.File.Exists(Path.Combine(oldPicture)))
                        {
                            System.IO.File.Delete(oldPicture);
                        }
                        slider.PhoroUrl = filename;
                    }
                    _context.Update(slider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SliderExists(slider.ID))
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
            return View(slider);
        }

        // GET: K207admin/AdminSliders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Sliders
                .FirstOrDefaultAsync(m => m.ID == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // POST: K207admin/AdminSliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var slider = await _context.Sliders.FindAsync(id);
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SliderExists(int id)
        {
            return _context.Sliders.Any(e => e.ID == id);
        }
    }
}
