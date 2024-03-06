using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

public class FeedbackController : Controller {
    private readonly InventoryManagementContext _context;

    public FeedbackController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Feedback
    public IActionResult Index() {
        var feedbackList = _context.Feedback.Include(f => f.Order).ToList();
        return View(feedbackList);
    }

    // GET: Feedback/Details/5
    public IActionResult Details(int? orderId) {
        if (orderId == null) return NotFound();
        var feedback = _context.Feedback.Include(f => f.Order).FirstOrDefault(f => f.OrderID == orderId);
        if (feedback == null) return NotFound();
        return View(feedback);
    }

    // GET: Feedback/Create
    public IActionResult Create() {
        ViewBag.Orders = _context.Order.ToList();
        return View();
    }

    // POST: Feedback/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("OrderID, Rating, Title, Description")] Feedback feedback) {
        _context.Add(feedback);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Feedback/Edit/5
    public IActionResult Edit(int? orderId) {
        if (orderId == null) return NotFound();
        var feedback = _context.Feedback.Find(orderId);
        if (feedback == null) return NotFound();
        ViewBag.Orders = _context.Order.ToList();
        return View(feedback);
    }

    // POST: Feedback/Edit/5
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
    public IActionResult Delete(int? orderId) {
        if (orderId == null) return NotFound();
        var feedback = _context.Feedback.Include(f => f.Order).FirstOrDefault(f => f.OrderID == orderId);
        if (feedback == null) return NotFound();
        return View(feedback);
    }

    // POST: Feedback/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int orderId) {
        var feedback = _context.Feedback.Find(orderId);
        _context.Feedback.Remove(feedback);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    private bool FeedbackExists(int orderId) {
        return _context.Feedback.Any(f => f.OrderID == orderId);
    }
}
