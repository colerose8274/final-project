using Microsoft.AspNetCore.Mvc;
using FinalProject.Models;
using FinalProject.Data;

namespace FinalProject.Controllers
{
    public class EpisodesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EpisodesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Episodes
        public IActionResult Index()
        {
            // Retrieve episodes from the database
            var episodes = _context.Episodes;

            // Pass the episodes to the view
            return View(episodes);
        }

        // GET: Episodes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Retrieve the episode with the specified ID from the database
            var episode = _context.Episodes.Find(id);

            if (episode == null)
            {
                return NotFound();
            }

            return View(episode);
        }

        // GET: Episodes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Episodes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Description")] Episode episode)
        {
            if (ModelState.IsValid)
            {
                // Save the new episode to the database
                _context.Episodes.Add(episode);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(episode);
        }

        // Other action methods...

    }
}
