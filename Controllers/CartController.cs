using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

/// <summary>
/// Controller responsible for managing shopping cart items. (Each Customer has multiple carts because each item is tied to its own cart)
/// </summary>
public class CartController : Controller {
    private readonly InventoryManagementContext _context;

    /// <summary>
    /// Constructor for CartController.
    /// </summary>
    /// <param name="context">Database context for inventory management.</param>
    public CartController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Cart
    /// <summary>
    /// Displays the list of items in the shopping cart.
    /// </summary>
    /// <returns>The view containing the list of items in the shopping cart.</returns>
    public IActionResult Index() {
        var carts = _context.Cart.Include(c => c.Customer).Include(c => c.Item).ToList();
        return View(carts);
    }

    // GET: Cart/Details/5
    /// <summary>
    /// Displays details of a specific item in the shopping cart.
    /// </summary>
    /// <param name="customerId">ID of the customer.</param>
    /// <param name="itemId">ID of the item.</param>
    /// <returns>The view containing details of the item in the shopping cart.</returns>
    public IActionResult Details(int? customerId, int? itemId) {
        if (customerId == null || itemId == null) return NotFound();
        var cart = _context.Cart
            .Include(c => c.Customer)
            .Include(c => c.Item)
            .FirstOrDefault(m => m.CustomerID == customerId && m.ItemID == itemId);
        if (cart == null) return NotFound();
        return View(cart);
    }

    // GET: Cart/Create
    /// <summary>
    /// Displays the form to create a new item in the shopping cart.
    /// </summary>
    /// <returns>The view containing the form to create a new item in the shopping cart.</returns>
    public IActionResult Create() {
        ViewBag.Customers = _context.Customer.ToList();
        ViewBag.Items = _context.Item.ToList();
        return View();
    }

    // POST: Cart/Create
    /// <summary>
    /// Adds a new item to the shopping cart.
    /// </summary>
    /// <param name="cart">The Cart object to be added.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("CustomerID, ItemID, Quantity")] Cart cart) {
        _context.Add(cart);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Cart/Delete/5
    /// <summary>
    /// Displays the confirmation page to delete an item from the shopping cart.
    /// </summary>
    /// <param name="customerId">ID of the customer.</param>
    /// <param name="itemId">ID of the item.</param>
    /// <returns>The view containing the confirmation page.</returns>
    public IActionResult Delete(int? customerId, int? itemId) {
        if (customerId == null || itemId == null) return NotFound();
        var cart = _context.Cart
            .Include(c => c.Customer)
            .Include(c => c.Item)
            .FirstOrDefault(m => m.CustomerID == customerId && m.ItemID == itemId);
        if (cart == null) return NotFound();
        return View(cart);
    }

    // POST: Cart/Delete/5
    /// <summary>
    /// Deletes a specific item from the shopping cart.
    /// </summary>
    /// <param name="customerId">ID of the customer.</param>
    /// <param name="itemId">ID of the item.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int customerId, int itemId) {
        var cart = _context.Cart.Find(customerId, itemId);
        _context.Cart.Remove(cart);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Checks if a specific item exists in the shopping cart.
    /// </summary>
    /// <param name="customerId">ID of the customer.</param>
    /// <param name="itemId">ID of the item.</param>
    /// <returns>True if the item exists in the shopping cart; otherwise, false.</returns>
    private bool CartExists(int customerId, int itemId) {
        return _context.Cart.Any(e => e.CustomerID == customerId && e.ItemID == itemId);
    }
}
