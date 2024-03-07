using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

/// <summary>
/// Controller responsible for managing discounts.
/// </summary>
public class DiscountController : Controller {
    private readonly InventoryManagementContext _context;

    /// <summary>
    /// Constructor for DiscountController.
    /// </summary>
    /// <param name="context">Database context for inventory management.</param>
    public DiscountController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Discount
    /// <summary>
    /// Displays the list of discounts.
    /// </summary>
    /// <returns>The view containing the list of discounts.</returns>
    public IActionResult Index() {
        var discounts = _context.Discount.Include(d => d.Item).ToList();
        return View(discounts);
    }

    // GET: Discount/Details/5
    /// <summary>
    /// Displays details of a specific discount.
    /// </summary>
    /// <param name="id">ID of the discount.</param>
    /// <returns>The view containing details of the discount.</returns>
    public IActionResult Details(int? id) {
        if (id == null) return NotFound();
        var discount = _context.Discount.Include(d => d.Item).FirstOrDefault(m => m.ItemID == id);
        if (discount == null) return NotFound();
        return View(discount);
    }

    // GET: Discount/Create
    /// <summary>
    /// Displays the form to create a new discount.
    /// </summary>
    /// <returns>The view containing the form to create a new discount.</returns>
    public IActionResult Create() {
        ViewBag.Items = _context.Item.ToList();
        return View();
    }

    // POST: Discount/Create
    /// <summary>
    /// Adds a new discount to the database.
    /// </summary>
    /// <param name="discount">The Discount object to be added.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("ItemID, Percentage, StartDate, EndDate, QuantityUsed, UsageLimit")] Discount discount) {
        _context.Add(discount);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Discount/Edit/5
    /// <summary>
    /// Displays the form to edit an existing discount.
    /// </summary>
    /// <param name="id">ID of the discount.</param>
    /// <returns>The view containing the form to edit an existing discount.</returns>
    public IActionResult Edit(int? id) {
        if (id == null) return NotFound();
        var discount = _context.Discount.Find(id);
        if (discount == null) return NotFound();
        ViewBag.Items = _context.Item.ToList();
        return View(discount);
    }

    // POST: Discount/Edit/5
    /// <summary>
    /// Updates an existing discount in the database.
    /// </summary>
    /// <param name="id">ID of the discount.</param>
    /// <param name="discount">The Discount object with updated information.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("ItemID, Percentage, StartDate, EndDate, QuantityUsed, UsageLimit")] Discount discount) {
        if (id != discount.ItemID) return NotFound();
        try {
            _context.Update(discount);
            _context.SaveChanges();
        } catch (DbUpdateConcurrencyException) {
            if (!DiscountExists(discount.ItemID)) return NotFound();
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Discount/Delete/5
    /// <summary>
    /// Displays the confirmation page to delete a discount.
    /// </summary>
    /// <param name="id">ID of the discount.</param>
    /// <returns>The view containing the confirmation page.</returns>
    public IActionResult Delete(int? id) {
        if (id == null) return NotFound();
        var discount = _context.Discount.Include(d => d.Item).FirstOrDefault(m => m.ItemID == id);
        if (discount == null) return NotFound();
        return View(discount);
    }

    // POST: Discount/Delete/5
    /// <summary>
    /// Deletes a specific discount from the database.
    /// </summary>
    /// <param name="id">ID of the discount.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id) {
        var discount = _context.Discount.Find(id);
        _context.Discount.Remove(discount);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Checks if a specific discount exists in the database.
    /// </summary>
    /// <param name="id">ID of the discount.</param>
    /// <returns>True if the discount exists; otherwise, false.</returns>
    private bool DiscountExists(int id) {
        return _context.Discount.Any(e => e.ItemID == id);
    }
    
}
