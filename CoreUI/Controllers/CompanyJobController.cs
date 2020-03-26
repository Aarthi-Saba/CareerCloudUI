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
    public class CompanyJobController : Controller
    {
        private readonly CareerCloudContext _context;

        public CompanyJobController(CareerCloudContext context)
        {
            _context = context;
        }

        // GET: CompanyJob
        public async Task<IActionResult> Index()
        {
            var careerCloudContext = _context.CompanyJobs.Include(c => c.CompanyProfile);
            return View(await careerCloudContext.ToListAsync());
        }

        // GET: CompanyJob/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobPoco = await _context.CompanyJobs
                .Include(c => c.CompanyProfile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyJobPoco == null)
            {
                return NotFound();
            }

            return View(companyJobPoco);
        }

        // GET: CompanyJob/Create
        public IActionResult Create()
        {
            ViewData["Company"] = new SelectList(_context.CompanyProfiles, "Id", "CompanyWebsite");
            return View();
        }

        // POST: CompanyJob/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Company,ProfileCreated,IsInactive,IsCompanyHidden")] CompanyJobPoco companyJobPoco)
        {
            if (ModelState.IsValid)
            {
                companyJobPoco.Id = Guid.NewGuid();
                _context.Add(companyJobPoco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Company"] = new SelectList(_context.CompanyProfiles, "Id", "CompanyWebsite", companyJobPoco.Company);
            return View(companyJobPoco);
        }

        // GET: CompanyJob/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobPoco = await _context.CompanyJobs.FindAsync(id);
            if (companyJobPoco == null)
            {
                return NotFound();
            }
            ViewData["Company"] = new SelectList(_context.CompanyProfiles, "Id", "CompanyWebsite", companyJobPoco.Company);
            return View(companyJobPoco);
        }

        // POST: CompanyJob/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Company,ProfileCreated,IsInactive,IsCompanyHidden")] CompanyJobPoco companyJobPoco)
        {
            if (id != companyJobPoco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyJobPoco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyJobPocoExists(companyJobPoco.Id))
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
            ViewData["Company"] = new SelectList(_context.CompanyProfiles, "Id", "CompanyWebsite", companyJobPoco.Company);
            return View(companyJobPoco);
        }

        // GET: CompanyJob/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobPoco = await _context.CompanyJobs
                .Include(c => c.CompanyProfile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyJobPoco == null)
            {
                return NotFound();
            }

            return View(companyJobPoco);
        }

        // POST: CompanyJob/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var companyJobPoco = await _context.CompanyJobs.FindAsync(id);
            _context.CompanyJobs.Remove(companyJobPoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyJobPocoExists(Guid id)
        {
            return _context.CompanyJobs.Any(e => e.Id == id);
        }
    }
}
