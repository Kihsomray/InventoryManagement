using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

/// <summary>
/// Controller responsible for managing items.
/// </summary>
public class ItemController : Controller {
    private readonly InventoryManagementContext _context;

    /// <summary>
    /// Constructor for ItemController.
    /// </summary>
    /// <param name="context">Database context for item management.</param>
    public ItemController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Item
    /// <summary>
    /// Displays the list of items, including their supplier details.
    /// </summary>
    /// <returns>The view containing the list of items.</returns>
    public IActionResult Index() {
        var items = _context.Item.Include(i => i.Supplier).ToList();
        return View(items);
    }

    // GET: Item/Details/5
    /// <summary>
    /// Displays details of a specific item, including its supplier details.
    /// </summary>
    /// <param name="id">ID of the item.</param>
    /// <returns>The view containing details of the item.</returns>
    public IActionResult Details(int? id) {
        if (id == null) return NotFound();
        var item = _context.Item.Include(i => i.Supplier).FirstOrDefault(m => m.ItemID == id);
        if (item == null) return NotFound();
        return View(item);
    }

    // GET: Item/Create
    /// <summary>
    /// Displays the form to create a new item.
    /// </summary>
    /// <returns>The view containing the form to create a new item.</returns>
    public IActionResult Create() {
        ViewBag.Suppliers = _context.Supplier.ToList();
        return View();
    }

    // POST: Item/Create
    /// <summary>
    /// Adds a new item to the database.
    /// </summary>
    /// <param name="item">The Item object to be added.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("ItemID, Name, Category, Description, Price, SupplierID")] Item item) {
        _context.Add(item);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Item/Edit/5
    /// <summary>
    /// Displays the form to edit an existing item.
    /// </summary>
    /// <param name="id">ID of the item.</param>
    /// <returns>The view containing the form to edit an existing item.</returns>
    public IActionResult Edit(int? id) {
        if (id == null) return NotFound();
        var item = _context.Item.Find(id);
        if (item == null) return NotFound();
        ViewBag.Suppliers = _context.Supplier.ToList();
        return View(item);
    }

    // POST: Item/Edit/5
    /// <summary>
    /// Displays the form to edit an existing item.
    /// </summary>
    /// <param name="id">ID of the item.</param>
    /// <returns>The view containing the form to edit an existing item.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("ItemID, Name, Category, Description, Price, SupplierID")] Item item) {
        if (id != item.ItemID) return NotFound();
        try {
            _context.Update(item);
            _context.SaveChanges();
        } catch (DbUpdateConcurrencyException) {
            if (!ItemExists(item.ItemID)) return NotFound();
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Item/Delete/5
    /// <summary>
    /// Displays the confirmation page to delete an item.
    /// </summary>
    /// <param name="id">ID of the item.</param>
    /// <returns>The view containing the confirmation page.</returns>
    public IActionResult Delete(int? id) {
        if (id == null) return NotFound();
        var item = _context.Item.Include(i => i.Supplier).FirstOrDefault(m => m.ItemID == id);
        if (item == null) return NotFound();
        return View(item);
    }

    // POST: Item/Delete/5
    /// <summary>
    /// Deletes a specified item from the database.
    /// </summary>
    /// <param name="id">ID of the item.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id) {
        var item = _context.Item.Find(id);
        _context.Item.Remove(item);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Checks if a specific item exists.
    /// </summary>
    /// <param name="id">ID of the item.</param>
    /// <returns>True if the item exists, otherwise false.</returns>
    private bool ItemExists(int id) {
        return _context.Item.Any(e => e.ItemID == id);
    }
}
