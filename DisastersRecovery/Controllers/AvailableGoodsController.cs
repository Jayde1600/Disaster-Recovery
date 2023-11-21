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
    public class AvailableGoodsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AvailableGoodsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AvailableGoods
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AvailableGoods.Include(a => a.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AvailableGoods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AvailableGoods == null)
            {
                return NotFound();
            }

            var availableGoods = await _context.AvailableGoods
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (availableGoods == null)
            {
                return NotFound();
            }

            return View(availableGoods);
        }

        // GET: AvailableGoods/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: AvailableGoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryId,AvailableQuantity,QuantityUsed")] AvailableGoods availableGoods)
        {
            if (ModelState.IsValid)
            {
                _context.Add(availableGoods);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", availableGoods.CategoryId);
            return View(availableGoods);
        }

        // GET: AvailableGoods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AvailableGoods == null)
            {
                return NotFound();
            }

            var availableGoods = await _context.AvailableGoods.FindAsync(id);
            if (availableGoods == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", availableGoods.CategoryId);
            return View(availableGoods);
        }

        // POST: AvailableGoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryId,AvailableQuantity,QuantityUsed")] AvailableGoods availableGoods)
        {
            if (id != availableGoods.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(availableGoods);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvailableGoodsExists(availableGoods.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", availableGoods.CategoryId);
            return View(availableGoods);
        }

        // GET: AvailableGoods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AvailableGoods == null)
            {
                return NotFound();
            }

            var availableGoods = await _context.AvailableGoods
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (availableGoods == null)
            {
                return NotFound();
            }

            return View(availableGoods);
        }

        // POST: AvailableGoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AvailableGoods == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AvailableGoods'  is null.");
            }
            var availableGoods = await _context.AvailableGoods.FindAsync(id);
            if (availableGoods != null)
            {
                _context.AvailableGoods.Remove(availableGoods);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvailableGoodsExists(int id)
        {
          return (_context.AvailableGoods?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

