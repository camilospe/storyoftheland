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


        // GET: Specimen/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Specimens/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("SpecimenID,LatinName,SpecimenDescription,EnglishName,CreeName,CulturalSignificance,SpecimenMedia")] Specimen specimen)
        {
            if (ModelState.IsValid)
            {
                // Check for duplicate names in the database
                var existingSpecimen = await _context.Specimen
                    .FirstOrDefaultAsync(s => s.EnglishName == specimen.EnglishName
                                           || s.LatinName == specimen.LatinName
                                           || s.CreeName == specimen.CreeName);

                if (existingSpecimen != null)
                {
                    // Add an error to ModelState if a duplicate is found
                    ModelState.AddModelError(string.Empty, "A specimen with the same name(s) already exists. Please double check");
                    return View(specimen); // Return to the form with the current data and error message
                }
                Console.WriteLine("The media is "+specimen.SpecimenMedia.SpecimenImagePath);
                // If no duplicates, proceed to add the new specimen
                _context.Add(specimen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // If ModelState is not valid, return to the form with the current data
                return View(specimen);
            }
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
                .Include(s=> s.SpecimenMedia)
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
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewData["EnglishSortParm"] = sortOrder == "EnglishName" ? "EnglishName" : "EnglishName";
            ViewData["EnglishSortParmDescending"] = sortOrder == "EnglishName_Desc" ? "EnglishName_Desc" : "EnglishName_Desc";
            ViewData["LatinSortParm"] = sortOrder == "LatinName" ? "LatinName" : "LatinName";
            ViewData["LatinSortParmDescending"] = sortOrder == "LatinName_Desc" ? "LatinName_Desc" : "LatinName_Desc";
            ViewData["CreeSortParm"] = sortOrder == "CreeName" ? "CreeName" : "CreeName";
            ViewData["CreeSortParmDescending"] = sortOrder == "CreeName_Desc" ? "CreeName_Desc" : "CreeName_Desc";



            // Check to see if there are NO specimen inside of the list
            if (_context.Specimen == null)
            {
                // Return the problem object, stating that there are no specimen inside of the database
                return Problem("Entity set 'StoriesOfTheLandContext.Specimen' is null");
            }

            // Obtain the list of Specimen from the context
            var specimens = from s in _context.Specimen select s;

            switch (sortOrder)
            {
                case "EnglishName_Desc":
                    specimens = specimens.OrderByDescending(s => s.EnglishName);
                    
                    break;
                case "EnglishName": 
                    specimens = specimens.OrderBy(s => s.EnglishName);
                    break;
                case "LatinName_Desc":
                    specimens = specimens.OrderByDescending(s => s.LatinName);
                    break;
                case "LatinName":
                    specimens = specimens.OrderBy(s => s.LatinName);
                    break;
                case "CreeName_Desc":
                    specimens = specimens.OrderByDescending(s => s.CreeName);
                    break;
                case "CreeName":
                    specimens = specimens.OrderBy(s => s.CreeName);
                    break;
            }


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
    }
}
