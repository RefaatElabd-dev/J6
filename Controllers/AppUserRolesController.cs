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
    public class AppUserRolesController : Controller
    {
        private readonly DbContainer _context;

        public AppUserRolesController(DbContainer context)
        {
            _context = context;
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

        // GET: AppUserRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUserRole = await _context.UserRoles.FindAsync(id);
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
                    _context.Update(appUserRole);
                    await _context.SaveChangesAsync();
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
            var appUserRole = await _context.UserRoles.FindAsync(id);
            _context.UserRoles.Remove(appUserRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppUserRoleExists(int id)
        {
            return _context.UserRoles.Any(e => e.UserId == id);
        }
    }
}
