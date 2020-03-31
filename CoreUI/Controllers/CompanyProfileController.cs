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
    public class CompanyProfileController : Controller
    {
        private readonly CareerCloudContext _context;

        public CompanyProfileController(CareerCloudContext context)
        {
            _context = context;
        }

        // GET: CompanyProfile
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "date_desc_order" : "";
            var company = _context.CompanyProfiles.Include(c => c.CompanyDescription).AsQueryable();
            switch (sortOrder)
            {
                case "date_desc_order":
                    company = company.OrderByDescending(s => s.RegistrationDate);
                    break;
                default:
                    company = company.OrderBy(s => s.RegistrationDate);
                    break;
            }
            return View(await company.AsNoTracking().ToListAsync());
        }

        // GET: CompanyProfile/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var companyProfilePoco = await _context.CompanyProfiles
                .Include(c => c.CompanyLocation)
                .Include(c => c.CompanyJob)
                .Include(c => c.CompanyDescription)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyProfilePoco == null)
            {
                return NotFound();
            }

            return View(companyProfilePoco);
        }

        // GET: CompanyProfile/Create
        public IActionResult Create()
        {
            ViewData["Language"]=  new SelectList(_context.SystemLanguageCodes, "Name", "Name");
            return View();
        }

        // POST: CompanyProfile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyDetails companyDetails)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<CompanyProfile, CompanyProfilePoco>();
                                                cfg.CreateMap<CompanyDescriptionModel, CompanyDescriptionPoco>();});
                var mapper = config.CreateMapper();
                CompanyProfilePoco companyProfilePoco = mapper.Map<CompanyProfilePoco>(companyDetails.companyProfile);
                companyProfilePoco.Id = Guid.NewGuid();

                CompanyDescriptionPoco companyDescriptionPoco =
                    mapper.Map<CompanyDescriptionPoco>(companyDetails.companyDescription);

                companyDescriptionPoco.Id = Guid.NewGuid();
                companyDescriptionPoco.Company = companyProfilePoco.Id;
                var lang = _context.SystemLanguageCodes.Where(l => l.Name == companyDetails.companyDescription.LanguageId).FirstOrDefault();
                companyDescriptionPoco.LanguageId = lang.LanguageID;
                _context.Add(companyProfilePoco);
                _context.Add(companyDescriptionPoco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details),new { id = companyProfilePoco.Id });
            }
            return View(companyDetails);
        }

        // GET: CompanyProfile/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyProfilePoco = await _context.CompanyProfiles.FindAsync(id);
            if (companyProfilePoco == null)
            {
                return NotFound();
            }
            return View(companyProfilePoco);
        }

        // POST: CompanyProfile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,RegistrationDate,CompanyWebsite,ContactPhone,ContactName,CompanyLogo")] CompanyProfilePoco companyProfilePoco)
        {
            if (id != companyProfilePoco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyProfilePoco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyProfilePocoExists(companyProfilePoco.Id))
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
            return View(companyProfilePoco);
        }

        // GET: CompanyProfile/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyProfilePoco = await _context.CompanyProfiles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyProfilePoco == null)
            {
                return NotFound();
            }

            return View(companyProfilePoco);
        }

        // POST: CompanyProfile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var companyProfilePoco = await _context.CompanyProfiles.FindAsync(id);
            _context.CompanyProfiles.Remove(companyProfilePoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyProfilePocoExists(Guid id)
        {
            return _context.CompanyProfiles.Any(e => e.Id == id);
        }
        public async Task<IActionResult> Jobs(Guid id)
        {
            var companyProfilePoco = await _context.CompanyProfiles
                .Include(c => c.CompanyJob)
                .FirstOrDefaultAsync(m => m.Id == id);
            return View(companyProfilePoco);
            
        }
    }
}
