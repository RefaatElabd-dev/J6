using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using J6.DAL.Database;
using J6.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using J6.BL.Servises;

namespace J6.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AppUserRolesController : Controller
    {
        private readonly DbContainer _context;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IAdminStatisticsService _adminStatistics;
        private readonly UserManager<AppUser> _userManager;

        public AppUserRolesController(DbContainer context,RoleManager<AppRole> roleManager, IAdminStatisticsService adminStatistics, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _adminStatistics = adminStatistics;
            _context = context;
            _userManager = userManager;
        }

        // GET: AppUserRoles
        public async Task<IActionResult> Index()
        {
            var dbContainer = _context.UserRoles.Include(a => a.Role).Include(a => a.user);
            return View(await dbContainer.ToListAsync());
        }

        // GET: AppUserRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUserRole = await _context.UserRoles
                .Include(a => a.Role)
                .Include(a => a.user)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (appUserRole == null)
            {
                return NotFound();
            }

            return View(appUserRole);
        }

        // GET: AppUserRoles/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: AppUserRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,RoleId")] AppUserRole appUserRole)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(appUserRole);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    //Handel Error Here
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return View(appUserRole);
        }

        // GET: AppUserRoles/ApproveSeller
        public IActionResult ApproveSeller()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users.Where(p => p.IsActive == false), "Id", "UserName");
            return View();
        }

        // POST: AppUserRoles/ApproveSeller
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveSeller(int id,AppUser appUser)
        {

            if (id != appUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var seller = await _userManager.FindByIdAsync(id.ToString());
                    seller.IsActive = true;
                    await _userManager.UpdateAsync(seller);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppUserRoleExists(appUser.Id))
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
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users.Where(p => p.IsActive == false), "Id", "UserName");
            return View(appUser);
        }

        // GET: AppUserRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUserRole = await _context.UserRoles.FirstOrDefaultAsync(p => p.UserId == id);
            if (appUserRole == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", appUserRole.RoleId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", appUserRole.UserId);
            return View(appUserRole);
        }

        // POST: AppUserRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,RoleId")] AppUserRole appUserRole)
        {
            if (id != appUserRole.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    AppRole role = await _roleManager.FindByIdAsync(appUserRole.RoleId.ToString());
                    AppUser user = await _userManager.FindByIdAsync(id.ToString());
                    var oldRoles = await _userManager.GetRolesAsync(user);
                    string OldRole = oldRoles.First();
                    var result = await _userManager.RemoveFromRoleAsync(user, OldRole);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, role.Name);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppUserRoleExists(appUserRole.UserId))
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

            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", appUserRole.RoleId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", appUserRole.UserId);
            return View(appUserRole);
        }

        // GET: AppUserRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUserRole = await _context.UserRoles
                .Include(a => a.Role)
                .Include(a => a.user)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (appUserRole == null)
            {
                return NotFound();
            }

            return View(appUserRole);
        }

        // POST: AppUserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            AppUser user = await _userManager.FindByIdAsync(id.ToString());
            var oldRoles = await _userManager.GetRolesAsync(user);
            string OldRole = oldRoles.First();
            var result = await _userManager.RemoveFromRoleAsync(user, OldRole);
            return RedirectToAction(nameof(Index));
        }

        private bool AppUserRoleExists(int id)
        {
            return _context.UserRoles.Any(e => e.UserId == id);
        }
    }
}
