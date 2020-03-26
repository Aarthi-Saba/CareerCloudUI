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
    public class CompanyJobEducationController : Controller
    {
        private readonly CareerCloudContext _context;

        public CompanyJobEducationController(CareerCloudContext context)
        {
            _context = context;
        }

        // GET: CompanyJobEducation
        public async Task<IActionResult> Index()
        {
            var careerCloudContext = _context.CompanyJobEducations.Include(c => c.CompanyJob);
            return View(await careerCloudContext.ToListAsync());
        }

        // GET: CompanyJobEducation/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobEducationPoco = await _context.CompanyJobEducations
                .Include(c => c.CompanyJob)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyJobEducationPoco == null)
            {
                return NotFound();
            }

            return View(companyJobEducationPoco);
        }

        // GET: CompanyJobEducation/Create
        public IActionResult Create()
        {
            ViewData["Job"] = new SelectList(_context.CompanyJobs, "Id", "Id");
            return View();
        }

        // POST: CompanyJobEducation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Job,Major,Importance")] CompanyJobEducationPoco companyJobEducationPoco)
        {
            if (ModelState.IsValid)
            {
                companyJobEducationPoco.Id = Guid.NewGuid();
                _context.Add(companyJobEducationPoco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Job"] = new SelectList(_context.CompanyJobs, "Id", "Id", companyJobEducationPoco.Job);
            return View(companyJobEducationPoco);
        }

        // GET: CompanyJobEducation/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobEducationPoco = await _context.CompanyJobEducations.FindAsync(id);
            if (companyJobEducationPoco == null)
            {
                return NotFound();
            }
            ViewData["Job"] = new SelectList(_context.CompanyJobs, "Id", "Id", companyJobEducationPoco.Job);
            return View(companyJobEducationPoco);
        }

        // POST: CompanyJobEducation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Job,Major,Importance")] CompanyJobEducationPoco companyJobEducationPoco)
        {
            if (id != companyJobEducationPoco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyJobEducationPoco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyJobEducationPocoExists(companyJobEducationPoco.Id))
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
            ViewData["Job"] = new SelectList(_context.CompanyJobs, "Id", "Id", companyJobEducationPoco.Job);
            return View(companyJobEducationPoco);
        }

        // GET: CompanyJobEducation/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobEducationPoco = await _context.CompanyJobEducations
                .Include(c => c.CompanyJob)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyJobEducationPoco == null)
            {
                return NotFound();
            }

            return View(companyJobEducationPoco);
        }

        // POST: CompanyJobEducation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var companyJobEducationPoco = await _context.CompanyJobEducations.FindAsync(id);
            _context.CompanyJobEducations.Remove(companyJobEducationPoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyJobEducationPocoExists(Guid id)
        {
            return _context.CompanyJobEducations.Any(e => e.Id == id);
        }
    }
}
