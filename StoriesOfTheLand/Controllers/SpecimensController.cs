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
        //Method gets details for a specimen based in the SpecimenID and returns a 
        //corresponding veiw
        public async Task<IActionResult> Details(int? id)
        {
            //checks to see if the id is null or the specimen context is null
            if (id == null || _context.Specimen == null)
            {
                return NotFound();
            }

            //checks the database for the specimen object with the given id
            var specimen = await _context.Specimen
                .FirstOrDefaultAsync(m => m.SpecimenID ==id);
            if (specimen == null)
            {
                return NotFound();
            }

            //returns the corresponding review of specimen
            return View(specimen);
        }
    }
}
