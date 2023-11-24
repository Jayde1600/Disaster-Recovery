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
    public class MonetaryDonationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MonetaryDonationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MonetaryDonations
        public async Task<IActionResult> Index()
        {
              return _context.MonetaryDonation != null ? 
                          View(await _context.MonetaryDonation.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.MonetaryDonation'  is null.");
        }

        // GET: MonetaryDonations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MonetaryDonation == null)
            {
                return NotFound();
            }

            var monetaryDonation = await _context.MonetaryDonation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (monetaryDonation == null)
            {
                return NotFound();
            }

            return View(monetaryDonation);
        }

        // GET: MonetaryDonations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MonetaryDonations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DonationDate,Amount,IsAnonymous,DonorName")] MonetaryDonation monetaryDonation)
        {
            if (ModelState.IsValid)
            {
                monetaryDonation.DonationDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddHours(2), TimeZoneInfo.Local);

                _context.Add(monetaryDonation);
                await _context.SaveChangesAsync();

                // Retrieve the AvailableMoney record
                var availableMoney = await _context.AvailableMoney.FirstOrDefaultAsync();

                if (availableMoney == null)
                {
                    // Create a new AvailableMoney record if none exists
                    availableMoney = new AvailableMoney { TotalAmount = monetaryDonation.Amount, AmountUsed = 0 };
                    _context.Add(availableMoney);
                }
                else
                {
                    // Update the TotalAmount by adding the new donation amount
                    availableMoney.TotalAmount += monetaryDonation.Amount;
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(monetaryDonation);
        }


        // GET: MonetaryDonations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MonetaryDonation == null)
            {
                return NotFound();
            }

            var monetaryDonation = await _context.MonetaryDonation.FindAsync(id);
            if (monetaryDonation == null)
            {
                return NotFound();
            }
            return View(monetaryDonation);
        }

        // POST: MonetaryDonations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DonationDate,Amount,IsAnonymous,DonorName")] MonetaryDonation monetaryDonation)
        {
            if (id != monetaryDonation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monetaryDonation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonetaryDonationExists(monetaryDonation.Id))
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
            return View(monetaryDonation);
        }

        // GET: MonetaryDonations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MonetaryDonation == null)
            {
                return NotFound();
            }

            var monetaryDonation = await _context.MonetaryDonation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (monetaryDonation == null)
            {
                return NotFound();
            }

            return View(monetaryDonation);
        }

        // POST: MonetaryDonations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MonetaryDonation == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MonetaryDonation'  is null.");
            }
            var monetaryDonation = await _context.MonetaryDonation.FindAsync(id);
            if (monetaryDonation != null)
            {
                _context.MonetaryDonation.Remove(monetaryDonation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MonetaryDonationExists(int id)
        {
          return (_context.MonetaryDonation?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
