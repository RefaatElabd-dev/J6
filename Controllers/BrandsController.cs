using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using J6.DAL.Database;
using J6.DAL.Entities;
using Microsoft.AspNetCore.Hosting;
using J6.DAL.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace J6.Controllers
{
    public class BrandsController : Controller
    {
        private readonly DbContainer _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BrandsController(DbContainer context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _webHostEnvironment = hostEnvironment;
        }

        // GET: Brands
        public async Task<IActionResult> Index()
        {
            return View(await _context.Brands.ToListAsync());
        }

        // GET: Brands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .FirstOrDefaultAsync(m => m.BrandId == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // GET: Brands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Brands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandsViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                string uniqueFileName = UploadedFile(model);
                Brand brand = new Brand
                {
                    BrandName = model.BrandName,
                    CreatedAt = model.CreatedAt,
                    UpdatedAt = model.UpdatedAt,
                    Image = uniqueFileName,
                };
                _context.Add(brand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }



        private string UploadedFile(BrandsViewModel model)
        {
            string uniqueFileName = null;

            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }







        // GET: brands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Brand brand = await _context.Brands.Where(x => x.BrandId == id).FirstOrDefaultAsync();
            BrandsViewModel viewModel = new BrandsViewModel
            {
                BrandName = brand.BrandName,
                CreatedAt = brand.CreatedAt,
                UpdatedAt = DateTime.Now,
                //Image = CategoriesVi.Image;

            };
            // var category = await _context.Categories.FindAsync(id);

            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // POST: brands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,IFormFile file ,BrandsViewModel model)
        {
            if (id == null)
            {
                return NotFound();
            }
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            brand.BrandName = model.BrandName;
            brand.CreatedAt = model.CreatedAt;
            brand.UpdatedAt = DateTime.Now;
            brand.Image = UploadedFile(model);

            if (brand.Image == null)
            {
                // model.Image = file;
                //category.Image = model.Image.Name;
            }
            _context.Update(brand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            if (file != null || file.Length != 0)
            {
                string filename = System.Guid.NewGuid().ToString() + ".jpg";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", filename);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                brand.Image = filename;
            }
        }

        // GET: Brands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _context.Brands
                .FirstOrDefaultAsync(m => m.BrandId == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrandExists(int id)
        {
            return _context.Brands.Any(e => e.BrandId == id);
        }
    }
}
