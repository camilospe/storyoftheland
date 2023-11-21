using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoriesOfTheLand.Data;
using StoriesOfTheLand.Models;

namespace StoriesOfTheLand
{
    public class SponsorController : Controller
    {
        private readonly StoriesOfTheLandContext _context;

        public SponsorController(StoriesOfTheLandContext context)
        {
            _context = context;
        }

        // GET: Sponsor
        public async Task<IActionResult> Index()
        {
              return _context.Sponsor != null ? 
                          View(await _context.Sponsor.ToListAsync()) :
                          Problem("Entity set 'StoriesOfTheLandContext.Sponsor'  is null.");
        }

        // GET: Sponsor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sponsor == null)
            {
                return NotFound();
            }

            var sponsor = await _context.Sponsor
                .FirstOrDefaultAsync(m => m.SponsorID == id);
            if (sponsor == null)
            {
                return NotFound();
            }

            return View(sponsor);
        }

        // GET: Sponsor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sponsor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SponsorID,SponsorName,SponsorURL")] Sponsor sponsor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sponsor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sponsor);
        }

        // GET: Sponsor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sponsor == null)
            {
                return NotFound();
            }

            var sponsor = await _context.Sponsor.FindAsync(id);
            if (sponsor == null)
            {
                return NotFound();
            }
            return View(sponsor);
        }

        // POST: Sponsor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SponsorID,SponsorName,SponsorURL")] Sponsor sponsor)
        {
            if (id != sponsor.SponsorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sponsor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SponsorExists(sponsor.SponsorID))
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
            return View(sponsor);
        }

        // GET: Sponsor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sponsor == null)
            {
                return NotFound();
            }

            var sponsor = await _context.Sponsor
                .FirstOrDefaultAsync(m => m.SponsorID == id);
            if (sponsor == null)
            {
                return NotFound();
            }

            return View(sponsor);
        }

        // POST: Sponsor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sponsor == null)
            {
                return Problem("Entity set 'StoriesOfTheLandContext.Sponsor'  is null.");
            }
            var sponsor = await _context.Sponsor.FindAsync(id);
            if (sponsor != null)
            {
                _context.Sponsor.Remove(sponsor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SponsorExists(int id)
        {
          return (_context.Sponsor?.Any(e => e.SponsorID == id)).GetValueOrDefault();
        }
    }
}
