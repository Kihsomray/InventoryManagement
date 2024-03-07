using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

/// <summary>
/// Controller responsible for managing suppliers.
/// </summary>
public class SupplierController : Controller {
    private readonly InventoryManagementContext _context;

    /// <summary>
    /// Constructor for the SupplierController.
    /// </summary>
    /// <param name="context">Database context for supplier management.</param>
    public SupplierController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Supplier
    /// <summary>
    /// Displays the list of suppliers.
    /// </summary>
    /// <returns>The view containing the list of suppliers.</returns>
    public IActionResult Index() {
        var suppliers = _context.Supplier.ToList();
        return View(suppliers);
    }

    // GET: Supplier/Details/5
    /// <summary>
    /// Displays details of a specific supplier.
    /// </summary>
    /// <param name="id">ID of the supplier to display details of.</param>
    /// <returns>The view containing details of the supplier.</returns>
    public IActionResult Details(int? id) {
        if (id == null) return NotFound();
        var supplier = _context.Supplier.FirstOrDefault(m => m.SupplierID == id);
        if (supplier == null) return NotFound();
        return View(supplier);
    }

    // GET: Supplier/Create
    /// <summary>
    /// Displays the form to create a new supplier.
    /// </summary>
    /// <returns>The view containing the form to create a new supplier.</returns>
    public IActionResult Create() {
        return View();
    }

    // POST: Supplier/Create
    /// <summary>
    /// Adds a new supplier to the database.
    /// </summary>
    /// <param name="supplier">The Supplier object to be added.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("SupplierID, Name, Email, PhoneNumber, Address, DateOfCreation")] Supplier supplier) {
        _context.Add(supplier);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Supplier/Edit/5
    /// <summary>
    /// Displays the form to edit an existing supplier.
    /// </summary>
    /// <param name="id">ID of the supplier to edit.</param>
    /// <returns>The view containing the form to edit an existing supplier.</returns>
    public IActionResult Edit(int? id) {
        if (id == null) return NotFound();
        var supplier = _context.Supplier.Find(id);
        if (supplier == null) return NotFound();
        return View(supplier);
    }

    // POST: Supplier/Edit/5
    /// <summary>
    /// Updates an existing supplier in the database.
    /// </summary>
    /// <param name="id">ID of the supplier to update.</param>
    /// <param name="supplier">The Supplier object with updated information.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("SupplierID, Name, Email, PhoneNumber, Address, DateOfCreation")] Supplier supplier) {
        if (id != supplier.SupplierID) return NotFound();
        try {
            _context.Update(supplier);
            _context.SaveChanges();
        } catch (DbUpdateConcurrencyException) {
            if (!SupplierExists(supplier.SupplierID)) return NotFound();
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Supplier/Delete/5
    /// <summary>
    /// Displays the confirmation page to delete a supplier.
    /// </summary>
    /// <param name="id">ID of the supplier to delete.</param>
    /// <returns>The view containing the confirmation page.</returns>
    public IActionResult Delete(int? id) {
        if (id == null) return NotFound();
        var supplier = _context.Supplier.FirstOrDefault(m => m.SupplierID == id);
        if (supplier == null) return NotFound();
        return View(supplier);
    }

    // POST: Supplier/Delete/5
    /// <summary>
    /// Deletes a specified supplier from the database.
    /// </summary>
    /// <param name="id">ID of the supplier to delete.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id) {
        var supplier = _context.Supplier.Find(id);
        _context.Supplier.Remove(supplier);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Checks if a specific supplier exists.
    /// </summary>
    /// <param name="id">ID of the supplier to check.</param>
    /// <returns>True if the supplier exists, otherwise false.</returns>
    private bool SupplierExists(int id) {
        return _context.Supplier.Any(e => e.SupplierID == id);
    }
}
