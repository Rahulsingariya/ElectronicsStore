using ElectronicsStore.Data;
using ElectronicsStore.Models;
using ElectronicsStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ElectronicsStore.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Order/Checkout
        public async Task<IActionResult> Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.CartItems.Any())
            {
                TempData["ErrorMessage"] = "Your cart is empty. Add some products first.";
                return RedirectToAction("Index", "Product");
            }

            ViewBag.Cart = cart;
            return View(new CheckoutViewModel());
        }

        // POST: Order/PlaceOrder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var cart = await _context.Carts
                    .Include(c => c.CartItems)
                        .ThenInclude(ci => ci.Product)
                    .FirstOrDefaultAsync(c => c.UserId == userId);

                ViewBag.Cart = cart;
                return View("Checkout", model);
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userCart = await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == currentUserId);

            if (userCart == null || !userCart.CartItems.Any())
            {
                TempData["ErrorMessage"] = "Your cart is empty.";
                return RedirectToAction("Index", "Cart");
            }

            var totalAmount = userCart.CartItems.Sum(item => item.Product.Price * item.Quantity);
            var tax = totalAmount * 0.18m;
            var finalTotal = totalAmount + tax;

            var order = new Order
            {
                UserId = currentUserId,
                OrderDate = DateTime.Now,
                TotalAmount = finalTotal,
                OrderStatus = "Pending",
                ShippingAddress = model.ShippingAddress,
                City = model.City,
                State = model.State,
                ZipCode = model.ZipCode,
                PhoneNumber = model.PhoneNumber,
                FullName = model.FullName,
                PaymentMethod = model.PaymentMethod  // Add this line
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            foreach (var cartItem in userCart.CartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.OrderId,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.Product.Price,
                    TotalPrice = cartItem.Product.Price * cartItem.Quantity
                };

                _context.OrderItems.Add(orderItem);
                cartItem.Product.StockQuantity -= cartItem.Quantity;
            }

            _context.CartItems.RemoveRange(userCart.CartItems);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Order placed successfully!";
            return RedirectToAction("OrderConfirmation", new { orderId = order.OrderId });  // Changed from id to orderId
        }

        // GET: Order/OrderConfirmation
        public async Task<IActionResult> OrderConfirmation(int orderId)  // Changed from id to orderId
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                        .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);  // Changed from id to orderId

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Order/MyOrders
        public async Task<IActionResult> MyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                        .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(o => o.OrderId == id && o.UserId == userId);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}