using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectronicsStore.Data;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace ElectronicsStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Change this email to your admin email
        private string GetAdminEmail() => "rahul@gmail.com";

        private bool IsAdmin()
        {
            return User.Identity?.IsAuthenticated == true &&
                   string.Equals(User.Identity.Name, GetAdminEmail(), StringComparison.OrdinalIgnoreCase);
        }

        // GET: Admin/Admin/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            if (!IsAdmin())
                return RedirectToAction("AccessDenied", "Account", new { area = "" });

            var totalProducts = await _context.Products.CountAsync();
            var totalOrders = await _context.Orders.CountAsync();
            var totalRevenue = await _context.Orders.SumAsync(o => (decimal?)o.TotalAmount) ?? 0;
            var pendingOrders = await _context.Orders.CountAsync(o => o.OrderStatus == "Pending");

            ViewBag.TotalProducts = totalProducts;
            ViewBag.TotalOrders = totalOrders;
            ViewBag.TotalRevenue = totalRevenue;
            ViewBag.PendingOrders = pendingOrders;

            var recentOrders = await _context.Orders
                .Include(o => o.OrderItems)
                .OrderByDescending(o => o.OrderDate)
                .Take(10)
                .ToListAsync();

            return View(recentOrders);
        }

        // GET: Admin/Admin/Products
        public async Task<IActionResult> Products()
        {
            if (!IsAdmin())
                return RedirectToAction("AccessDenied", "Account", new { area = "" });

            var products = await _context.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();

            return View(products);
        }

        // POST: Admin/Admin/UpdateStock
        [HttpPost]
        public async Task<IActionResult> UpdateStock(int productId, int quantity)
        {
            try
            {
                if (!IsAdmin())
                {
                    return Json(new { success = false, message = "Unauthorized" });
                }

                var product = await _context.Products.FindAsync(productId);

                if (product == null)
                {
                    return Json(new { success = false, message = $"Product #{productId} not found in database" });
                }

                // Update stock
                product.StockQuantity += quantity;

                // Prevent negative stock
                if (product.StockQuantity < 0)
                {
                    product.StockQuantity = 0;
                }

                await _context.SaveChangesAsync();

                return Json(new
                {
                    success = true,
                    newStock = product.StockQuantity,
                    message = $"{product.ProductName} stock updated to {product.StockQuantity}"
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        // GET: Admin/Admin/Orders
        public async Task<IActionResult> Orders()
        {
            if (!IsAdmin())
                return RedirectToAction("AccessDenied", "Account", new { area = "" });

            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }

        // GET: Admin/Admin/OrderDetails/5
        public async Task<IActionResult> OrderDetails(int id)
        {
            if (!IsAdmin())
                return RedirectToAction("AccessDenied", "Account", new { area = "" });

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                        .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Admin/Admin/UpdateOrderStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, string status)
        {
            if (!IsAdmin())
                return RedirectToAction("AccessDenied", "Account", new { area = "" });

            var order = await _context.Orders.FindAsync(orderId);

            if (order != null)
            {
                order.OrderStatus = status;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Order #{orderId} status updated to {status}!";
            }
            else
            {
                TempData["ErrorMessage"] = "Order not found.";
            }

            return RedirectToAction("Orders");
        }

        // GET: Admin/Admin/Customers
        public async Task<IActionResult> Customers()
        {
            if (!IsAdmin())
                return RedirectToAction("AccessDenied", "Account", new { area = "" });

            var customers = await _context.Orders
                .GroupBy(o => new { o.UserId, o.FullName, o.PhoneNumber })
                .Select(g => new
                {
                    UserId = g.Key.UserId,
                    FullName = g.Key.FullName,
                    PhoneNumber = g.Key.PhoneNumber,
                    TotalOrders = g.Count(),
                    TotalSpent = g.Sum(o => o.TotalAmount),
                    LastOrderDate = g.Max(o => o.OrderDate)
                })
                .OrderByDescending(c => c.TotalSpent)
                .ToListAsync();

            return View(customers);
        }
    }
}