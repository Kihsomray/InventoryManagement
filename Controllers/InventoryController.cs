using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

public class InventoryController : Controller {
    private readonly InventoryManagementContext _context;

    public InventoryController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Inventory
    public IActionResult Index() {
        var inventoryItems = _context.Inventory.Include(i => i.Location).Include(i => i.Item).ToList();
        return View(inventoryItems);
    }

    // GET: Inventory/Details/5
    public IActionResult Details(int? locationId, int? itemId) {
        if (locationId == null || itemId == null) return NotFound();
        var inventoryItem = _context.Inventory
            .Include(i => i.Location)
            .Include(i => i.Item)
            .FirstOrDefault(m => m.LocationID == locationId && m.ItemID == itemId);
        if (inventoryItem == null) return NotFound();
        return View(inventoryItem);
    }

    // GET: Inventory/Create
    public IActionResult Create() {
        ViewBag.Locations = _context.Location.ToList();
        ViewBag.Items = _context.Item.ToList();
        return View();
    }

    // POST: Inventory/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("LocationID, ItemID, ReorderQuantity, ReorderLevel")] Inventory inventoryItem) {
        _context.Add(inventoryItem);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Inventory/Edit/5/1
    public IActionResult Edit(int? locationId, int? itemId) {
        if (locationId == null || itemId == null) return NotFound();
        var inventoryItem = _context.Inventory.Find(locationId, itemId);
        if (inventoryItem == null) return NotFound();
        ViewBag.Locations = _context.Location.ToList();
        ViewBag.Items = _context.Item.ToList();
        return View(inventoryItem);
    }

    // POST: Inventory/Edit/5/1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int locationId, int itemId, [Bind("LocationID, ItemID, ReorderQuantity, ReorderLevel")] Inventory inventoryItem) {
        if (locationId != inventoryItem.LocationID || itemId != inventoryItem.ItemID) return NotFound();
        try {
            _context.Update(inventoryItem);
            _context.SaveChanges();
        } catch (DbUpdateConcurrencyException) {
            if (!InventoryItemExists(inventoryItem.LocationID, inventoryItem.ItemID)) return NotFound();
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Inventory/Delete/5/1
    public IActionResult Delete(int? locationId, int? itemId) {
        if (locationId == null || itemId == null) return NotFound();
        var inventoryItem = _context.Inventory
            .Include(i => i.Location)
            .Include(i => i.Item)
            .FirstOrDefault(m => m.LocationID == locationId && m.ItemID == itemId);
        if (inventoryItem == null) return NotFound();
        return View(inventoryItem);
    }

    // POST: Inventory/Delete/5/1
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int locationId, int itemId) {
        var inventoryItem = _context.Inventory.Find(locationId, itemId);
        _context.Inventory.Remove(inventoryItem);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    private bool InventoryItemExists(int locationId, int itemId) {
        return _context.Inventory.Any(e => e.LocationID == locationId && e.ItemID == itemId);
    }
}
