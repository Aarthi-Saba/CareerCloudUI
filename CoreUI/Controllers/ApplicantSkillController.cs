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
    public class ApplicantSkillController : Controller
    {
        private readonly CareerCloudContext _context;

        public ApplicantSkillController(CareerCloudContext context)
        {
            _context = context;
        }

        // GET: ApplicantSkill
        public async Task<IActionResult> Index()
        {
            var careerCloudContext = _context.ApplicantSkills.Include(a => a.ApplicantProfile);
            return View(await careerCloudContext.ToListAsync());
        }

        // GET: ApplicantSkill/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantSkillPoco = await _context.ApplicantSkills
                .Include(a => a.ApplicantProfile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantSkillPoco == null)
            {
                return NotFound();
            }

            return View(applicantSkillPoco);
        }

        // GET: ApplicantSkill/Create
        public IActionResult Create()
        {
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "City");
            return View();
        }

        // POST: ApplicantSkill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Applicant,Skill,SkillLevel,StartMonth,StartYear,EndMonth,EndYear")] ApplicantSkillPoco applicantSkillPoco)
        {
            if (ModelState.IsValid)
            {
                applicantSkillPoco.Id = Guid.NewGuid();
                _context.Add(applicantSkillPoco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "City", applicantSkillPoco.Applicant);
            return View(applicantSkillPoco);
        }

        // GET: ApplicantSkill/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantSkillPoco = await _context.ApplicantSkills.FindAsync(id);
            if (applicantSkillPoco == null)
            {
                return NotFound();
            }
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "City", applicantSkillPoco.Applicant);
            return View(applicantSkillPoco);
        }

        // POST: ApplicantSkill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Applicant,Skill,SkillLevel,StartMonth,StartYear,EndMonth,EndYear")] ApplicantSkillPoco applicantSkillPoco)
        {
            if (id != applicantSkillPoco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantSkillPoco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantSkillPocoExists(applicantSkillPoco.Id))
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
            ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "City", applicantSkillPoco.Applicant);
            return View(applicantSkillPoco);
        }

        // GET: ApplicantSkill/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantSkillPoco = await _context.ApplicantSkills
                .Include(a => a.ApplicantProfile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantSkillPoco == null)
            {
                return NotFound();
            }

            return View(applicantSkillPoco);
        }

        // POST: ApplicantSkill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var applicantSkillPoco = await _context.ApplicantSkills.FindAsync(id);
            _context.ApplicantSkills.Remove(applicantSkillPoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantSkillPocoExists(Guid id)
        {
            return _context.ApplicantSkills.Any(e => e.Id == id);
        }
    }
}
