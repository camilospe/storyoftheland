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
    
    /**
     * http://localhost:32771/specimen/
     * doesnt work - no index
     */
    public class SpecimenController : Controller
    {
        private readonly StoriesOfTheLandContext _context;

        public SpecimenController(StoriesOfTheLandContext context)
        {
            _context = context;
        }

        // GET:  http://localhost:32771/specimen/details/1
        public async Task<IActionResult> Details(int ?Id)
        {
            if (Id == null || _context.Specimen == null)
            {
                return NotFound();
            }

            var specimen = await _context.Specimen
                .FirstOrDefaultAsync(m => m.SpecimenID == Id);

            if (specimen == null)
            {
                return NotFound();
            }


            return View(specimen);
        }
    }
}
