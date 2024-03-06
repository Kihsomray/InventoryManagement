using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

public class SupplierController : Controller {
    private readonly InventoryManagementContext _context;

    public SupplierController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Supplier
    public IActionResult Index() {
        var suppliers = _context.Supplier.ToList();
        return View(suppliers);
    }

    // GET: Supplier/Details/5
    public IActionResult Details(int? id) {
        if (id == null) return NotFound();
        var supplier = _context.Supplier.FirstOrDefault(m => m.SupplierID == id);
        if (supplier == null) return NotFound();
        return View(supplier);
    }

    // GET: Supplier/Create
    public IActionResult Create() {
        return View();
    }

    // POST: Supplier/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("SupplierID, Name, Email, PhoneNumber, Address, DateOfCreation")] Supplier supplier) {
        _context.Add(supplier);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Supplier/Edit/5
    public IActionResult Edit(int? id) {
        if (id == null) return NotFound();
        var supplier = _context.Supplier.Find(id);
        if (supplier == null) return NotFound();
        return View(supplier);
    }

    // POST: Supplier/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("SupplierID, Name, Email, PhoneNumber, Address, DateOfCreation")] Supplier supplier) {
        if (id != supplier.SupplierID)return NotFound();
        try {
            _context.Update(supplier);
            _context.SaveChanges();
        } catch (DbUpdateConcurrencyException){
            if (!SupplierExists(supplier.SupplierID)) return NotFound();
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Supplier/Delete/5
    public IActionResult Delete(int? id) {
        if (id == null)return NotFound();
        var supplier = _context.Supplier.FirstOrDefault(m => m.SupplierID == id);
        if (supplier == null) return NotFound();
        return View(supplier);
    }

    // POST: Supplier/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id) {
        var supplier = _context.Supplier.Find(id);
        _context.Supplier.Remove(supplier);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    private bool SupplierExists(int id) {
        return _context.Supplier.Any(e => e.SupplierID == id);
    }
    
}
