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
    public class AvailableMoneysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AvailableMoneysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AvailableMoneys
        public async Task<IActionResult> Index()
        {
              return _context.AvailableMoney != null ? 
                          View(await _context.AvailableMoney.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.AvailableMoney'  is null.");
        }

        // GET: AvailableMoneys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AvailableMoney == null)
            {
                return NotFound();
            }

            var availableMoney = await _context.AvailableMoney
                .FirstOrDefaultAsync(m => m.Id == id);
            if (availableMoney == null)
            {
                return NotFound();
            }

            return View(availableMoney);
        }

        // GET: AvailableMoneys/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AvailableMoneys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TotalAmount,AmountUsed")] AvailableMoney availableMoney)
        {
            if (ModelState.IsValid)
            {
                availableMoney.TotalAmount -= availableMoney.AmountUsed; // Subtract used amount from total amount
                _context.Add(availableMoney);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(availableMoney);
        }

        // GET: AvailableMoneys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AvailableMoney == null)
            {
                return NotFound();
            }

            var availableMoney = await _context.AvailableMoney.FindAsync(id);
            if (availableMoney == null)
            {
                return NotFound();
            }
            return View(availableMoney);
        }

        // POST: AvailableMoneys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TotalAmount,AmountUsed")] AvailableMoney availableMoney)
        {
            {
                if (id != availableMoney.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        availableMoney.TotalAmount -= availableMoney.AmountUsed; // Subtract used amount from total amount
                        _context.Update(availableMoney);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AvailableMoneyExists(availableMoney.Id))
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
                return View(availableMoney);
            }
        }

            // GET: AvailableMoneys/Delete/5
            public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AvailableMoney == null)
            {
                return NotFound();
            }

            var availableMoney = await _context.AvailableMoney
                .FirstOrDefaultAsync(m => m.Id == id);
            if (availableMoney == null)
            {
                return NotFound();
            }

            return View(availableMoney);
        }

        // POST: AvailableMoneys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AvailableMoney == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AvailableMoney'  is null.");
            }
            var availableMoney = await _context.AvailableMoney.FindAsync(id);
            if (availableMoney != null)
            {
                _context.AvailableMoney.Remove(availableMoney);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvailableMoneyExists(int id)
        {
          return (_context.AvailableMoney?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
