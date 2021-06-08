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
    public class ProductsController : Controller
    {
        private readonly DbContainer _context;

        public ProductsController(DbContainer context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var dbContainer = _context.Products.Include(p => p.Promotion).Include(p => p.Subcategory);
            return View(await dbContainer.ToListAsync());

            
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Promotion)
                .Include(p => p.Subcategory)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            //ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName");
            ViewData["PromotionId"] = new SelectList(_context.Promotions, "Id", "Id");
            ViewData["SubcategoryId"] = new SelectList(_context.SubCategories, "SubcategoryId", "SubcategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Price,SoldQuantities,Quantity,Size,Color,ProductName,Model,SubcategoryId,Rating,Discount,Description,Ship,CreatedAt,UpdatedAt,DeletedAt,PromotionId,material,BrandName,Manufacture")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandId", product.BrandId);
            ViewData["PromotionId"] = new SelectList(_context.Promotions, "Id", "Id", product.PromotionId);
            ViewData["SubcategoryId"] = new SelectList(_context.SubCategories, "SubcategoryId", "SubcategoryId", product.SubcategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
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
            //ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandId", product.BrandId);
            ViewData["PromotionId"] = new SelectList(_context.Promotions, "Id", "Id", product.PromotionId);
            ViewData["SubcategoryId"] = new SelectList(_context.SubCategories, "SubcategoryId", "SubcategoryId", product.SubcategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Price,SoldQuantities,Quantity,Size,Color,ProductName,Model,SubcategoryId,Rating,Discount,Description,Ship,CreatedAt,UpdatedAt,DeletedAt,PromotionId,material,BrandName,Manufacture")] Product product)
        {
            if (id != product.ProductId)
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
                    if (!ProductExists(product.ProductId))
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
            //ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandId", product.BrandId);
            ViewData["PromotionId"] = new SelectList(_context.Promotions, "Id", "Id", product.PromotionId);
            ViewData["SubcategoryId"] = new SelectList(_context.SubCategories, "SubcategoryId", "SubcategoryId", product.SubcategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                
                .Include(p => p.Promotion)
                .Include(p => p.Subcategory)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
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
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
