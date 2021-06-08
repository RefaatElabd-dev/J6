using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using J6.DAL.Database;
using J6.DAL.Entities;

namespace J6.Controllers
{
    public class BrandsController : Controller
    {
        private readonly DbContainer _context;

        public BrandsController(DbContainer context)
        {
            _context = context;
        }

        // GET: Brands
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Brands.ToListAsync());
        //}

        //// GET: Brands/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var brand = await _context.Brands
        //        .FirstOrDefaultAsync(m => m.BrandId == id);
        //    if (brand == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(brand);
        //}

        //// GET: Brands/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Brands/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("BrandId,BrandName,CreatedAt,UpdatedAt")] Brand brand)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(brand);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(brand);
        //}

        //// GET: Brands/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var brand = await _context.Brands.FindAsync(id);
        //    if (brand == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(brand);
        //}

        //// POST: Brands/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("BrandId,BrandName,CreatedAt,UpdatedAt")] Brand brand)
        //{
        //    if (id != brand.BrandId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(brand);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!BrandExists(brand.BrandId))
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
        //    return View(brand);
        //}

        //// GET: Brands/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var brand = await _context.Brands
        //        .FirstOrDefaultAsync(m => m.BrandId == id);
        //    if (brand == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(brand);
        //}

        //// POST: Brands/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var brand = await _context.Brands.FindAsync(id);
        //    _context.Brands.Remove(brand);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool BrandExists(int id)
        //{
        //    return _context.Brands.Any(e => e.BrandId == id);
        //}
    }
}
