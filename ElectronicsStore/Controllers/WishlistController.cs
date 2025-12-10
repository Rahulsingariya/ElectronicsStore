using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectronicsStore.Data;
using ElectronicsStore.Models;
using System.Security.Claims;

namespace ElectronicsStore.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WishlistController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Wishlist
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var wishlistItems = await _context.Wishlists
                .Include(w => w.Product)
                    .ThenInclude(p => p.Category)
                .Where(w => w.UserId == userId)
                .OrderByDescending(w => w.AddedDate)
                .ToListAsync();

            return View(wishlistItems);
        }

        // POST: Wishlist/AddToWishlist
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToWishlist(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Check if product exists
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                TempData["ToastType"] = "error";
                TempData["ToastTitle"] = "Product Not Found";
                TempData["ToastMessage"] = "The product you're looking for doesn't exist";
                return RedirectToAction("Index", "Product");
            }

            // Check if already in wishlist
            var existingItem = await _context.Wishlists
                .FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == productId);

            if (existingItem != null)
            {
                TempData["ToastType"] = "info";
                TempData["ToastTitle"] = "Already in Wishlist";
                TempData["ToastMessage"] = $"{product.ProductName} is already in your wishlist";
                return RedirectToAction("Index");
            }

            // Add to wishlist
            var wishlistItem = new Wishlist
            {
                UserId = userId,
                ProductId = productId,
                AddedDate = DateTime.Now
            };

            _context.Wishlists.Add(wishlistItem);
            await _context.SaveChangesAsync();

            TempData["ToastType"] = "success";
            TempData["ToastTitle"] = "Added to Wishlist";
            TempData["ToastMessage"] = $"{product.ProductName} has been added to your wishlist";

            return RedirectToAction("Index");
        }

        // POST: Wishlist/RemoveFromWishlist
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromWishlist(int wishlistId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var wishlistItem = await _context.Wishlists
                .Include(w => w.Product)
                .FirstOrDefaultAsync(w => w.WishlistId == wishlistId && w.UserId == userId);

            if (wishlistItem == null)
            {
                TempData["ToastType"] = "error";
                TempData["ToastTitle"] = "Item Not Found";
                TempData["ToastMessage"] = "Wishlist item not found";
                return RedirectToAction("Index");
            }

            var productName = wishlistItem.Product.ProductName;
            _context.Wishlists.Remove(wishlistItem);
            await _context.SaveChangesAsync();

            TempData["ToastType"] = "info";
            TempData["ToastTitle"] = "Removed from Wishlist";
            TempData["ToastMessage"] = $"{productName} has been removed from your wishlist";

            return RedirectToAction("Index");
        }

        // POST: Wishlist/MoveToCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MoveToCart(int wishlistId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var wishlistItem = await _context.Wishlists
                .Include(w => w.Product)
                .FirstOrDefaultAsync(w => w.WishlistId == wishlistId && w.UserId == userId);

            if (wishlistItem == null)
            {
                TempData["ToastType"] = "error";
                TempData["ToastTitle"] = "Item Not Found";
                TempData["ToastMessage"] = "Wishlist item not found";
                return RedirectToAction("Index");
            }

            // Check stock availability
            if (wishlistItem.Product.StockQuantity <= 0)
            {
                TempData["ToastType"] = "warning";
                TempData["ToastTitle"] = "Out of Stock";
                TempData["ToastMessage"] = $"{wishlistItem.Product.ProductName} is currently out of stock";
                return RedirectToAction("Index");
            }

            // Get or create cart
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

            // Check if product already in cart
            var existingCartItem = cart.CartItems
                .FirstOrDefault(ci => ci.ProductId == wishlistItem.ProductId);

            if (existingCartItem != null)
            {
                // Increase quantity if already in cart
                existingCartItem.Quantity += 1;

                if (existingCartItem.Quantity > wishlistItem.Product.StockQuantity)
                {
                    TempData["ToastType"] = "warning";
                    TempData["ToastTitle"] = "Stock Limit Reached";
                    TempData["ToastMessage"] = $"Cannot add more. Only {wishlistItem.Product.StockQuantity} units available";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                // Add new cart item
                var cartItem = new CartItem
                {
                    CartId = cart.CartId,
                    ProductId = wishlistItem.ProductId,
                    Quantity = 1,
                    AddedDate = DateTime.Now
                };
                _context.CartItems.Add(cartItem);
            }

            // Remove from wishlist
            var productName = wishlistItem.Product.ProductName;
            _context.Wishlists.Remove(wishlistItem);
            await _context.SaveChangesAsync();

            TempData["ToastType"] = "success";
            TempData["ToastTitle"] = "Moved to Cart";
            TempData["ToastMessage"] = $"{productName} has been moved to your cart";

            return RedirectToAction("Index");
        }

        // POST: Wishlist/ClearWishlist
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClearWishlist()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var wishlistItems = await _context.Wishlists
                .Where(w => w.UserId == userId)
                .ToListAsync();

            if (wishlistItems.Any())
            {
                _context.Wishlists.RemoveRange(wishlistItems);
                await _context.SaveChangesAsync();

                TempData["ToastType"] = "success";
                TempData["ToastTitle"] = "Wishlist Cleared";
                TempData["ToastMessage"] = $"{wishlistItems.Count} items have been removed from your wishlist";
            }
            else
            {
                TempData["ToastType"] = "info";
                TempData["ToastTitle"] = "Empty Wishlist";
                TempData["ToastMessage"] = "Your wishlist is already empty";
            }

            return RedirectToAction("Index");
        }

        // POST: Wishlist/MoveAllToCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MoveAllToCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var wishlistItems = await _context.Wishlists
                .Include(w => w.Product)
                .Where(w => w.UserId == userId)
                .ToListAsync();

            if (!wishlistItems.Any())
            {
                TempData["ToastType"] = "info";
                TempData["ToastTitle"] = "Empty Wishlist";
                TempData["ToastMessage"] = "Your wishlist is empty";
                return RedirectToAction("Index");
            }

            // Get or create cart
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

            int movedCount = 0;
            int skippedCount = 0;

            foreach (var wishlistItem in wishlistItems)
            {
                // Skip out of stock items
                if (wishlistItem.Product.StockQuantity <= 0)
                {
                    skippedCount++;
                    continue;
                }

                // Check if already in cart
                var existingCartItem = cart.CartItems
                    .FirstOrDefault(ci => ci.ProductId == wishlistItem.ProductId);

                if (existingCartItem != null)
                {
                    // Increase quantity
                    if (existingCartItem.Quantity < wishlistItem.Product.StockQuantity)
                    {
                        existingCartItem.Quantity += 1;
                        movedCount++;
                    }
                    else
                    {
                        skippedCount++;
                    }
                }
                else
                {
                    // Add to cart
                    var cartItem = new CartItem
                    {
                        CartId = cart.CartId,
                        ProductId = wishlistItem.ProductId,
                        Quantity = 1,
                        AddedDate = DateTime.Now
                    };
                    _context.CartItems.Add(cartItem);
                    movedCount++;
                }
            }

            // Remove all from wishlist
            _context.Wishlists.RemoveRange(wishlistItems);
            await _context.SaveChangesAsync();

            if (movedCount > 0)
            {
                TempData["ToastType"] = "success";
                TempData["ToastTitle"] = "Moved to Cart";
                TempData["ToastMessage"] = $"{movedCount} items moved to cart" +
                    (skippedCount > 0 ? $" ({skippedCount} items skipped)" : "");
            }
            else
            {
                TempData["ToastType"] = "warning";
                TempData["ToastTitle"] = "Unable to Move";
                TempData["ToastMessage"] = "All items are either out of stock or already in cart";
            }

            return RedirectToAction("Index", "Cart");
        }
    }
}