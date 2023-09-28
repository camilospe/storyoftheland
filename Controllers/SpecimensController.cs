using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StorisOfTheLand.Data;
using StorisOfTheLand.Models;

namespace StorisOfTheLand.Controllers
{
    public class SpecimensController : Controller
    {
        private readonly StorisOfTheLandContext _context;

        public SpecimensController(StorisOfTheLandContext context)
        {
            _context = context;
        }

        // GET: Specimens
        public async Task<IActionResult> Index()
        {
              return _context.Specimen != null ? 
                          View(await _context.Specimen.ToListAsync()) :
                          Problem("Entity set 'StorisOfTheLandContext.Specimen'  is null.");
        }

        // GET: Specimens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Specimen == null)
            {
                return NotFound();
            }

            var specimen = await _context.Specimen
                .FirstOrDefaultAsync(m => m.SpecimenID == id);
            if (specimen == null)
            {
                return NotFound();
            }

            return View(specimen);
        }

        // GET: Specimens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Specimens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpecimenID")] Specimen specimen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(specimen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specimen);
        }

        // GET: Specimens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Specimen == null)
            {
                return NotFound();
            }

            var specimen = await _context.Specimen.FindAsync(id);
            if (specimen == null)
            {
                return NotFound();
            }
            return View(specimen);
        }

        // POST: Specimens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpecimenID")] Specimen specimen)
        {
            if (id != specimen.SpecimenID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(specimen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecimenExists(specimen.SpecimenID))
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
            return View(specimen);
        }

        // GET: Specimens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Specimen == null)
            {
                return NotFound();
            }

            var specimen = await _context.Specimen
                .FirstOrDefaultAsync(m => m.SpecimenID == id);
            if (specimen == null)
            {
                return NotFound();
            }

            return View(specimen);
        }

        // POST: Specimens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Specimen == null)
            {
                return Problem("Entity set 'StorisOfTheLandContext.Specimen'  is null.");
            }
            var specimen = await _context.Specimen.FindAsync(id);
            if (specimen != null)
            {
                _context.Specimen.Remove(specimen);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecimenExists(int id)
        {
          return (_context.Specimen?.Any(e => e.SpecimenID == id)).GetValueOrDefault();
        }
    }
}
