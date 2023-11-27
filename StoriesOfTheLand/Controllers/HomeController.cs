using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoriesOfTheLand.Data;
using StoriesOfTheLand.Models;
using System.Diagnostics;

namespace StoriesOfTheLand.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly StoriesOfTheLandContext _context;

        public HomeController(StoriesOfTheLandContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return _context.Sponsor != null ?
                          View(await _context.Sponsor.ToListAsync()) :
                          Problem("Entity set 'StorisOfTheLandContext.Specimen'  is null.");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}