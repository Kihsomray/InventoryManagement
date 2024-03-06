using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

public class ReviewController : Controller {
    private readonly InventoryManagementContext _context;

    public ReviewController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Review
    public IActionResult Index() {
        var reviews = _context.Review.Include(r => r.Customer).Include(r => r.Item).ToList();
        return View(reviews);
    }

    // GET: Review/Details/1/2
    public IActionResult Details(int? customerId, int? itemId) {
        if (customerId == null || itemId == null) return NotFound();
        var review = _context.Review
            .Include(r => r.Customer)
            .Include(r => r.Item)
            .FirstOrDefault(r => r.CustomerID == customerId && r.ItemID == itemId);
        if (review == null) return NotFound();
        return View(review);
    }

    // GET: Review/Create
    public IActionResult Create() {
        ViewBag.Customers = _context.Customer.ToList();
        ViewBag.Items = _context.Item.ToList();
        return View();
    }

    // POST: Review/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("CustomerID, ItemID, Rating, Description")] Review review) {
        _context.Add(review);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Review/Edit/1/2
    public IActionResult Edit(int? customerId, int? itemId) {
        if (customerId == null || itemId == null) return NotFound();
        var review = _context.Review
            .Include(r => r.Customer)
            .Include(r => r.Item)
            .FirstOrDefault(r => r.CustomerID == customerId && r.ItemID == itemId);
        if (review == null) return NotFound();
        ViewBag.Customers = _context.Customer.ToList();
        ViewBag.Items = _context.Item.ToList();
        return View(review);
    }

    // POST: Review/Edit/1/2
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int customerId, int itemId, [Bind("CustomerID, ItemID, Rating, Description")] Review review) {
        if (customerId != review.CustomerID || itemId != review.ItemID) return NotFound();
        try {
            _context.Update(review);
            _context.SaveChanges();
        } catch (DbUpdateConcurrencyException) {
            if (!ReviewExists(review.CustomerID, review.ItemID)) return NotFound();
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Review/Delete/1/2
    public IActionResult Delete(int? customerId, int? itemId) {
        if (customerId == null || itemId == null) return NotFound();
        var review = _context.Review
            .Include(r => r.Customer)
            .Include(r => r.Item)
            .FirstOrDefault(r => r.CustomerID == customerId && r.ItemID == itemId);
        if (review == null) return NotFound();
        return View(review);
    }

    // POST: Review/Delete/1/2
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int customerId, int itemId) {
        var review = _context.Review.Find(customerId, itemId);
        _context.Review.Remove(review);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    private bool ReviewExists(int customerId, int itemId) {
        return _context.Review.Any(r => r.CustomerID == customerId && r.ItemID == itemId);
    }
}
