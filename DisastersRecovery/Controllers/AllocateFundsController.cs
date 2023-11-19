using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DisastersRecovery.Data;
using DisastersRecovery.Models;

namespace DisastersRecovery.Controllers
{
    public class AllocateFundsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AllocateFundsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AllocateFunds
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AllocateFunds.Include(a => a.Disaster);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AllocateFunds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AllocateFunds == null)
            {
                return NotFound();
            }

            var allocateFunds = await _context.AllocateFunds
                .Include(a => a.Disaster)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (allocateFunds == null)
            {
                return NotFound();
            }

            return View(allocateFunds);
        }

        // GET: AllocateFunds/Create
        public IActionResult Create()
        {
            ViewData["DisasterId"] = new SelectList(_context.DisasterCheck, "Id", "AidType");
            return View();
        }

        // POST: AllocateFunds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,DisasterId,AllocationDate")] AllocateFunds allocateFunds)
        {
            if (ModelState.IsValid)
            {
                _context.Add(allocateFunds);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DisasterId"] = new SelectList(_context.DisasterCheck, "Id", "AidType", allocateFunds.DisasterId);
            return View(allocateFunds);
        }

        // GET: AllocateFunds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AllocateFunds == null)
            {
                return NotFound();
            }

            var allocateFunds = await _context.AllocateFunds.FindAsync(id);
            if (allocateFunds == null)
            {
                return NotFound();
            }
            ViewData["DisasterId"] = new SelectList(_context.DisasterCheck, "Id", "AidType", allocateFunds.DisasterId);
            return View(allocateFunds);
        }

        // POST: AllocateFunds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,DisasterId,AllocationDate")] AllocateFunds allocateFunds)
        {
            if (id != allocateFunds.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(allocateFunds);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AllocateFundsExists(allocateFunds.Id))
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
            ViewData["DisasterId"] = new SelectList(_context.DisasterCheck, "Id", "AidType", allocateFunds.DisasterId);
            return View(allocateFunds);
        }

        // GET: AllocateFunds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AllocateFunds == null)
            {
                return NotFound();
            }

            var allocateFunds = await _context.AllocateFunds
                .Include(a => a.Disaster)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (allocateFunds == null)
            {
                return NotFound();
            }

            return View(allocateFunds);
        }

        // POST: AllocateFunds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AllocateFunds == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AllocateFunds'  is null.");
            }
            var allocateFunds = await _context.AllocateFunds.FindAsync(id);
            if (allocateFunds != null)
            {
                _context.AllocateFunds.Remove(allocateFunds);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AllocateFundsExists(int id)
        {
          return (_context.AllocateFunds?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
