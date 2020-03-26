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
    public class ApplicantWorkHistoryController : Controller
    {
        private readonly CareerCloudContext _context;

        public ApplicantWorkHistoryController(CareerCloudContext context)
        {
            _context = context;
        }

        // GET: ApplicantWorkHistory
        public async Task<IActionResult> Index()
        {
            var careerCloudContext = _context.ApplicantWorkHistories.Include(a => a.ApplicantProfile).Include(a => a.SystemCountryCode);
            return View(await careerCloudContext.ToListAsync());
        }

        // GET: ApplicantWorkHistory/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantWorkHistoryPoco = await _context.ApplicantWorkHistories
                .Include(a => a.ApplicantProfile)
                .Include(a => a.SystemCountryCode)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantWorkHistoryPoco == null)
            {
                return NotFound();
            }

            return View(applicantWorkHistoryPoco);
        }

        // GET: ApplicantWorkHistory/Create
        public IActionResult Create()
        {
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "City");
            ViewData["CountryCode"] = new SelectList(_context.SystemCountryCodes, "Code", "Code");
            return View();
        }

        // POST: ApplicantWorkHistory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Applicant,CompanyName,CountryCode,Location,JobTitle,JobDescription,StartMonth,StartYear,EndMonth,EndYear")] ApplicantWorkHistoryPoco applicantWorkHistoryPoco)
        {
            if (ModelState.IsValid)
            {
                applicantWorkHistoryPoco.Id = Guid.NewGuid();
                _context.Add(applicantWorkHistoryPoco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "City", applicantWorkHistoryPoco.Applicant);
            ViewData["CountryCode"] = new SelectList(_context.SystemCountryCodes, "Code", "Code", applicantWorkHistoryPoco.CountryCode);
            return View(applicantWorkHistoryPoco);
        }

        // GET: ApplicantWorkHistory/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantWorkHistoryPoco = await _context.ApplicantWorkHistories.FindAsync(id);
            if (applicantWorkHistoryPoco == null)
            {
                return NotFound();
            }
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "City", applicantWorkHistoryPoco.Applicant);
            ViewData["CountryCode"] = new SelectList(_context.SystemCountryCodes, "Code", "Code", applicantWorkHistoryPoco.CountryCode);
            return View(applicantWorkHistoryPoco);
        }

        // POST: ApplicantWorkHistory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Applicant,CompanyName,CountryCode,Location,JobTitle,JobDescription,StartMonth,StartYear,EndMonth,EndYear")] ApplicantWorkHistoryPoco applicantWorkHistoryPoco)
        {
            if (id != applicantWorkHistoryPoco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantWorkHistoryPoco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantWorkHistoryPocoExists(applicantWorkHistoryPoco.Id))
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
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "City", applicantWorkHistoryPoco.Applicant);
            ViewData["CountryCode"] = new SelectList(_context.SystemCountryCodes, "Code", "Code", applicantWorkHistoryPoco.CountryCode);
            return View(applicantWorkHistoryPoco);
        }

        // GET: ApplicantWorkHistory/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantWorkHistoryPoco = await _context.ApplicantWorkHistories
                .Include(a => a.ApplicantProfile)
                .Include(a => a.SystemCountryCode)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantWorkHistoryPoco == null)
            {
                return NotFound();
            }

            return View(applicantWorkHistoryPoco);
        }

        // POST: ApplicantWorkHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var applicantWorkHistoryPoco = await _context.ApplicantWorkHistories.FindAsync(id);
            _context.ApplicantWorkHistories.Remove(applicantWorkHistoryPoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantWorkHistoryPocoExists(Guid id)
        {
            return _context.ApplicantWorkHistories.Any(e => e.Id == id);
        }
    }
}
