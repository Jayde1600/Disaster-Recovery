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
    public class ActiveDisastersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActiveDisastersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ActiveDisasters
        public async Task<IActionResult> Index()
        {
            var currentDate = DateTime.UtcNow;

            var activeDisasters = await _context.DisasterCheck
                .Where(disaster => disaster.EndDate > currentDate) // Filter out expired disasters
                .Select(disaster => new ActiveDisasters
                {
                    DisasterCheckId = disaster.Id,
                    Disaster = disaster.Description,
                    Location = disaster.Location,
                    EndDate = disaster.EndDate,
                    AllocatedAmount = _context.AllocateFunds
                        .Where(fund => fund.DisasterId == disaster.Id)
                        .Sum(fund => fund.Amount),
                    AllocatedQuantity = _context.AllocateGoods
                        .Where(goods => goods.DisasterId == disaster.Id)
                        .Sum(goods => goods.Quantity),
                    GoodsCategory = _context.AllocateGoods
                        .Where(goods => goods.DisasterId == disaster.Id)
                        .Select(goods => goods.Category.CategoryName)
                        .FirstOrDefault() // Assuming Category has a property CategoryName
                })
                .ToListAsync();

            return View(activeDisasters);
        }
        // GET: ActiveDisasters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ActiveDisasters == null)
            {
                return NotFound();
            }

            var activeDisasters = await _context.ActiveDisasters
                .FirstOrDefaultAsync(m => m.DisasterCheckId == id);
            if (activeDisasters == null)
            {
                return NotFound();
            }

            return View(activeDisasters);
        }

        // GET: ActiveDisasters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ActiveDisasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DisasterCheckId,Disaster,Location,EndDate,AllocatedAmount,AllocatedQuantity,GoodsCategory")] ActiveDisasters activeDisasters)
        {
            if (ModelState.IsValid)
            {
                _context.Add(activeDisasters);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(activeDisasters);
        }

        // GET: ActiveDisasters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ActiveDisasters == null)
            {
                return NotFound();
            }

            var activeDisasters = await _context.ActiveDisasters.FindAsync(id);
            if (activeDisasters == null)
            {
                return NotFound();
            }
            return View(activeDisasters);
        }

        // POST: ActiveDisasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DisasterCheckId,Disaster,Location,EndDate,AllocatedAmount,AllocatedQuantity,GoodsCategory")] ActiveDisasters activeDisasters)
        {
            if (id != activeDisasters.DisasterCheckId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activeDisasters);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActiveDisastersExists(activeDisasters.DisasterCheckId))
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
            return View(activeDisasters);
        }

        // GET: ActiveDisasters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ActiveDisasters == null)
            {
                return NotFound();
            }

            var activeDisasters = await _context.ActiveDisasters
                .FirstOrDefaultAsync(m => m.DisasterCheckId == id);
            if (activeDisasters == null)
            {
                return NotFound();
            }

            return View(activeDisasters);
        }

        // POST: ActiveDisasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ActiveDisasters == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ActiveDisasters'  is null.");
            }
            var activeDisasters = await _context.ActiveDisasters.FindAsync(id);
            if (activeDisasters != null)
            {
                _context.ActiveDisasters.Remove(activeDisasters);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActiveDisastersExists(int id)
        {
          return (_context.ActiveDisasters?.Any(e => e.DisasterCheckId == id)).GetValueOrDefault();
        }
    }
}
