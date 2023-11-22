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
    public class PurchaseGoodsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseGoodsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PurchaseGoods
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PurchaseGoods.Include(p => p.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PurchaseGoods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PurchaseGoods == null)
            {
                return NotFound();
            }

            var purchaseGoods = await _context.PurchaseGoods
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseGoods == null)
            {
                return NotFound();
            }

            return View(purchaseGoods);
        }

        // GET: PurchaseGoods/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: PurchaseGoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ItemName,Quantity,AmountUsed,CategoryId,PurchaseDate")] PurchaseGoods purchaseGoods)
        {
            if (ModelState.IsValid)
            {
                purchaseGoods.PurchaseDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.Local);

                // Fetch available money
                var availableMoney = await _context.AvailableMoney.FirstOrDefaultAsync();
                if (availableMoney != null)
                {
                    // Deduct amount used from available money
                    availableMoney.TotalAmount -= purchaseGoods.AmountUsed;

                    // Update amount spent
                    availableMoney.AmountUsed += purchaseGoods.AmountUsed;
                }

                // Fetch available goods related to the purchase
                var availableGoods = await _context.AvailableGoods.FirstOrDefaultAsync(ag => ag.CategoryId == purchaseGoods.CategoryId);
                if (availableGoods != null)
                {
                    // Deduct purchased quantity from available goods
                    availableGoods.AvailableQuantity += purchaseGoods.Quantity;
                    // Remove this line to prevent QuantityUsed from increasing
                    // availableGoods.QuantityUsed += purchaseGoods.Quantity; 
                }

                _context.Add(purchaseGoods);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", purchaseGoods.CategoryId);
            return View(purchaseGoods);
        }

        // GET: PurchaseGoods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PurchaseGoods == null)
            {
                return NotFound();
            }

            var purchaseGoods = await _context.PurchaseGoods.FindAsync(id);
            if (purchaseGoods == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", purchaseGoods.CategoryId);
            return View(purchaseGoods);
        }

        // POST: PurchaseGoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ItemName,Quantity,AmountUsed,CategoryId,PurchaseDate")] PurchaseGoods purchaseGoods)
        {
            if (id != purchaseGoods.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseGoods);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseGoodsExists(purchaseGoods.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", purchaseGoods.CategoryId);
            return View(purchaseGoods);
        }

        // GET: PurchaseGoods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PurchaseGoods == null)
            {
                return NotFound();
            }

            var purchaseGoods = await _context.PurchaseGoods
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseGoods == null)
            {
                return NotFound();
            }

            return View(purchaseGoods);
        }

        // POST: PurchaseGoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PurchaseGoods == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PurchaseGoods'  is null.");
            }
            var purchaseGoods = await _context.PurchaseGoods.FindAsync(id);
            if (purchaseGoods != null)
            {
                _context.PurchaseGoods.Remove(purchaseGoods);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseGoodsExists(int id)
        {
          return (_context.PurchaseGoods?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

