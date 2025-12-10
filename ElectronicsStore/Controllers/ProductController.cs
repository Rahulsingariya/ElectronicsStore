using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectronicsStore.Data;
using ElectronicsStore.Models;
using ElectronicsStore.Models.ViewModels;
using System.Security.Claims;

namespace ElectronicsStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Product/Index
        public async Task<IActionResult> Index(int? categoryId, string? category, string? searchString)
        {
            var productsQuery = _context.Products
                .Include(p => p.Category)
                .Where(p => p.IsActive)
                .AsQueryable();

            // Filter by category NAME (from home page links)
            if (!string.IsNullOrEmpty(category))
            {
                productsQuery = productsQuery.Where(p => p.Category.CategoryName == category);
                ViewBag.SelectedCategoryName = category;
            }
            // OR Filter by category ID (from dropdown)
            else if (categoryId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId.Value);
                var selectedCat = await _context.Categories.FindAsync(categoryId.Value);
                ViewBag.SelectedCategoryName = selectedCat?.CategoryName;
            }

            // Filter by search string
            if (!string.IsNullOrEmpty(searchString))
            {
                productsQuery = productsQuery.Where(p =>
                    p.ProductName.Contains(searchString) ||
                    p.Description.Contains(searchString) ||
                    p.Category.CategoryName.Contains(searchString)
                );
                ViewBag.SearchString = searchString;
            }

            var products = await productsQuery
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();

            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.SelectedCategory = categoryId;

            return View(products);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            // Get reviews for this product (if Review model exists)
            try
            {
                var reviews = await _context.Reviews
                    .Where(r => r.ProductId == id)
                    .OrderByDescending(r => r.ReviewDate)
                    .ToListAsync();

                ViewBag.Reviews = reviews;
                ViewBag.AverageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0;
                ViewBag.TotalReviews = reviews.Count;

                // Check if current user has already reviewed
                if (User.Identity?.IsAuthenticated == true)
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    ViewBag.HasReviewed = await _context.Reviews
                        .AnyAsync(r => r.ProductId == id && r.UserId == userId);
                }
                else
                {
                    ViewBag.HasReviewed = false;
                }
            }
            catch
            {
                // If Reviews table doesn't exist, just skip it
                ViewBag.Reviews = new List<Review>();
                ViewBag.AverageRating = 0;
                ViewBag.TotalReviews = 0;
                ViewBag.HasReviewed = false;
            }

            return View(product);
        }

        // POST: Product/AddReview
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddReview(ReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Check if user already reviewed this product
                var existingReview = await _context.Reviews
                    .FirstOrDefaultAsync(r => r.ProductId == model.ProductId && r.UserId == userId);

                if (existingReview != null)
                {
                    TempData["ErrorMessage"] = "You have already reviewed this product.";
                    return RedirectToAction("Details", new { id = model.ProductId });
                }

                var review = new Review
                {
                    ProductId = model.ProductId,
                    UserId = userId,
                    Rating = model.Rating,
                    Comment = model.Comment,
                    ReviewDate = DateTime.Now
                };

                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Thank you for your review!";
            }

            return RedirectToAction("Details", new { id = model.ProductId });
        }
    }
}