﻿using System;
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

        // GET: Specimen/
        // This method corresponds with the Index.cshtml page
        // Passing in Search String enables the user to search the specimen index.
        public async Task<IActionResult> Index(string searchString)
        {
            // Check to see if there are NO specimen inside of the list
            if (_context.Specimen == null)
            {
                // Return the problem object, stating that there are no specimen inside of the database
                return Problem("Entity set 'StoriesOfTheLandContext.Specimen' is null");
            }

            // Obtain the list of Specimen from the context
            var specimens = from s in _context.Specimen select s;

            // Check to see if the string that the user searches is NOT empty, or NOT NULL
            if (!String.IsNullOrEmpty(searchString))
            {
                // "For each specimen that contains the search string in English Name,
                // /Latin Name, Cree Name, make the list of specimen contain only those specimens"
                specimens = specimens.Where(S => 
                S.EnglishName.Contains(searchString) || S.LatinName.Contains(searchString) || S.CreeName.Contains(searchString));
   
            }

            // Returns a View of the Specimen
            return View(await specimens.ToListAsync());
        }

        public List<Specimen> SortList(string sortOption, string filterOption, List<Specimen> Specimens)
        {
            throw new NotImplementedException();
        }

    }
}
