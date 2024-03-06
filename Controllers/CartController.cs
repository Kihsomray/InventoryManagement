using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

public class CartController : Controller {
    private readonly InventoryManagementContext _context;

    public CartController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Cart
    public IActionResult Index() {
        var carts = _context.Cart.Include(c => c.Customer).Include(c => c.Item).ToList();
        return View(carts);
    }

    // GET: Cart/Details/5
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
    public IActionResult Create() {
        ViewBag.Customers = _context.Customer.ToList();
        ViewBag.Items = _context.Item.ToList();
        return View();
    }

    // POST: Cart/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("CustomerID, ItemID, Quantity")] Cart cart) {
        _context.Add(cart);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Cart/Delete/5
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
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int customerId, int itemId) {
        var cart = _context.Cart.Find(customerId, itemId);
        _context.Cart.Remove(cart);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    private bool CartExists(int customerId, int itemId) {
        return _context.Cart.Any(e => e.CustomerID == customerId && e.ItemID == itemId);
    }
}
