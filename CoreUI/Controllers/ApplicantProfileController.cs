using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using AutoMapper;
using CoreUI.Models;

namespace CoreUI.Controllers
{
    public class ApplicantProfileController : Controller
    {
        private readonly CareerCloudContext _context;

        public ApplicantProfileController(CareerCloudContext context)
        {
            _context = context;
        }

        // GET: ApplicantProfile
        public async Task<IActionResult> Index()
        {
            var careerCloudContext = _context.ApplicantProfiles.Include(a => a.SecurityLogin).Include(a => a.SystemCountry);
            return View(await careerCloudContext.ToListAsync());
        }

        // GET: ApplicantProfile/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var applicantProfilePoco = await _context.ApplicantProfiles
                .Include(a => a.ApplicantEducation)
                .Include(a => a.ApplicantJob)
                .Include(a => a.ApplicantResume)
                .Include(a => a.ApplicantWork)
                .Include(a => a.ApplicantSkill)
                .Include(a => a.SecurityLogin)
                .Include(a => a.SystemCountry)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantProfilePoco == null)
            {
                return NotFound();
            }
            return View(applicantProfilePoco);
        }
        [HttpPost]
        public ActionResult JobSearch(Guid? id,string searchString)
        {
            var companyJobPoco = _context.CompanyJobDescriptions
                                    .Where(j => j.JobName.Contains(searchString));
            var poco =  companyJobPoco.Include(a => a.CompanyJob)
                .ThenInclude(a => a.CompanyProfile)
                    .ThenInclude(a => a.CompanyDescription).ToList();
            
            if (poco == null)
            {
                return NotFound();
            }
            TempData.Keep("Applicant");
            return View(poco);
        }
        
        public async Task<IActionResult> JobSave(Guid id)
        {
            var applicantJobPoco = new ApplicantJobApplicationPoco();
            applicantJobPoco.Id = Guid.NewGuid();
            applicantJobPoco.Job = id;
            applicantJobPoco.Applicant = Guid.Parse(TempData["Applicant"].ToString());
            applicantJobPoco.ApplicationDate = DateTime.Today;
            _context.Add(applicantJobPoco);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details",new { id = applicantJobPoco.Applicant });
        }

        // GET: ApplicantProfile/Create
        public IActionResult Create()
        {
            TempData["securityFlag"]=false;
            return RedirectToAction("Create", "SecurityLogin");
        }
        //[Route("/id")]
        public IActionResult CreateProfile(Guid id)
        {
            ViewData["Currency"] = new SelectList(_context.ApplicantProfiles, "Currency", "Currency");
            ViewData["Country"] = new SelectList(_context.SystemCountryCodes, "Code", "Code");
            TempData.Keep("Login");
            return View("~/Views/ApplicantProfile/Create.cshtml");

        }

        // POST: ApplicantProfile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicantProfile applicantProfile)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<ApplicantProfile, ApplicantProfilePoco>());
                var mapper = config.CreateMapper();
                ApplicantProfilePoco poco = mapper.Map<ApplicantProfilePoco>(applicantProfile);
                poco.Id = Guid.NewGuid();
                var login = TempData["Login"];
                poco.Login = Guid.Parse(login.ToString());
                _context.Add(poco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details),new {id = poco.Id});
            }
            ViewData["Login"] = new SelectList(_context.SecurityLogins, "Id", "EmailAddress", applicantProfile.Login);
            ViewData["Country"] = new SelectList(_context.SystemCountryCodes, "Code", "Code", applicantProfile.Country);
            return View(applicantProfile);
        }

        // GET: ApplicantProfile/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantProfilePoco = await _context.ApplicantProfiles.FindAsync(id);
            if (applicantProfilePoco == null)
            {
                return NotFound();
            }
            ViewData["Login"] = new SelectList(_context.SecurityLogins, "Id", "EmailAddress", applicantProfilePoco.Login);
            ViewData["Country"] = new SelectList(_context.SystemCountryCodes, "Code", "Code", applicantProfilePoco.Country);
            return View(applicantProfilePoco);
        }

        // POST: ApplicantProfile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Login,CurrentSalary,CurrentRate,Currency,Country,Province,Street,City,PostalCode")] ApplicantProfilePoco applicantProfilePoco)
        {
            if (id != applicantProfilePoco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantProfilePoco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantProfilePocoExists(applicantProfilePoco.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details));
            }
            ViewData["Login"] = new SelectList(_context.SecurityLogins, "Id", "EmailAddress", applicantProfilePoco.Login);
            ViewData["Country"] = new SelectList(_context.SystemCountryCodes, "Code", "Code", applicantProfilePoco.Country);
            return View(applicantProfilePoco);
        }

        // GET: ApplicantProfile/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var applicantProfilePoco = await _context.ApplicantProfiles
                .Include(a => a.SecurityLogin)
                .Include(a => a.SystemCountry)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantProfilePoco == null)
            {
                return NotFound();
            }
            return View(applicantProfilePoco);
        }

        // POST: ApplicantProfile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var applicantProfilePoco = await _context.ApplicantProfiles.FindAsync(id);
            _context.ApplicantProfiles.Remove(applicantProfilePoco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantProfilePocoExists(Guid id)
        {
            return _context.ApplicantProfiles.Any(e => e.Id == id);
        }
    }
}
