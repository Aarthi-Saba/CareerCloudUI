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
    public class SecurityRoleController : Controller
    {
        private readonly CareerCloudContext _context;

        public SecurityRoleController(CareerCloudContext context)
        {
            _context = context;
        }

        // GET: SecurityRole
        public async Task<IActionResult> Index()
        {
            return View(await _context.SecurityRoles.ToListAsync());
        }

        // GET: SecurityRole/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securityRolePoco = await _context.SecurityRoles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (securityRolePoco == null)
            {
                return NotFound();
            }

            return View(securityRolePoco);
        }

        // GET: SecurityRole/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SecurityRole/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Role,IsInactive")] SecurityRolePoco securityRolePoco)
        {
            if (ModelState.IsValid)
            {
                securityRolePoco.Id = Guid.NewGuid();
                _context.Add(securityRolePoco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(securityRolePoco);
        }

        // GET: SecurityRole/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securityRolePoco = await _context.SecurityRoles.FindAsync(id);
            if (securityRolePoco == null)
            {
                return NotFound();
            }
            return View(securityRolePoco);
        }

        // POST: SecurityRole/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Role,IsInactive")] SecurityRolePoco securityRolePoco)
        {
            if (id != securityRolePoco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(securityRolePoco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SecurityRolePocoExists(securityRolePoco.Id))
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
            return View(securityRolePoco);
        }

        // GET: SecurityRole/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securityRolePoco = await _context.SecurityRoles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (securityRolePoco == null)
            {
                return NotFound();
            }

            return View(securityRolePoco);
        }

        // POST: SecurityRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var securityRolePoco = await _context.SecurityRoles.FindAsync(id);
            _context.SecurityRoles.Remove(securityRolePoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SecurityRolePocoExists(Guid id)
        {
            return _context.SecurityRoles.Any(e => e.Id == id);
        }
    }
}
