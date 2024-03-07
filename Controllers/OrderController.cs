using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

/// <summary>
/// Controller responsible for managing orders.
/// </summary>
public class OrderController : Controller {
    private readonly InventoryManagementContext _context;

    /// <summary>
    /// Constructor for OrderController.
    /// </summary>
    /// <param name="context">Database context for order management.</param>
    public OrderController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Order
    /// <summary>
    /// Displays the list of orders, including associated customer details.
    /// </summary>
    /// <returns>The view containing the list of orders.</returns>
    public IActionResult Index() {
        var orders = _context.Order.Include(o => o.Customer).ToList();
        return View(orders);
    }

    // GET: Order/Details/5
    /// <summary>
    /// Displays details of a specific order, including associated customer details.
    /// </summary>
    /// <param name="id">ID of the order.</param>
    /// <returns>The view containing details of the order.</returns>
    public IActionResult Details(int? id) {
        if (id == null) return NotFound();
        var order = _context.Order.Include(o => o.Customer).FirstOrDefault(m => m.OrderID == id);
        if (order == null) return NotFound();
        return View(order);
    }

    // GET: Order/Create
    /// <summary>
    /// Displays the form to create a new order.
    /// </summary>
    /// <returns>The view containing the form to create a new order.</returns>
    public IActionResult Create() {
        ViewBag.Customers = _context.Customer.ToList();
        return View();
    }

    // POST: Order/Create
    /// <summary>
    /// Adds a new order to the database.
    /// </summary>
    /// <param name="order">The Order object to be added.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("OrderID, CustomerID, OrderDate")] Order order) {
        _context.Add(order);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Order/Edit/5
    /// <summary>
    /// Displays the form to edit an existing order.
    /// </summary>
    /// <param name="id">ID of the order.</param>
    /// <returns>The view containing the form to edit an existing order.</returns>
    public IActionResult Edit(int? id) {
        if (id == null) return NotFound();
        var order = _context.Order.Find(id);
        if (order == null) return NotFound();
        ViewBag.Customers = _context.Customer.ToList();
        return View(order);
    }

    // POST: Order/Edit/5
    /// <summary>
    /// Updates an existing order in the database.
    /// </summary>
    /// <param name="id">ID of the order.</param>
    /// <param name="order">The Order object with updated information.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
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
    /// <summary>
    /// Displays the confirmation page to delete an order.
    /// </summary>
    /// <param name="id">ID of the order.</param>
    /// <returns>The view containing the confirmation page.</returns>
    public IActionResult Delete(int? id) {
        if (id == null) return NotFound();
        var order = _context.Order.Include(o => o.Customer).FirstOrDefault(m => m.OrderID == id);
        if (order == null) return NotFound();
        return View(order);
    }

    // POST: Order/Delete/5
    /// <summary>
    /// Deletes a specified order from the database.
    /// </summary>
    /// <param name="id">ID of the order.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id) {
        var order = _context.Order.Find(id);
        _context.Order.Remove(order);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Checks if a specific order exists.
    /// </summary>
    /// <param name="id">ID of the order.</param>
    /// <returns>True if the order exists, otherwise false.</returns>
    private bool OrderExists(int id) {
        return _context.Order.Any(e => e.OrderID == id);
    }
}
