using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

/// <summary>
/// Controller responsible for managing reviews.
/// </summary>
public class ReviewController : Controller {
    private readonly InventoryManagementContext _context;

    /// <summary>
    /// Constructor for ReviewController.
    /// </summary>
    /// <param name="context">Database context for review management.</param>
    public ReviewController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Review
    /// <summary>
    /// Displays the list of reviews.
    /// </summary>
    /// <returns>The view containing the list of reviews.</returns>
    public IActionResult Index() {
        var reviews = _context.Review.Include(r => r.Customer).Include(r => r.Item).ToList();
        return View(reviews);
    }

    // GET: Review/Details/1/2
    /// <summary>
    /// Displays details of a specific review.
    /// </summary>
    /// <param name="customerId">ID of the customer who created the review.</param>
    /// <param name="itemId">ID of the item associated with the review.</param>
    /// <returns>The view containing details of the review.</returns>
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
    /// <summary>
    /// Displays the form to create a new review.
    /// </summary>
    /// <returns>The view containing the form to create a new review.</returns>
    public IActionResult Create() {
        ViewBag.Customers = _context.Customer.ToList();
        ViewBag.Items = _context.Item.ToList();
        return View();
    }

    // POST: Review/Create
    /// <summary>
    /// Adds a new review to the database.
    /// </summary>
    /// <param name="review">The Review object to be added.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("CustomerID, ItemID, Rating, Description")] Review review) {
        _context.Add(review);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Review/Edit/1/2
    /// <summary>
    /// Displays the form to edit an existing review.
    /// </summary>
    /// <param name="customerId">ID of the customer who created the review.</param>
    /// <param name="itemId">ID of the item associated with the review.</param>
    /// <returns>The view containing the form to edit an existing review.</returns>
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
    /// <summary>
    /// Updates an existing review in the database.
    /// </summary>
    /// <param name="customerId">ID of the customer who created the review.</param>
    /// <param name="itemId">ID of the item associated with the review.</param>
    /// <param name="review">The Review object with updated information.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
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
    /// <summary>
    /// Displays the confirmation page to delete a review.
    /// </summary>
    /// <param name="customerId">ID of the customer who created the review.</param>
    /// <param name="itemId">ID of the item associated with the review.</param>
    /// <returns>The view containing the confirmation page.</returns>
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
    /// <summary>
    /// Deletes a specified review from the database.
    /// </summary>
    /// <param name="customerId">ID of the customer who created the review.</param>
    /// <param name="itemId">ID of the item associated with the review.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int customerId, int itemId) {
        var review = _context.Review.Find(customerId, itemId);
        _context.Review.Remove(review);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Checks if a specific review exists.
    /// </summary>
    /// <param name="customerId">ID of the customer who created the review.</param>
    /// <param name="itemId">ID of the item associated with the review.</param>
    /// <returns>True if the review exists, otherwise false.</returns>
    private bool ReviewExists(int customerId, int itemId) {
        return _context.Review.Any(r => r.CustomerID == customerId && r.ItemID == itemId);
    }
}
