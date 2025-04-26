using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SMS.Models;

namespace SMS.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ManagementSystemContext _context;

        public RegistrationController(ManagementSystemContext context)
        {
            _context = context;
        }

        // GET: Registration
        public async Task<IActionResult> Index()
        {
            var managementSystemContext = _context.Registrations.Include(r => r.CcodeNavigation).Include(r => r.SidNavigation).Include(r => r.TidNavigation);
            return View(await managementSystemContext.ToListAsync());
        }

        // GET: Registration/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .Include(r => r.CcodeNavigation)
                .Include(r => r.SidNavigation)
                .Include(r => r.TidNavigation)
                .FirstOrDefaultAsync(m => m.Rid == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // GET: Registration/Create
        public IActionResult Create()
        {
            ViewData["Ccode"] = new SelectList(_context.Courses, "CCode", "CCode");
            ViewData["Sid"] = new SelectList(_context.Students, "Sid", "Sid");
            ViewData["Tid"] = new SelectList(_context.Teachers, "Tid", "Tid");
            return View();
        }

        // POST: Registration/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Rid,Tid,Ccode,Sid")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Ccode"] = new SelectList(_context.Courses, "CCode", "CCode", registration.Ccode);
            ViewData["Sid"] = new SelectList(_context.Students, "Sid", "Sid", registration.Sid);
            ViewData["Tid"] = new SelectList(_context.Teachers, "Tid", "Tid", registration.Tid);
            return View(registration);
        }

        // GET: Registration/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }
            ViewData["Ccode"] = new SelectList(_context.Courses, "CCode", "CCode", registration.Ccode);
            ViewData["Sid"] = new SelectList(_context.Students, "Sid", "Sid", registration.Sid);
            ViewData["Tid"] = new SelectList(_context.Teachers, "Tid", "Tid", registration.Tid);
            return View(registration);
        }

        // POST: Registration/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Rid,Tid,Ccode,Sid")] Registration registration)
        {
            if (id != registration.Rid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrationExists(registration.Rid))
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
            ViewData["Ccode"] = new SelectList(_context.Courses, "CCode", "CCode", registration.Ccode);
            ViewData["Sid"] = new SelectList(_context.Students, "Sid", "Sid", registration.Sid);
            ViewData["Tid"] = new SelectList(_context.Teachers, "Tid", "Tid", registration.Tid);
            return View(registration);
        }

        // GET: Registration/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .Include(r => r.CcodeNavigation)
                .Include(r => r.SidNavigation)
                .Include(r => r.TidNavigation)
                .FirstOrDefaultAsync(m => m.Rid == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // POST: Registration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            if (registration != null)
            {
                _context.Registrations.Remove(registration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistrationExists(int id)
        {
            return _context.Registrations.Any(e => e.Rid == id);
        }
    }
}
