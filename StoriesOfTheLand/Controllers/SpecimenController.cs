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
    public class SpecimenController : Controller
    {
        private readonly StoriesOfTheLandContext _context;

        public SpecimenController(StoriesOfTheLandContext context)
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

        // GET: Specimen/Details/5
        public async Task<IActionResult> Details(string ?specimenName)
        {
            if (specimenName == null || _context.Specimen == null)
            {
                return NotFound();
            }

            var specimen = await _context.Specimen
                .FirstOrDefaultAsync(m => m.SpecimenName.Equals(specimenName));

            if (specimen == null)
            {
                return NotFound();
            }


            return View(specimen);
        }
    }
}
