using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

/// <summary>
/// Controller responsible for managing inventory items.
/// </summary>
public class InventoryController : Controller {
    private readonly InventoryManagementContext _context;

    /// <summary>
    /// Constructor for InventoryController.
    /// </summary>
    /// <param name="context">Database context for inventory management.</param>
    public InventoryController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Inventory
    /// <summary>
    /// Displays the list of inventory items, including their locations and details.
    /// </summary>
    /// <returns>The view containing the list of inventory items.</returns>
    public IActionResult Index() {
        var inventoryItems = _context.Inventory.Include(i => i.Location).Include(i => i.Item).ToList();
        return View(inventoryItems);
    }

    // GET: Inventory/Details/5
    /// <summary>
    /// Displays details of a specific inventory item, including its location and details.
    /// </summary>
    /// <param name="locationId">ID of the location where the item is stored.</param>
    /// <param name="itemId">ID of the item.</param>
    /// <returns>The view containing details of the inventory item.</returns>
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
    /// <summary>
    /// Displays the form to create a new inventory item.
    /// </summary>
    /// <returns>The view containing the form to create a new inventory item.</returns>
    public IActionResult Create() {
        ViewBag.Locations = _context.Location.ToList();
        ViewBag.Items = _context.Item.ToList();
        return View();
    }

    // POST: Inventory/Create
    /// <summary>
    /// Adds a new inventory item to the database.
    /// </summary>
    /// <param name="inventoryItem">The Inventory object to be added.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("LocationID, ItemID, ReorderQuantity, ReorderLevel")] Inventory inventoryItem) {
        _context.Add(inventoryItem);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Inventory/Edit/5/1
    /// <summary>
    /// Displays the form to edit an existing inventory item.
    /// </summary>
    /// <param name="locationId">ID of the location where the item is stored.</param>
    /// <param name="itemId">ID of the item.</param>
    /// <returns>The view containing the form to edit an existing inventory item.</returns>
    public IActionResult Edit(int? locationId, int? itemId) {
        if (locationId == null || itemId == null) return NotFound();
        var inventoryItem = _context.Inventory.Find(locationId, itemId);
        if (inventoryItem == null) return NotFound();
        ViewBag.Locations = _context.Location.ToList();
        ViewBag.Items = _context.Item.ToList();
        return View(inventoryItem);
    }

    // POST: Inventory/Edit/5/1
    /// <summary>
    /// Updates an existing inventory item in the database.
    /// </summary>
    /// <param name="locationId">ID of the location where the item is stored.</param>
    /// <param name="itemId">ID of the item.</param>
    /// <param name="inventoryItem">The Inventory object with updated information.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
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
    /// <summary>
    /// Displays the confirmation page to delete an inventory item.
    /// </summary>
    /// <param name="locationId">ID of the location where the item is stored.</param>
    /// <param name="itemId">ID of the item.</param>
    /// <returns>The view containing the confirmation page.</returns>
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
    /// <summary>
    /// Deletes a specified inventory item from the database.
    /// </summary>
    /// <param name="locationId">ID of the location where the item is stored.</param>
    /// <param name="itemId">ID of the item.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int locationId, int itemId) {
        var inventoryItem = _context.Inventory.Find(locationId, itemId);
        _context.Inventory.Remove(inventoryItem);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Checks if a specific inventory item exists.
    /// </summary>
    /// <param name="locationId">ID of the location where the item is stored.</param>
    /// <param name="itemId">ID of the item.</param>
    /// <returns>True if the inventory item exists, otherwise false.</returns>
    private bool InventoryItemExists(int locationId, int itemId) {
        return _context.Inventory.Any(e => e.LocationID == locationId && e.ItemID == itemId);
    }

}
