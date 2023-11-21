using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoriesOfTheLand.Data;
using StoriesOfTheLand.Models;

namespace StoriesOfTheLand.Controllers
{
    public class SpecimenController : Controller
    {
        private readonly StoriesOfTheLandContext _context;

        public SpecimenController(StoriesOfTheLandContext context)
        {
            _context = context;
        }

        // GET: Specimens/Details/5
        //Method gets details for a specimen based in the SpecimenID and returns a 
        //corresponding veiw
        public async Task<IActionResult> Details(int? id)
        {
            //checks to see if the id is null or the specimen context is null
            if (id == null || _context.Specimen == null)
            {
                // Returns not found
                return NotFound();
            }

            //checks the database for the specimen object with the given id
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

        public async Task<IActionResult> Index()
        {
            if (_context.Specimen == null)
            {
                return Problem("Entity set 'StoriesOfTheLandContext.Specimen' is null");
            }

            var specimens = from specimen in _context.Specimen select specimen;

            return View(specimens.ToListAsync());
        }

        public List<Specimen> SortList(string sortOption, string filterOption, List<Specimen> Specimens)
        {
            throw new NotImplementedException();
        }

    }
}
