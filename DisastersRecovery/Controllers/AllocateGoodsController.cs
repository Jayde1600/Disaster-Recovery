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
    public class AllocateGoodsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AllocateGoodsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AllocateGoods
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AllocateGoods.Include(a => a.Category).Include(a => a.Disaster);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AllocateGoods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AllocateGoods == null)
            {
                return NotFound();
            }

            var allocateGoods = await _context.AllocateGoods
                .Include(a => a.Category)
                .Include(a => a.Disaster)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (allocateGoods == null)
            {
                return NotFound();
            }

            return View(allocateGoods);
        }

        // GET: AllocateGoods/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");
            ViewData["DisasterId"] = new SelectList(_context.DisasterCheck, "Id", "Description");
            return View();
        }

        // POST: AllocateGoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quantity,DisasterId,CategoryId,AllocationDate")] AllocateGoods allocateGoods)
        {
            if (ModelState.IsValid)
            {
                allocateGoods.AllocationDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.Local);

                var availableGoods = await _context.AvailableGoods.FirstOrDefaultAsync(a => a.CategoryId == allocateGoods.CategoryId);

                // Check if the category exists in AvailableGoods and if the available quantity is enough for allocation
                if (availableGoods != null && availableGoods.AvailableQuantity >= allocateGoods.Quantity)
                {
                    availableGoods.QuantityUsed += allocateGoods.Quantity;
                    availableGoods.AvailableQuantity -= allocateGoods.Quantity;
                    await _context.SaveChangesAsync();

                    _context.Add(allocateGoods);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Quantity", "The entered quantity exceeds the available amount or the category hasn't received donations yet.");
                }
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", allocateGoods.CategoryId);
            ViewData["DisasterId"] = new SelectList(_context.DisasterCheck, "Id", "Description", allocateGoods.DisasterId);
            return View(allocateGoods);
        }

        // GET: AllocateGoods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AllocateGoods == null)
            {
                return NotFound();
            }

            var allocateGoods = await _context.AllocateGoods.FindAsync(id);
            if (allocateGoods == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", allocateGoods.CategoryId);
            ViewData["DisasterId"] = new SelectList(_context.DisasterCheck, "Id", "Description", allocateGoods.DisasterId);
            return View(allocateGoods);
        }

        // POST: AllocateGoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Quantity,DisasterId,CategoryId,AllocationDate")] AllocateGoods allocateGoods)
        {
            if (id != allocateGoods.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingAllocation = await _context.AllocateGoods.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
                    var quantityDifference = allocateGoods.Quantity - existingAllocation.Quantity;

                    _context.Update(allocateGoods);
                    await _context.SaveChangesAsync();

                    // Update QuantityUsed and AvailableQuantity in AvailableGoods
                    var availableGoods = await _context.AvailableGoods.FirstOrDefaultAsync(a => a.CategoryId == allocateGoods.CategoryId);
                    if (availableGoods != null)
                    {
                        availableGoods.QuantityUsed += quantityDifference;
                        availableGoods.AvailableQuantity -= quantityDifference;
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        // Category doesn't exist in AvailableGoods, create a new entry
                        var newAvailableGoodsEntry = new AvailableGoods
                        {
                            CategoryId = allocateGoods.CategoryId,
                            AvailableQuantity = allocateGoods.Quantity, // Assuming the entire allocation becomes the AvailableQuantity initially
                            QuantityUsed = 0 // Assuming initially no quantity has been used
                                             // Other properties or relationships as needed
                        };

                        _context.Add(newAvailableGoodsEntry);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AllocateGoodsExists(allocateGoods.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", allocateGoods.CategoryId);
            ViewData["DisasterId"] = new SelectList(_context.DisasterCheck, "Id", "Description", allocateGoods.DisasterId);
            return View(allocateGoods);
        }

        // GET: AllocateGoods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AllocateGoods == null)
            {
                return NotFound();
            }

            var allocateGoods = await _context.AllocateGoods
                .Include(a => a.Category)
                .Include(a => a.Disaster)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (allocateGoods == null)
            {
                return NotFound();
            }

            return View(allocateGoods);
        }

        // POST: AllocateGoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AllocateGoods == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AllocateGoods'  is null.");
            }
            var allocateGoods = await _context.AllocateGoods.FindAsync(id);
            if (allocateGoods != null)
            {
                _context.AllocateGoods.Remove(allocateGoods);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AllocateGoodsExists(int id)
        {
          return (_context.AllocateGoods?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

