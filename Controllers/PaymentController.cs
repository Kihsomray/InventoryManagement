using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

public class PaymentController : Controller {
    private readonly InventoryManagementContext _context;

    public PaymentController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Payment
    public IActionResult Index() {
        var payments = _context.Payment.Include(p => p.Order).ToList();
        return View(payments);
    }

    // GET: Payment/Details/5
    public IActionResult Details(int? id) {
        if (id == null) return NotFound();
        var payment = _context.Payment.Include(p => p.Order).FirstOrDefault(m => m.OrderID == id);
        if (payment == null) return NotFound();
        return View(payment);
    }

    // GET: Payment/Create
    public IActionResult Create() {
        ViewBag.Orders = _context.Order.ToList();
        return View();
    }

    // POST: Payment/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("OrderID, Date, Amount, Method, Completed")] Payment payment) {
        _context.Add(payment);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Payment/Edit/5
    public IActionResult Edit(int? id) {
        if (id == null) return NotFound();
        var payment = _context.Payment.Find(id);
        if (payment == null) return NotFound();
        ViewBag.Orders = _context.Order.ToList();
        return View(payment);
    }

    // POST: Payment/Edit/5
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
    public IActionResult Delete(int? id) {
        if (id == null) return NotFound();
        var payment = _context.Payment.Include(p => p.Order).FirstOrDefault(m => m.OrderID == id);
        if (payment == null) return NotFound();
        return View(payment);
    }

    // POST: Payment/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id) {
        var payment = _context.Payment.Find(id);
        _context.Payment.Remove(payment);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    private bool PaymentExists(int id) {
        return _context.Payment.Any(e => e.OrderID == id);
    }
}
