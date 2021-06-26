using AutoMapper;
using J6.BL.Servises;
using J6.DAL.Database;
using J6.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public SellerController(UserManager<AppUser> userManager, IMapper mapper, DbContainer context, ITokenServices tokenService)
        {
            _context = context;
            _tokenService = tokenService;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        // GET: SellerController
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public async Task<IActionResult> GetSellerProduct(int id)
        {
            //seller id
            var Sellers = await userManager.GetUsersInRoleAsync("Seller");
            var Seller = Sellers.SingleOrDefault(S => S.Id == id);
            if (Seller == null) return NotFound("No Seller Matched");
            var product = await _context.Products.Where(q => q.SellerId == Seller.Id).Include(a => a.Promotion).Include(c => c.ProdCarts).Include(p => p.ProdOrders).Include(i => i.Reviews).Include(y => y.Views).ToListAsync();
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: SellerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SellerController/Create
        public ActionResult sellerAddProduct()
        {
            return View();
        }

        //// POST: SellerController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        //seller add product
        [HttpPost]
        public async Task<ActionResult> sellerAddProduct(Product prod)
        {

            _context.Products.Add(prod);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(prod.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            //return CreatedAtAction("GetProduct", new { id = prod.Id }, prod);
            return RedirectToAction("GetSellerProduct",new { id= prod.SellerId});
        }
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        // GET: SellerController/Edit/5
        public ActionResult PutSellerProduct(int id)
        {
            return View();
        }

        // POST: SellerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        public async Task<IActionResult> PutSellerProduct(int id, Product product, int sellerid)
        {// id is product id
            if (id != product.Id || sellerid != product.SellerId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // GET: SellerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SellerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
