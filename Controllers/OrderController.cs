using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

public class OrderController : Controller {
    private readonly InventoryManagementContext _context;

    public OrderController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Order
    public IActionResult Index() {
        var orders = _context.Order.Include(o => o.Customer).ToList();
        return View(orders);
    }

    // GET: Order/Details/5
    public IActionResult Details(int? id) {
        if (id == null) return NotFound();
        var order = _context.Order.Include(o => o.Customer).FirstOrDefault(m => m.OrderID == id);
        if (order == null) return NotFound();
        return View(order);
    }

    // GET: Order/Create
    public IActionResult Create() {
        ViewBag.Customers = _context.Customer.ToList();
        return View();
    }

    // POST: Order/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("OrderID, CustomerID, OrderDate")] Order order) {
        _context.Add(order);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Order/Edit/5
    public IActionResult Edit(int? id) {
        if (id == null) return NotFound();
        var order = _context.Order.Find(id);
        if (order == null) return NotFound();
        ViewBag.Customers = _context.Customer.ToList();
        return View(order);
    }

    // POST: Order/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("OrderID, CustomerID, OrderDate")] Order order) {
        if (id != order.OrderID) return NotFound();
        try {
            _context.Update(order);
            _context.SaveChanges();
        } catch (DbUpdateConcurrencyException) {
            if (!OrderExists(order.OrderID)) return NotFound();
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Order/Delete/5
    public IActionResult Delete(int? id) {
        if (id == null) return NotFound();
        var order = _context.Order.Include(o => o.Customer).FirstOrDefault(m => m.OrderID == id);
        if (order == null) return NotFound();
        return View(order);
    }

    // POST: Order/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id) {
        var order = _context.Order.Find(id);
        _context.Order.Remove(order);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    private bool OrderExists(int id) {
        return _context.Order.Any(e => e.OrderID == id);
    }
}
