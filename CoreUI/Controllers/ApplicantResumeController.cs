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
    public class ApplicantResumeController : Controller
    {
        private readonly CareerCloudContext _context;

        public ApplicantResumeController(CareerCloudContext context)
        {
            _context = context;
        }

        // GET: ApplicantResume
        public async Task<IActionResult> Index()
        {
            var careerCloudContext = _context.ApplicantResumes.Include(a => a.ApplicantProfile);
            return View(await careerCloudContext.ToListAsync());
        }

        // GET: ApplicantResume/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantResumePoco = await _context.ApplicantResumes
                .Include(a => a.ApplicantProfile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantResumePoco == null)
            {
                return NotFound();
            }

            return View(applicantResumePoco);
        }

        // GET: ApplicantResume/Create
        public IActionResult Create()
        {
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "City");
            return View();
        }

        // POST: ApplicantResume/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Applicant,Resume,LastUpdated")] ApplicantResumePoco applicantResumePoco)
        {
            if (ModelState.IsValid)
            {
                applicantResumePoco.Id = Guid.NewGuid();
                _context.Add(applicantResumePoco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "City", applicantResumePoco.Applicant);
            return View(applicantResumePoco);
        }

        // GET: ApplicantResume/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantResumePoco = await _context.ApplicantResumes.FindAsync(id);
            if (applicantResumePoco == null)
            {
                return NotFound();
            }
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "City", applicantResumePoco.Applicant);
            return View(applicantResumePoco);
        }

        // POST: ApplicantResume/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Applicant,Resume,LastUpdated")] ApplicantResumePoco applicantResumePoco)
        {
            if (id != applicantResumePoco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantResumePoco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantResumePocoExists(applicantResumePoco.Id))
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
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "City", applicantResumePoco.Applicant);
            return View(applicantResumePoco);
        }

        // GET: ApplicantResume/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantResumePoco = await _context.ApplicantResumes
                .Include(a => a.ApplicantProfile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantResumePoco == null)
            {
                return NotFound();
            }

            return View(applicantResumePoco);
        }

        // POST: ApplicantResume/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var applicantResumePoco = await _context.ApplicantResumes.FindAsync(id);
            _context.ApplicantResumes.Remove(applicantResumePoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantResumePocoExists(Guid id)
        {
            return _context.ApplicantResumes.Any(e => e.Id == id);
        }
    }
}
