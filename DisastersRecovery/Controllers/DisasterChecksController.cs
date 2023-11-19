using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DisastersRecovery.Data;
using DisastersRecovery.Models;
using Microsoft.AspNetCore.Authorization;

namespace DisastersRecovery.Controllers
{
    [Authorize]
    public class DisasterChecksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DisasterChecksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DisasterChecks
        public async Task<IActionResult> Index()
        {
              return _context.DisasterCheck != null ? 
                          View(await _context.DisasterCheck.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.DisasterCheck'  is null.");
        }

        // GET: DisasterChecks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DisasterCheck == null)
            {
                return NotFound();
            }

            var disasterCheck = await _context.DisasterCheck
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disasterCheck == null)
            {
                return NotFound();
            }

            return View(disasterCheck);
        }

        // GET: DisasterChecks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DisasterChecks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartDate,EndDate,Location,Description,AidType")] DisasterCheck disasterCheck)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disasterCheck);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(disasterCheck);
        }

        // GET: DisasterChecks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DisasterCheck == null)
            {
                return NotFound();
            }

            var disasterCheck = await _context.DisasterCheck.FindAsync(id);
            if (disasterCheck == null)
            {
                return NotFound();
            }
            return View(disasterCheck);
        }

        // POST: DisasterChecks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartDate,EndDate,Location,Description,AidType")] DisasterCheck disasterCheck)
        {
            if (id != disasterCheck.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disasterCheck);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisasterCheckExists(disasterCheck.Id))
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
            return View(disasterCheck);
        }

        // GET: DisasterChecks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DisasterCheck == null)
            {
                return NotFound();
            }

            var disasterCheck = await _context.DisasterCheck
                .FirstOrDefaultAsync(m => m.Id == id);
            if (disasterCheck == null)
            {
                return NotFound();
            }

            return View(disasterCheck);
        }

        // POST: DisasterChecks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DisasterCheck == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DisasterCheck'  is null.");
            }
            var disasterCheck = await _context.DisasterCheck.FindAsync(id);
            if (disasterCheck != null)
            {
                _context.DisasterCheck.Remove(disasterCheck);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisasterCheckExists(int id)
        {
          return (_context.DisasterCheck?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
