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
    public class GoodsDonationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GoodsDonationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GoodsDonations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.GoodsDonation.Include(g => g.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: GoodsDonations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GoodsDonation == null)
            {
                return NotFound();
            }

            var goodsDonation = await _context.GoodsDonation
                .Include(g => g.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (goodsDonation == null)
            {
                return NotFound();
            }

            return View(goodsDonation);
        }

        // GET: GoodsDonations/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: GoodsDonations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DonationDate,NumberOfItems,CategoryId,Description,IsAnonymous,DonorName")] GoodsDonation goodsDonation)
        {
            if (ModelState.IsValid)
            {
                // Set the DonationDate to the current date in the server's time zone
                goodsDonation.DonationDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.Local);

                _context.Add(goodsDonation);
                await _context.SaveChangesAsync();

                // Update AvailableQuantity in AvailableGoods
                var availableGoods = await _context.AvailableGoods
                .FirstOrDefaultAsync(g => g.CategoryId == goodsDonation.CategoryId);

                if (availableGoods != null)
                {
                    availableGoods.AvailableQuantity += goodsDonation.NumberOfItems;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // Create new entry in AvailableGoods if category doesn't exist
                    var newAvailableGoodsEntry = new AvailableGoods
                    {
                        CategoryId = goodsDonation.CategoryId,
                        AvailableQuantity = goodsDonation.NumberOfItems,
                        QuantityUsed = 0
                        // Other properties or relationships as needed
                    };

                    _context.Add(newAvailableGoodsEntry);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", goodsDonation.CategoryId);
            return View(goodsDonation);
        }

        // GET: GoodsDonations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GoodsDonation == null)
            {
                return NotFound();
            }

            var goodsDonation = await _context.GoodsDonation.FindAsync(id);
            if (goodsDonation == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", goodsDonation.CategoryId);
            return View(goodsDonation);
        }

        // POST: GoodsDonations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DonationDate,NumberOfItems,CategoryId,Description,IsAnonymous,DonorName")] GoodsDonation goodsDonation)
        {
            if (id != goodsDonation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(goodsDonation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GoodsDonationExists(goodsDonation.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", goodsDonation.CategoryId);
            return View(goodsDonation);
        }

        // GET: GoodsDonations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GoodsDonation == null)
            {
                return NotFound();
            }

            var goodsDonation = await _context.GoodsDonation
                .Include(g => g.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (goodsDonation == null)
            {
                return NotFound();
            }

            return View(goodsDonation);
        }

        // POST: GoodsDonations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GoodsDonation == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GoodsDonation'  is null.");
            }
            var goodsDonation = await _context.GoodsDonation.FindAsync(id);
            if (goodsDonation != null)
            {
                _context.GoodsDonation.Remove(goodsDonation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GoodsDonationExists(int id)
        {
          return (_context.GoodsDonation?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
