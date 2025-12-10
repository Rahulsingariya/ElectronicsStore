using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectronicsStore.Data;
using ElectronicsStore.Models;
using System.Diagnostics;

namespace ElectronicsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Home Page
        public async Task<IActionResult> Index()
        {
            // Get featured products (first 8 products)
            var products = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.IsActive)
                .Take(8)
                .ToListAsync();

            return View(products);
        }

        // About Us Page
        public IActionResult AboutUs()
        {
            return View();
        }

        // Contact Page
        public IActionResult Contact()
        {
            return View();
        }

        // Privacy Policy Page
        public IActionResult Privacy()
        {
            return View();
        }

        // Error Page
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}