using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;

namespace CoreUI.Controllers
{
    public class SecurityLoginsRoleController : Controller
    {
        private readonly CareerCloudContext _context;

        public SecurityLoginsRoleController(CareerCloudContext context)
        {
            _context = context;
        }

        // GET: SecurityLoginsRole
        public async Task<IActionResult> Index()
        {
            var careerCloudContext = _context.SecurityLoginsRoles.Include(s => s.SecurityLogin).Include(s => s.SecurityRole);
            return View(await careerCloudContext.ToListAsync());
        }

        // GET: SecurityLoginsRole/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securityLoginsRolePoco = await _context.SecurityLoginsRoles
                .Include(s => s.SecurityLogin)
                .Include(s => s.SecurityRole)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (securityLoginsRolePoco == null)
            {
                return NotFound();
            }

            return View(securityLoginsRolePoco);
        }

        // GET: SecurityLoginsRole/Create
        public IActionResult Create()
        {
            ViewData["Login"] = new SelectList(_context.SecurityLogins, "Id", "EmailAddress");
            ViewData["Role"] = new SelectList(_context.SecurityRoles, "Id", "Role");
            return View();
        }

        // POST: SecurityLoginsRole/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Login,Role")] SecurityLoginsRolePoco securityLoginsRolePoco)
        {
            if (ModelState.IsValid)
            {
                securityLoginsRolePoco.Id = Guid.NewGuid();
                _context.Add(securityLoginsRolePoco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Login"] = new SelectList(_context.SecurityLogins, "Id", "EmailAddress", securityLoginsRolePoco.Login);
            ViewData["Role"] = new SelectList(_context.SecurityRoles, "Id", "Role", securityLoginsRolePoco.Role);
            return View(securityLoginsRolePoco);
        }

        // GET: SecurityLoginsRole/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securityLoginsRolePoco = await _context.SecurityLoginsRoles.FindAsync(id);
            if (securityLoginsRolePoco == null)
            {
                return NotFound();
            }
            ViewData["Login"] = new SelectList(_context.SecurityLogins, "Id", "EmailAddress", securityLoginsRolePoco.Login);
            ViewData["Role"] = new SelectList(_context.SecurityRoles, "Id", "Role", securityLoginsRolePoco.Role);
            return View(securityLoginsRolePoco);
        }

        // POST: SecurityLoginsRole/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Login,Role")] SecurityLoginsRolePoco securityLoginsRolePoco)
        {
            if (id != securityLoginsRolePoco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(securityLoginsRolePoco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SecurityLoginsRolePocoExists(securityLoginsRolePoco.Id))
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
            ViewData["Login"] = new SelectList(_context.SecurityLogins, "Id", "EmailAddress", securityLoginsRolePoco.Login);
            ViewData["Role"] = new SelectList(_context.SecurityRoles, "Id", "Role", securityLoginsRolePoco.Role);
            return View(securityLoginsRolePoco);
        }

        // GET: SecurityLoginsRole/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securityLoginsRolePoco = await _context.SecurityLoginsRoles
                .Include(s => s.SecurityLogin)
                .Include(s => s.SecurityRole)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (securityLoginsRolePoco == null)
            {
                return NotFound();
            }

            return View(securityLoginsRolePoco);
        }

        // POST: SecurityLoginsRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var securityLoginsRolePoco = await _context.SecurityLoginsRoles.FindAsync(id);
            _context.SecurityLoginsRoles.Remove(securityLoginsRolePoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SecurityLoginsRolePocoExists(Guid id)
        {
            return _context.SecurityLoginsRoles.Any(e => e.Id == id);
        }
    }
}
