using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

/// <summary>
/// Controller responsible for managing payments.
/// </summary>
public class PaymentController : Controller {
    
    private readonly InventoryManagementContext _context;

    /// <summary>
    /// Constructor for PaymentController.
    /// </summary>
    /// <param name="context">Database context for payment management.</param>
    public PaymentController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Payment
    /// <summary>
    /// Displays the list of payments, including associated order details.
    /// </summary>
    /// <returns>The view containing the list of payments.</returns>
    public IActionResult Index() {
        var payments = _context.Payment.Include(p => p.Order).ToList();
        return View(payments);
    }

    // GET: Payment/Details/5
    /// <summary>
    /// Displays details of a specific payment, including associated order details.
    /// </summary>
    /// <param name="id">ID of the payment.</param>
    /// <returns>The view containing details of the payment.</returns>
    public IActionResult Details(int? id) {
        if (id == null) return NotFound();
        var payment = _context.Payment.Include(p => p.Order).FirstOrDefault(m => m.OrderID == id);
        if (payment == null) return NotFound();
        return View(payment);
    }

    // GET: Payment/Create
    /// <summary>
    /// Displays the form to create a new payment.
    /// </summary>
    /// <returns>The view containing the form to create a new payment.</returns>
    public IActionResult Create() {
        ViewBag.Orders = _context.Order.ToList();
        return View();
    }

    // POST: Payment/Create
    /// <summary>
    /// Adds a new payment to the database.
    /// </summary>
    /// <param name="payment">The Payment object to be added.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("OrderID, Date, Amount, Method, Completed")] Payment payment) {
        _context.Add(payment);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Payment/Edit/5
    /// <summary>
    /// Displays the form to edit an existing payment.
    /// </summary>
    /// <param name="id">ID of the payment.</param>
    /// <returns>The view containing the form to edit an existing payment.</returns>
    public IActionResult Edit(int? id) {
        if (id == null) return NotFound();
        var payment = _context.Payment.Find(id);
        if (payment == null) return NotFound();
        ViewBag.Orders = _context.Order.ToList();
        return View(payment);
    }

    // POST: Payment/Edit/5
    /// <summary>
    /// Updates an existing payment in the database.
    /// </summary>
    /// <param name="id">ID of the payment.</param>
    /// <param name="payment">The Payment object with updated information.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("OrderID, Date, Amount, Method, Completed")] Payment payment) {
        if (id != payment.OrderID) return NotFound();
        try {
            _context.Update(payment);
            _context.SaveChanges();
        } catch (DbUpdateConcurrencyException) {
            if (!PaymentExists(payment.OrderID)) return NotFound();
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Payment/Delete/5
    /// <summary>
    /// Displays the confirmation page to delete a payment.
    /// </summary>
    /// <param name="id">ID of the payment.</param>
    /// <returns>The view containing the confirmation page.</returns>
    public IActionResult Delete(int? id) {
        if (id == null) return NotFound();
        var payment = _context.Payment.Include(p => p.Order).FirstOrDefault(m => m.OrderID == id);
        if (payment == null) return NotFound();
        return View(payment);
    }

    // POST: Payment/Delete/5
    /// <summary>
    /// Deletes a specified payment from the database.
    /// </summary>
    /// <param name="id">ID of the payment.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id) {
        var payment = _context.Payment.Find(id);
        _context.Payment.Remove(payment);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Checks if a specific payment exists.
    /// </summary>
    /// <param name="id">ID of the payment.</param>
    /// <returns>True if the payment exists, otherwise false.</returns>
    private bool PaymentExists(int id) {
        return _context.Payment.Any(e => e.OrderID == id);
    }
}
