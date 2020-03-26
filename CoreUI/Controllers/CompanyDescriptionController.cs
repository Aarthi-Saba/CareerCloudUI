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
    public class CompanyDescriptionController : Controller
    {
        private readonly CareerCloudContext _context;

        public CompanyDescriptionController(CareerCloudContext context)
        {
            _context = context;
        }

        // GET: CompanyDescription
        public async Task<IActionResult> Index()
        {
            var careerCloudContext = _context.CompanyDescriptions.Include(c => c.CompanyProfile).Include(c => c.SystemLanguageCode);
            return View(await careerCloudContext.ToListAsync());
        }

        // GET: CompanyDescription/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyDescriptionPoco = await _context.CompanyDescriptions
                .Include(c => c.CompanyProfile)
                .Include(c => c.SystemLanguageCode)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyDescriptionPoco == null)
            {
                return NotFound();
            }

            return View(companyDescriptionPoco);
        }

        // GET: CompanyDescription/Create
        public IActionResult Create()
        {
            ViewData["Company"] = new SelectList(_context.CompanyProfiles, "Id", "CompanyWebsite");
            ViewData["LanguageId"] = new SelectList(_context.SystemLanguageCodes, "LanguageID", "LanguageID");
            return View();
        }

        // POST: CompanyDescription/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Company,LanguageId,CompanyName,CompanyDescription")] CompanyDescriptionPoco companyDescriptionPoco)
        {
            if (ModelState.IsValid)
            {
                companyDescriptionPoco.Id = Guid.NewGuid();
                _context.Add(companyDescriptionPoco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Company"] = new SelectList(_context.CompanyProfiles, "Id", "CompanyWebsite", companyDescriptionPoco.Company);
            ViewData["LanguageId"] = new SelectList(_context.SystemLanguageCodes, "LanguageID", "LanguageID", companyDescriptionPoco.LanguageId);
            return View(companyDescriptionPoco);
        }

        // GET: CompanyDescription/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyDescriptionPoco = await _context.CompanyDescriptions.FindAsync(id);
            if (companyDescriptionPoco == null)
            {
                return NotFound();
            }
            ViewData["Company"] = new SelectList(_context.CompanyProfiles, "Id", "CompanyWebsite", companyDescriptionPoco.Company);
            ViewData["LanguageId"] = new SelectList(_context.SystemLanguageCodes, "LanguageID", "LanguageID", companyDescriptionPoco.LanguageId);
            return View(companyDescriptionPoco);
        }

        // POST: CompanyDescription/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Company,LanguageId,CompanyName,CompanyDescription")] CompanyDescriptionPoco companyDescriptionPoco)
        {
            if (id != companyDescriptionPoco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyDescriptionPoco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyDescriptionPocoExists(companyDescriptionPoco.Id))
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
            ViewData["Company"] = new SelectList(_context.CompanyProfiles, "Id", "CompanyWebsite", companyDescriptionPoco.Company);
            ViewData["LanguageId"] = new SelectList(_context.SystemLanguageCodes, "LanguageID", "LanguageID", companyDescriptionPoco.LanguageId);
            return View(companyDescriptionPoco);
        }

        // GET: CompanyDescription/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyDescriptionPoco = await _context.CompanyDescriptions
                .Include(c => c.CompanyProfile)
                .Include(c => c.SystemLanguageCode)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyDescriptionPoco == null)
            {
                return NotFound();
            }

            return View(companyDescriptionPoco);
        }

        // POST: CompanyDescription/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var companyDescriptionPoco = await _context.CompanyDescriptions.FindAsync(id);
            _context.CompanyDescriptions.Remove(companyDescriptionPoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyDescriptionPocoExists(Guid id)
        {
            return _context.CompanyDescriptions.Any(e => e.Id == id);
        }
    }
}
