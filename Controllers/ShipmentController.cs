using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

/// <summary>
/// Controller responsible for managing shipments.
/// </summary>
public class ShipmentController : Controller {
    private readonly InventoryManagementContext _context;

    /// <summary>
    /// Constructor for the ShipmentController.
    /// </summary>
    /// <param name="context">Database context for shipment management.</param>
    public ShipmentController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Shipment
    /// <summary>
    /// Displays the list of shipments.
    /// </summary>
    /// <returns>The view containing the list of shipments.</returns>
    public IActionResult Index() {
        var shipments = _context.Shipment.Include(s => s.Order).ToList();
        return View(shipments);
    }

    // GET: Shipment/Details/5
    /// <summary>
    /// Displays details of a specific shipment.
    /// </summary>
    /// <param name="id">ID of the shipment to display details of.</param>
    /// <returns>The view containing details of the shipment.</returns>
    public IActionResult Details(int? id) {
        if (id == null) return NotFound();
        var shipment = _context.Shipment.Include(s => s.Order).FirstOrDefault(m => m.OrderID == id);
        if (shipment == null) return NotFound();
        return View(shipment);
    }

    // GET: Shipment/Create
    /// <summary>
    /// Displays the form to create a new shipment.
    /// </summary>
    /// <returns>The view containing the form to create a new shipment.</returns>
    public IActionResult Create() {
        ViewBag.Orders = _context.Order.ToList();
        return View();
    }

    // POST: Shipment/Create
    /// <summary>
    /// Adds a new shipment to the database.
    /// </summary>
    /// <param name="shipment">The Shipment object to be added.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("OrderID, Date, Status, ShippingNumber")] Shipment shipment) {
        _context.Add(shipment);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Shipment/Edit/5
    /// <summary>
    /// Displays the form to edit an existing shipment.
    /// </summary>
    /// <param name="id">ID of the shipment to edit.</param>
    /// <returns>The view containing the form to edit an existing shipment.</returns>
    public IActionResult Edit(int? id) {
        if (id == null) return NotFound();
        var shipment = _context.Shipment.Find(id);
        if (shipment == null) return NotFound();
        ViewBag.Orders = _context.Order.ToList();
        return View(shipment);
    }

    // POST: Shipment/Edit/5
    /// <summary>
    /// Updates an existing shipment in the database.
    /// </summary>
    /// <param name="id">ID of the shipment to update.</param>
    /// <param name="shipment">The Shipment object with updated information.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("OrderID, Date, Status, ShippingNumber")] Shipment shipment) {
        if (id != shipment.OrderID) return NotFound();
        try {
            _context.Update(shipment);
            _context.SaveChanges();
        } catch (DbUpdateConcurrencyException) {
            if (!ShipmentExists(shipment.OrderID)) return NotFound();
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Shipment/Delete/5
    /// <summary>
    /// Displays the confirmation page to delete a shipment.
    /// </summary>
    /// <param name="id">ID of the shipment to delete.</param>
    /// <returns>The view containing the confirmation page.</returns>
    public IActionResult Delete(int? id) {
        if (id == null) return NotFound();
        var shipment = _context.Shipment.Include(s => s.Order).FirstOrDefault(m => m.OrderID == id);
        if (shipment == null) return NotFound();
        return View(shipment);
    }

    // POST: Shipment/Delete/5
    /// <summary>
    /// Deletes a specified shipment from the database.
    /// </summary>
    /// <param name="id">ID of the shipment to delete.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id) {
        var shipment = _context.Shipment.Find(id);
        _context.Shipment.Remove(shipment);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Checks if a specific shipment exists.
    /// </summary>
    /// <param name="id">ID of the shipment to check.</param>
    /// <returns>True if the shipment exists, otherwise false.</returns>
    private bool ShipmentExists(int id) {
        return _context.Shipment.Any(e => e.OrderID == id);
    }
}
