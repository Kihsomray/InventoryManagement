using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

public class ItemController : Controller {
    private readonly InventoryManagementContext _context;

    public ItemController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Item
    public IActionResult Index() {
        var items = _context.Item.Include(i => i.Supplier).ToList();
        return View(items);
    }

    // GET: Item/Details/5
    public IActionResult Details(int? id) {
        if (id == null) return NotFound();
        var item = _context.Item.Include(i => i.Supplier).FirstOrDefault(m => m.ItemID == id);
        if (item == null) return NotFound();
        return View(item);
    }

    // GET: Item/Create
    public IActionResult Create() {
        ViewBag.Suppliers = _context.Supplier.ToList();
        return View();
    }

    // POST: Item/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("ItemID, Name, Category, Description, Price, SupplierID")] Item item) {
        _context.Add(item);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Item/Edit/5
    public IActionResult Edit(int? id) {
        if (id == null) return NotFound();
        var item = _context.Item.Find(id);
        if (item == null) return NotFound();
        ViewBag.Suppliers = _context.Supplier.ToList();
        return View(item);
    }

    // POST: Item/Edit/5
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
    public IActionResult Delete(int? id) {
        if (id == null) return NotFound();
        var item = _context.Item.Include(i => i.Supplier).FirstOrDefault(m => m.ItemID == id);
        if (item == null) return NotFound();
        return View(item);
    }

    // POST: Item/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id) {
        var item = _context.Item.Find(id);
        _context.Item.Remove(item);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    private bool ItemExists(int id) {
        return _context.Item.Any(e => e.ItemID == id);
    }
}
