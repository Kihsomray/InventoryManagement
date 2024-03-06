using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

public class ShipmentController : Controller {
    private readonly InventoryManagementContext _context;

    public ShipmentController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Shipment
    public IActionResult Index() {
        var shipments = _context.Shipment.Include(s => s.Order).ToList();
        return View(shipments);
    }

    // GET: Shipment/Details/5
    public IActionResult Details(int? id) {
        if (id == null) return NotFound();
        var shipment = _context.Shipment.Include(s => s.Order).FirstOrDefault(m => m.OrderID == id);
        if (shipment == null) return NotFound();
        return View(shipment);
    }

    // GET: Shipment/Create
    public IActionResult Create() {
        ViewBag.Orders = _context.Order.ToList();
        return View();
    }

    // POST: Shipment/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("OrderID, Date, Status, ShippingNumber")] Shipment shipment) {
        _context.Add(shipment);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Shipment/Edit/5
    public IActionResult Edit(int? id) {
        if (id == null) return NotFound();
        var shipment = _context.Shipment.Find(id);
        if (shipment == null) return NotFound();
        ViewBag.Orders = _context.Order.ToList();
        return View(shipment);
    }

    // POST: Shipment/Edit/5
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
    public IActionResult Delete(int? id) {
        if (id == null) return NotFound();
        var shipment = _context.Shipment.Include(s => s.Order).FirstOrDefault(m => m.OrderID == id);
        if (shipment == null) return NotFound();
        return View(shipment);
    }

    // POST: Shipment/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id) {
        var shipment = _context.Shipment.Find(id);
        _context.Shipment.Remove(shipment);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    private bool ShipmentExists(int id) {
        return _context.Shipment.Any(e => e.OrderID == id);
    }
}
