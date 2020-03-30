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
    public class CompanyJobDescription : Controller
    {
        private readonly CareerCloudContext _context;

        public CompanyJobDescription(CareerCloudContext context)
        {
            _context = context;
        }

        // GET: CompanyJobDescription
        public async Task<IActionResult> Index()
        {
            var careerCloudContext = _context.CompanyJobDescriptions.Include(c => c.CompanyJob);
            return View(await careerCloudContext.ToListAsync());
        }

        // GET: CompanyJobDescription/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var companyJobDescriptionPoco = await _context.CompanyJobDescriptions
                .Include(c => c.CompanyJob)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyJobDescriptionPoco == null)
            {
                return NotFound();
            }
            return View(companyJobDescriptionPoco);
        }

        // GET: CompanyJobDescription/Create
        //[Route("id")]
        [HttpGet]
        public IActionResult Create(Guid? id)
        {
            //ViewData["Job"] = new SelectList(_context.CompanyJobs, "Id", "Id");
            TempData["Company"] = id;
            return View();
        }

        // POST: CompanyJobDescription/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyJobDetail companyJobDetails)
        {
            if (ModelState.IsValid)
            {

                var config1 = new MapperConfiguration(cfg => cfg.CreateMap<CompanyJob, CompanyJobPoco>());
                var config2 = new MapperConfiguration(cfg => cfg.CreateMap<CompanyJobDescriptionModel, CompanyJobDescriptionPoco>());
                var mapper1 = config1.CreateMapper();
                CompanyJobPoco companyJobPoco = mapper1.Map<CompanyJob,CompanyJobPoco>(companyJobDetails.companyJob);
                companyJobPoco.Id = Guid.NewGuid();
                companyJobPoco.Company = Guid.Parse(TempData["Company"].ToString());
                var mapper2 = config2.CreateMapper();
                CompanyJobDescriptionPoco companyJobDescriptionPoco =
                            mapper2.Map<CompanyJobDescriptionModel,CompanyJobDescriptionPoco>(companyJobDetails.companyJobDescription);
                companyJobDescriptionPoco.Id = Guid.NewGuid();
                companyJobDescriptionPoco.Job = companyJobPoco.Id;
                _context.Add(companyJobPoco);
                _context.Add(companyJobDescriptionPoco);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create","CompanyJobEducation",new { id = companyJobPoco.Id });
            }
            //ViewData["Job"] = new SelectList(_context.CompanyJobs, "Id", "Id", companyJobDescriptionPoco.Job);
            return View(nameof(Details));
        }

        // GET: CompanyJobDescription/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobDescriptionPoco = await _context.CompanyJobDescriptions.FindAsync(id);
            if (companyJobDescriptionPoco == null)
            {
                return NotFound();
            }
            ViewData["Job"] = new SelectList(_context.CompanyJobs, "Id", "Id", companyJobDescriptionPoco.Job);
            return View(companyJobDescriptionPoco);
        }

        // POST: CompanyJobDescription/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Job,JobName,JobDescriptions")] CompanyJobDescriptionPoco companyJobDescriptionPoco)
        {
            if (id != companyJobDescriptionPoco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyJobDescriptionPoco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyJobDescriptionPocoExists(companyJobDescriptionPoco.Id))
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
            ViewData["Job"] = new SelectList(_context.CompanyJobs, "Id", "Id", companyJobDescriptionPoco.Job);
            return View(companyJobDescriptionPoco);
        }

        // GET: CompanyJobDescription/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyJobDescriptionPoco = await _context.CompanyJobDescriptions
                .Include(c => c.CompanyJob)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyJobDescriptionPoco == null)
            {
                return NotFound();
            }

            return View(companyJobDescriptionPoco);
        }

        // POST: CompanyJobDescription/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var companyJobDescriptionPoco = await _context.CompanyJobDescriptions.FindAsync(id);
            _context.CompanyJobDescriptions.Remove(companyJobDescriptionPoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyJobDescriptionPocoExists(Guid id)
        {
            return _context.CompanyJobDescriptions.Any(e => e.Id == id);
        }
    }
}
