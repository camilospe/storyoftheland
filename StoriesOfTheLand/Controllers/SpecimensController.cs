using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoriesOfTheLand.Data;
using StorisOfTheLand.Models;

namespace StoriesOfTheLand.Controllers
{
    public class SpecimensController : Controller
    {
        private readonly StoriesOfTheLandContext _context;

        public SpecimensController(StoriesOfTheLandContext context)
        {
            _context = context;
        }
        // GET: Specimen
        public async Task<IActionResult> Index()
        {
            return _context.Specimen != null ?
                        View(await _context.Specimen.ToListAsync()) :
                        Problem("Entity set 'StoriesOfTheLandContext.Specimen'  is null.");
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
    }
}
