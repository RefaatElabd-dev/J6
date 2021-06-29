using AutoMapper;
using J6.BL.Servises;
using J6.DAL.Database;
using J6.DAL.Entities;
using J6.DAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace J6.Controllers
{
    public class SellerController : Controller
    {
        private readonly DbContainer _context;
        private readonly ITokenServices _tokenService;
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHostingEnvironment _hostingEnvironment;

        public SellerController(UserManager<AppUser> userManager, IMapper mapper, DbContainer context, ITokenServices tokenService, IWebHostEnvironment hostEnvironment, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _tokenService = tokenService;
            this.userManager = userManager;
            this.mapper = mapper;
            _webHostEnvironment = hostEnvironment;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: SellerController
        [Authorize(Roles ="Seller")]
        public async Task<IActionResult> GetSellerProduct(int id)
        {
            //seller id
            var Sellers = await userManager.GetUsersInRoleAsync("Seller");
            var Seller = Sellers.SingleOrDefault(S => S.Id == id);
            if (Seller == null) return NotFound("No Seller Matched");
            var product = await _context.Products.Where(q => q.SellerId == Seller.Id).Include(a => a.Promotion).Include(c => c.ProdCarts).Include(p => p.ProdOrders).Include(i => i.Reviews).Include(y => y.Views).Include(y=>y.Subcategory).Include(y=>y.Brand).Include(y=>y.Subcategory.Category).ToListAsync();
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: SellerController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Promotion)
                .Include(p => p.Subcategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: SellerController/Create
        public ActionResult sellerAddProduct()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName");
            ViewData["PromotionId"] = new SelectList(_context.Promotions, "PromotionId", "PromotionId");
            ViewData["SubcategoryId"] = new SelectList(_context.SubCategories, "SubcategoryId", "SubcategoryName");
            ViewData["SellerId"] = new SelectList(_context.Users, "Id", "UserName");

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> sellerAddProduct(ProductsEditViewModel promodel)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(promodel);
                Product product = new Product
                {
                    Price = promodel.Price,
                    SoldQuantities = promodel.SoldQuantities,
                    Quantity = promodel.Quantity,
                    Color = promodel.Color,
                    Size = promodel.Size,
                    ProductName = promodel.ProductName,
                    Model = promodel.Model,
                    Rating = promodel.Rating,
                    Discount = promodel.Discount,
                    Ship = promodel.Ship,
                    material = promodel.material,
                    SellerId = promodel.SellerId,
                    BrandId = promodel.BrandId,
                    PromotionId = promodel.PromotionId,
                    SubcategoryId = promodel.SubcategoryId,
                    Manufacture = promodel.Manufacture,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Image = uniqueFileName,
                    Description = promodel.Description,
                };
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("GetSellerProduct", new { id = promodel.SellerId});
                //return CreatedAtAction("GetSellerProduct", new { id = promodel.Id }, promodel);

            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName");
            ViewData["PromotionId"] = new SelectList(_context.Promotions, "PromotionId", "PromotionId", promodel.PromotionId);
            ViewData["SubcategoryId"] = new SelectList(_context.SubCategories, "SubcategoryId", "SubcategoryName");
            ViewData["SellerId"] = new SelectList(_context.Users, "Id", "UserName");

            //return CreatedAtAction("GetProduct", new { id = prod.Id }, prod);
            return RedirectToAction("GetSellerProduct",new { id= promodel.SellerId});
        }
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        private string UploadedFile(ProductsEditViewModel model)
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
            else
            {
                uniqueFileName = "100c4b49-f8ab-4272-988e-1739500fc52e_No-Photo-Available.jpg";
            }
            return uniqueFileName;
        }
        // GET: Products/Edit/5
        public async Task<IActionResult> PutSellerProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product product = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();

            ProductsEditViewModel viewModel = new ProductsEditViewModel
            {

                Price = product.Price,
                SoldQuantities = product.SoldQuantities,
                Quantity = product.Quantity,
                Color = product.Color,
                Size = product.Size,
                ProductName = product.ProductName,
                Model = product.Model,
                Rating = product.Rating,
                Discount = product.Discount,
                Ship = product.Ship,
                material = product.material,
                SellerId = product.SellerId,
                BrandId = product.BrandId,
                PromotionId = product.PromotionId,
                SubcategoryId = product.SubcategoryId,
                Manufacture = product.Manufacture,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Description = product.Description,
            };
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName");
            ViewData["PromotionId"] = new SelectList(_context.Promotions, "PromotionId", "PromotionId", product.PromotionId);
            ViewData["SubcategoryId"] = new SelectList(_context.SubCategories, "SubcategoryId", "SubcategoryName");

            return View(viewModel);
            //return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PutSellerProduct(int id, ProductsEditViewModel promodel, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (id != promodel.Id)
                {
                    return NotFound();
                }
                Product product = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
                product.Price = promodel.Price;
                product.SoldQuantities = promodel.SoldQuantities;
                product.Quantity = promodel.Quantity;
                product.Color = promodel.Color;
                product.Size = promodel.Size;
                product.ProductName = promodel.ProductName;
                product.Model = promodel.Model;
                product.Rating = promodel.Rating;
                product.Discount = promodel.Discount;
                product.Ship = promodel.Ship;
                product.material = promodel.material;
                product.SellerId = promodel.SellerId;
                product.BrandId = promodel.BrandId;
                product.PromotionId = promodel.PromotionId;
                product.SubcategoryId = promodel.SubcategoryId;
                product.Manufacture = promodel.Manufacture;
                product.CreatedAt = DateTime.Now;
                product.UpdatedAt = DateTime.Now;
                product.Description = promodel.Description;

                if (promodel.Image != null)
                {
                    if (promodel.Image != null)
                    {
                        string filepath = Path.Combine(_hostingEnvironment.WebRootPath, "images", promodel.Image.ToString());
                        System.IO.File.Delete(filepath);
                    }
                    product.Image = UploadedFile(promodel);
                }
                _context.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("GetSellerProduct", new { id = promodel.SellerId });
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName");
            ViewData["PromotionId"] = new SelectList(_context.Promotions, "PromotionId", "PromotionId", promodel.PromotionId);
            ViewData["SubcategoryId"] = new SelectList(_context.SubCategories, "SubcategoryId", "SubcategoryName");
            return View();
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Promotion)
                .Include(p => p.Subcategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id,AppUser user)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            AppUser CurrentUser = await userManager.FindByNameAsync(User.Identity.Name);
            return RedirectToAction("GetSellerProduct", new { CurrentUser.Id });
        }
    }
}
