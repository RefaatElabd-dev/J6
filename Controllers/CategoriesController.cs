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
    public class CategoriesController : Controller
    {
        private readonly DbContainer _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public CategoriesController(DbContainer context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriesViewModel model)
        {

            if (ModelState.IsValid)
            {

                string uniqueFileName = UploadedFile(model);
                Category category = new Category
                {

                    CategoryName = model.CategoryName,
                    CreatedAt = model.CreatedAt,
                    UpdatedAt = model.UpdatedAt,
                    Content = model.Content,
                    Image = uniqueFileName,
                };
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        private string UploadedFile(CategoriesViewModel model)
        {
            string uniqueFileName = null;

            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
            }
            else
            {
                uniqueFileName = "100c4b49-f8ab-4272-988e-1739500fc52e_No-Photo-Available.jpg";
            }
            return uniqueFileName;
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category category = await _context.Categories.Where(x => x.CategoryId == id).FirstOrDefaultAsync();
            CategoriesViewModel viewModel = new CategoriesViewModel
            {
                CategoryName = category.CategoryName,
                CreatedAt = category.CreatedAt,
                UpdatedAt = DateTime.Now,
                //Image = CategoriesVi.Image;




            };
            // var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoriesViewModel model, IFormFile file)
        {

            if (id != model.CategoryId)
            {
                return NotFound();
            }


            Category category = await _context.Categories.Where(x => x.CategoryId == id).FirstOrDefaultAsync();

            if (category == null)
            {
                return NotFound();
            }



            category.CategoryName = model.CategoryName;
            category.CreatedAt = model.CreatedAt;
            category.UpdatedAt = DateTime.Now;
            category.Content = model.Content;

            category.Image = UploadedFile(model);

            if (category.Image == null)
            {
                // model.Image = file;
                //category.Image = model.Image.Name;
            }



            _context.Update(category);
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
                category.Image = filename;
            }






            //    //llllllll

            //    if (id != category.CategoryId)
            //    {
            //        return NotFound();
            //    }

            //    Category u = await _context.Categories.Where(x => x.CategoryId == id).FirstOrDefaultAsync();

            //    if (u == null)
            //    {
            //        return NotFound();
            //    }

            //    if (file != null )

            //    {

            //        string filename = System.Guid.NewGuid().ToString() + ".jpg";
            //        //Path.Combine(webHostEnvironment.WebRootPath, "images");
            //        var path = Path.Combine( Directory.GetCurrentDirectory(), "wwwroot", "img", filename);
            //        using (var stream = new FileStream(path, FileMode.Create))
            //        {
            //            await file.CopyToAsync(stream);
            //        }
            //        u.Image = filename;
            //    }


            //    u.CategoryName = model.CategoryName;
            //    u.CreatedAt = model.CreatedAt;
            //    u.UpdatedAt = DateTime.Now;
            //    u.Content = model.Content;


            //    u.Image = UploadedFile(model);

            //    await _context.SaveChangesAsync();

            //    return RedirectToAction(nameof(Index));

            //}


            //lllllllll

            //    if (id != category.CategoryId)
            //    {
            //        return NotFound();
            //    }

            //    if (ModelState.IsValid)
            //    {
            //        try
            //        {
            //            _context.Update(category);
            //            await _context.SaveChangesAsync();
            //        }
            //        catch (DbUpdateConcurrencyException)
            //        {
            //            if (!CategoryExists(category.CategoryId))
            //            {
            //                return NotFound();
            //            }
            //            else
            //            {
            //                throw;
            //            }
            //        }
            //        return RedirectToAction(nameof(Index));
            //    }
            //    return View(category);
        }


        //GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
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
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}