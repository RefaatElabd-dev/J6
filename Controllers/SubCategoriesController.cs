using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using J6.DAL.Database;
using J6.DAL.Entities;
using J6.DAL.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace J6.Controllers
{
    public class SubCategoriesController : Controller
    {
        private readonly DbContainer _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SubCategoriesController(DbContainer context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _webHostEnvironment = hostEnvironment;
        }

        // GET: SubCategories
        public async Task<IActionResult> Index()
        {
            var dbContainer = _context.SubCategories.Include(s => s.Category);
            return View(await dbContainer.ToListAsync());
        }

        // GET: SubCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategory = await _context.SubCategories
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.SubcategoryId == id);
            if (subCategory == null)
            {
                return NotFound();
            }

            return View(subCategory);
        }

        // GET: SubCategories/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: SubCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                SubCategory subcategory = new SubCategory
                {
                    //  Id = model.Id,
                    SubcategoryName = model.SubcategoryName,
                    CreatedAt = model.CreatedAt,
                    UpdatedAt = model.UpdatedAt,
                    Image = uniqueFileName,
                    Content = model.Content,
                    CategoryId = model.CategoryId
                };
                _context.Add(subcategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", model.CategoryId);
            return View(model);
        }
        private string UploadedFile(SubCategoryViewModel model)
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

        //GET: SubCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SubCategory subCategory = await _context.SubCategories.Where(x => x.SubcategoryId == id).FirstOrDefaultAsync();
            SubCategoryViewModel viewModel = new SubCategoryViewModel
            {
                SubcategoryName = subCategory.SubcategoryName,
                CreatedAt = subCategory.CreatedAt,
                UpdatedAt = DateTime.Now,
                CategoryId = subCategory.CategoryId
            };
            if (subCategory == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", subCategory.CategoryId);
            return View(subCategory);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SubCategoryViewModel model, IFormFile file)
        {

            if (id != model.SubcategoryId)
            {
                return NotFound();
            }


            SubCategory subCategory = await _context.SubCategories.Where(x => x.SubcategoryId == id).FirstOrDefaultAsync();

            if (subCategory == null)
            {
                return NotFound();
            }



            subCategory.SubcategoryName = model.SubcategoryName;
            subCategory.CreatedAt = model.CreatedAt;
            subCategory.UpdatedAt = DateTime.Now;
            subCategory.Content = model.Content;
            subCategory.CategoryId = model.CategoryId;


            subCategory.Image = UploadedFile(model);

            if (subCategory.Image == null)
            {
                // model.Image = file;
                //category.Image = model.Image.Name;
            }



            _context.Update(subCategory);
            await _context.SaveChangesAsync();
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", subCategory.CategoryId);

            return RedirectToAction(nameof(Index));



            if (file != null || file.Length != 0)

            {
                string filename = System.Guid.NewGuid().ToString() + ".jpg";

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", filename);

                using (var stream = new FileStream(path, FileMode.Create))

                {
                    await file.CopyToAsync(stream);
                }
                subCategory.Image = filename;
            }



        }
















        // GET: SubCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategory = await _context.SubCategories
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.SubcategoryId == id);
            if (subCategory == null)
            {
                return NotFound();
            }

            return View(subCategory);
        }

        // POST: SubCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subCategory = await _context.SubCategories.FindAsync(id);
            _context.SubCategories.Remove(subCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubCategoryExists(int id)
        {
            return _context.SubCategories.Any(e => e.SubcategoryId == id);
        }
    }
}
