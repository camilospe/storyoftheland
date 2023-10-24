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

        // GET: Specimens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null || _context.Specimen == null)
            {
                // Returns not found
                return NotFound();
            }

            // Querying the database for the specimen object that has the same ID
            var specimen = await _context.Specimen
                .FirstOrDefaultAsync(m => m.SpecimenID == id);
            if (specimen == null)
            {
                // Returns not found
                return NotFound();
            }
            // Render's the specimen's details.cshtml file
            return View(specimen);
        }

    }
}
