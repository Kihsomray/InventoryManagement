using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

/// <summary>
/// Controller responsible for managing feedback from customers.
/// </summary>
public class FeedbackController : Controller {
    private readonly InventoryManagementContext _context;

    /// <summary>
    /// Constructor for FeedbackController.
    /// </summary>
    /// <param name="context">Database context for inventory management.</param>
    public FeedbackController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Feedback
    /// <summary>
    /// Displays the list of feedback entries, including associated orders.
    /// </summary>
    /// <returns>The view containing the list of feedback entries.</returns>
    public IActionResult Index() {
        var feedbackList = _context.Feedback.Include(f => f.Order).ToList();
        return View(feedbackList);
    }

    // GET: Feedback/Details/5
    /// <summary>
    /// Displays details of a specific feedback entry, including associated order.
    /// </summary>
    /// <param name="orderId">ID of the associated order.</param>
    /// <returns>The view containing details of the feedback entry.</returns>
    public IActionResult Details(int? orderId) {
        if (orderId == null) return NotFound();
        var feedback = _context.Feedback.Include(f => f.Order).FirstOrDefault(f => f.OrderID == orderId);
        if (feedback == null) return NotFound();
        return View(feedback);
    }

    // GET: Feedback/Create
    /// <summary>
    /// Displays the form to create a new feedback entry.
    /// </summary>
    /// <returns>The view containing the form to create a new feedback entry.</returns>
    public IActionResult Create() {
        ViewBag.Orders = _context.Order.ToList();
        return View();
    }

    // POST: Feedback/Create
    /// <summary>
    /// Adds a new feedback entry to the database.
    /// </summary>
    /// <param name="feedback">The Feedback object to be added.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("OrderID, Rating, Title, Description")] Feedback feedback) {
        _context.Add(feedback);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Feedback/Edit/5
    /// <summary>
    /// Displays the form to edit an existing feedback entry.
    /// </summary>
    /// <param name="orderId">ID of the associated order.</param>
    /// <returns>The view containing the form to edit an existing feedback entry.</returns>
    public IActionResult Edit(int? orderId) {
        if (orderId == null) return NotFound();
        var feedback = _context.Feedback.Find(orderId);
        if (feedback == null) return NotFound();
        ViewBag.Orders = _context.Order.ToList();
        return View(feedback);
    }

    // POST: Feedback/Edit/5
    /// <summary>
    /// Updates an existing feedback entry in the database.
    /// </summary>
    /// <param name="orderId">ID of the associated order.</param>
    /// <param name="feedback">The Feedback object with updated information.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int orderId, [Bind("OrderID, Rating, Title, Description")] Feedback feedback) {
        if (orderId != feedback.OrderID) return NotFound();
        try {
            _context.Update(feedback);
            _context.SaveChanges();
        } catch (DbUpdateConcurrencyException) {
            if (!FeedbackExists(feedback.OrderID)) return NotFound();
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Feedback/Delete/5
    /// <summary>
    /// Displays the confirmation page to delete a feedback entry.
    /// </summary>
    /// <param name="orderId">ID of the associated order.</param>
    /// <returns>The view containing the confirmation page.</returns>
    public IActionResult Delete(int? orderId) {
        if (orderId == null) return NotFound();
        var feedback = _context.Feedback.Include(f => f.Order).FirstOrDefault(f => f.OrderID == orderId);
        if (feedback == null) return NotFound();
        return View(feedback);
    }

    // POST: Feedback/Delete/5
    /// <summary>
    /// Deletes a specific feedback entry from the database.
    /// </summary>
    /// <param name="orderId">ID of the associated order.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int orderId) {
        var feedback = _context.Feedback.Find(orderId);
        _context.Feedback.Remove(feedback);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Checks if a specific feedback entry exists in the database.
    /// </summary>
    /// <param name="orderId">ID of the associated order.</param>
    /// <returns>True if the feedback entry exists; otherwise, false.</returns>
    private bool FeedbackExists(int orderId) {
        return _context.Feedback.Any(f => f.OrderID == orderId);
    }
}
