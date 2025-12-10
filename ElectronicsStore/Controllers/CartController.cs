using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectronicsStore.Data;
using ElectronicsStore.Models;
using ElectronicsStore.Models.ViewModels;
using System.Security.Claims;

namespace ElectronicsStore.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cart
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                        .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.CartItems.Any())
            {
                ViewBag.Message = "Your cart is empty";
                return View(new Cart { CartItems = new List<CartItem>() });
            }

            return View(cart);
        }

        // POST: Cart/AddToCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                TempData["ToastType"] = "error";
                TempData["ToastTitle"] = "Product Not Found";
                TempData["ToastMessage"] = "The product you're looking for doesn't exist";
                return RedirectToAction("Index", "Product");
            }

            if (product.StockQuantity < quantity)
            {
                TempData["ToastType"] = "warning";
                TempData["ToastTitle"] = "Low Stock";
                TempData["ToastMessage"] = $"Only {product.StockQuantity} units available in stock";
                return RedirectToAction("Details", "Product", new { id = productId });
            }

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CreatedDate = DateTime.Now
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var existingCartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;

                if (existingCartItem.Quantity > product.StockQuantity)
                {
                    TempData["ToastType"] = "warning";
                    TempData["ToastTitle"] = "Stock Limit Reached";
                    TempData["ToastMessage"] = $"Cannot add more. Only {product.StockQuantity} units available";
                    return RedirectToAction("Details", "Product", new { id = productId });
                }
            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId = cart.CartId,
                    ProductId = productId,
                    Quantity = quantity,
                    AddedDate = DateTime.Now
                };
                _context.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();

            TempData["ToastType"] = "success";
            TempData["ToastTitle"] = "Added to Cart";
            TempData["ToastMessage"] = $"{product.ProductName} has been added to your cart";

            return RedirectToAction("Index");
        }

        // POST: Cart/UpdateQuantity
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, int quantity)
        {
            var cartItem = await _context.CartItems
                .Include(ci => ci.Product)
                .FirstOrDefaultAsync(ci => ci.CartItemId == cartItemId);

            if (cartItem == null)
            {
                TempData["ToastType"] = "error";
                TempData["ToastTitle"] = "Item Not Found";
                TempData["ToastMessage"] = "Cart item not found";
                return RedirectToAction("Index");
            }

            if (quantity <= 0)
            {
                TempData["ToastType"] = "warning";
                TempData["ToastTitle"] = "Invalid Quantity";
                TempData["ToastMessage"] = "Quantity must be at least 1";
                return RedirectToAction("Index");
            }

            if (quantity > cartItem.Product.StockQuantity)
            {
                TempData["ToastType"] = "warning";
                TempData["ToastTitle"] = "Stock Unavailable";
                TempData["ToastMessage"] = $"Only {cartItem.Product.StockQuantity} units available";
                return RedirectToAction("Index");
            }

            cartItem.Quantity = quantity;
            await _context.SaveChangesAsync();

            TempData["ToastType"] = "success";
            TempData["ToastTitle"] = "Cart Updated";
            TempData["ToastMessage"] = "Quantity has been updated";
            return RedirectToAction("Index");
        }

        // POST: Cart/RemoveItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveItem(int cartItemId)
        {
            var cartItem = await _context.CartItems
                .Include(ci => ci.Product)
                .FirstOrDefaultAsync(ci => ci.CartItemId == cartItemId);

            if (cartItem == null)
            {
                TempData["ToastType"] = "error";
                TempData["ToastTitle"] = "Error";
                TempData["ToastMessage"] = "Cart item not found";
                return RedirectToAction("Index");
            }

            var productName = cartItem.Product.ProductName;
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            TempData["ToastType"] = "info";
            TempData["ToastTitle"] = "Removed from Cart";
            TempData["ToastMessage"] = $"{productName} has been removed from your cart";
            return RedirectToAction("Index");
        }

        // POST: Cart/ClearCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClearCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart != null)
            {
                _context.CartItems.RemoveRange(cart.CartItems);
                await _context.SaveChangesAsync();

                TempData["ToastType"] = "success";
                TempData["ToastTitle"] = "Cart Cleared";
                TempData["ToastMessage"] = "All items have been removed from your cart";
            }

            return RedirectToAction("Index");
        }

        // GET: Cart/Checkout
        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.CartItems.Any())
            {
                TempData["ToastType"] = "warning";
                TempData["ToastTitle"] = "Empty Cart";
                TempData["ToastMessage"] = "Please add items before checkout";
                return RedirectToAction("Index");
            }

            var cartTotal = cart.CartItems.Sum(c => c.Product.Price * c.Quantity);

            ViewBag.CartItems = cart.CartItems;
            ViewBag.CartTotal = cartTotal;

            return View(new CheckoutViewModel());
        }

        // POST: Cart/PlaceOrder
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

                ViewBag.CartItems = cart?.CartItems ?? new List<CartItem>();
                ViewBag.CartTotal = cart?.CartItems.Sum(c => c.Product.Price * c.Quantity) ?? 0;
                return View("Checkout", model);
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userCart = await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == currentUserId);

            if (userCart == null || !userCart.CartItems.Any())
            {
                TempData["ToastType"] = "error";
                TempData["ToastTitle"] = "Empty Cart";
                TempData["ToastMessage"] = "Your cart is empty";
                return RedirectToAction("Index");
            }

            var orderItems = userCart.CartItems.Select(ci => new OrderItem
            {
                ProductId = ci.ProductId,
                Quantity = ci.Quantity,
                UnitPrice = ci.Product.Price
            }).ToList();

            var totalAmount = orderItems.Sum(oi => oi.UnitPrice * oi.Quantity);

            var order = new Order
            {
                UserId = currentUserId,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                ShippingAddress = model.ShippingAddress,
                City = model.City,
                State = model.State,
                ZipCode = model.ZipCode,
                PaymentMethod = model.PaymentMethod,
                OrderDate = DateTime.Now,
                OrderStatus = "Pending",
                TotalAmount = totalAmount,
                OrderItems = orderItems
            };

            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(userCart.CartItems);
            await _context.SaveChangesAsync();

            TempData["ToastType"] = "success";
            TempData["ToastTitle"] = "Order Placed Successfully";
            TempData["ToastMessage"] = $"Your order #{order.OrderId} has been confirmed";

            return RedirectToAction("OrderConfirmation", new { orderId = order.OrderId });
        }
    }
}