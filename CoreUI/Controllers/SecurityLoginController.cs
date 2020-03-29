using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using CoreUI.Models;
using AutoMapper;

namespace CoreUI.Controllers
{
    public class SecurityLoginController : Controller
    {
        private readonly CareerCloudContext _context;

        public SecurityLoginController(CareerCloudContext context)
        {
            _context = context;
        }

        // GET: SecurityLogin
        public async Task<IActionResult> Index()
        {
            return View(await _context.SecurityLogins.ToListAsync());
        }

        // GET: SecurityLogin/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securityLoginPoco = await _context.SecurityLogins
                .FirstOrDefaultAsync(m => m.Id == id);
            if (securityLoginPoco == null)
            {
                return NotFound();
            }

            return View(securityLoginPoco);
        }

        // GET: SecurityLogin/Create
        public IActionResult Create()
        {
            if (!TempData.ContainsKey("securityFlag"))
            {
                ViewData["secFlag"] = true;
            }
            else
            {
                ViewData["secFlag"] = false;
            }
            return View();
        }

        // POST: SecurityLogin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SecurityLogin securityLogin)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<SecurityLogin, SecurityLoginPoco>());
                var mapper = config.CreateMapper();
                SecurityLoginPoco poco = mapper.Map<SecurityLoginPoco>(securityLogin);
                poco.Id = Guid.NewGuid();
                _context.Add(poco);
                await _context.SaveChangesAsync();
                TempData["Login"] = poco.Id;
                return RedirectToAction("CreateProfile","ApplicantProfile",new { id = poco.Id });
            }
            return View(securityLogin);
        }

        // GET: SecurityLogin/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securityLoginPoco = await _context.SecurityLogins.FindAsync(id);
            if (securityLoginPoco == null)
            {
                return NotFound();
            }
            return View(securityLoginPoco);
        }

        // POST: SecurityLogin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Login,Password,Created,PasswordUpdate,AgreementAccepted,IsLocked,IsInactive,EmailAddress,PhoneNumber,FullName,ForceChangePassword,PrefferredLanguage")] SecurityLoginPoco securityLoginPoco)
        {
            if (id != securityLoginPoco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(securityLoginPoco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SecurityLoginPocoExists(securityLoginPoco.Id))
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
            return View(securityLoginPoco);
        }

        // GET: SecurityLogin/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var securityLoginPoco = await _context.SecurityLogins
                .FirstOrDefaultAsync(m => m.Id == id);
            if (securityLoginPoco == null)
            {
                return NotFound();
            }

            return View(securityLoginPoco);
        }

        // POST: SecurityLogin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var securityLoginPoco = await _context.SecurityLogins.FindAsync(id);
            _context.SecurityLogins.Remove(securityLoginPoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SecurityLoginPocoExists(Guid id)
        {
            return _context.SecurityLogins.Any(e => e.Id == id);
        }
    }
}
