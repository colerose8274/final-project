using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using FinalProject.Data;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class EpisodesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EpisodesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page = 1, int pageSize = 10, string sortBy = "Title", string sortOrder = "asc", string searchQuery = "")
        {
            // Calculate the number of records to skip based on the requested page
            int skip = (page - 1) * pageSize;

            // Retrieve a subset of episodes based on the page size, number, and search query
            var episodesQuery = _context.Episodes.AsQueryable();

            // Apply search query
            episodesQuery = ApplySearch(episodesQuery, searchQuery);

            // Apply sorting
            episodesQuery = ApplySorting(episodesQuery, sortBy, sortOrder);

            // Get the total number of episodes
            int totalEpisodes = episodesQuery.Count();

            // Calculate the total number of pages based on the page size
            int totalPages = (int)Math.Ceiling((double)totalEpisodes / pageSize);

            // Retrieve the episodes for the current page
            var episodes = episodesQuery
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            // Pass the episodes, current page, total pages, sorting options, and search query to the view
            ViewData["Episodes"] = episodes;
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = totalPages;
            ViewData["SortBy"] = sortBy;
            ViewData["SortOrder"] = sortOrder;
            ViewData["SearchQuery"] = searchQuery;

            return View();
        }

        private IQueryable<Episode> ApplySearch(IQueryable<Episode> query, string searchQuery)
        {
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(e => e.Title.Contains(searchQuery));
            }

            return query;
        }

        private IQueryable<Episode> ApplySorting(IQueryable<Episode> query, string sortBy, string sortOrder)
        {
            switch (sortBy)
            {
                case "Title":
                    query = sortOrder == "asc" ? query.OrderBy(e => e.Title) : query.OrderByDescending(e => e.Title);
                    break;
                case "Rating":
                    query = sortOrder == "asc" ? query.OrderBy(e => e.Rating) : query.OrderByDescending(e => e.Rating);
                    break;
                // Add more sorting options if needed
            }

            return query;
        }

        public IActionResult Details(int id)
        {
            Episode episode = _context.Episodes.Include(e => e.Ratings).FirstOrDefault(e => e.Id == id);

            if (episode == null)
            {
                return NotFound();
            }

            List<Rating> ratings = episode.Ratings;

            // Pass the episode and its ratings to the view
            ViewData["Episode"] = episode;
            ViewData["Ratings"] = ratings;

            return View();
}

    }
}
