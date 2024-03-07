using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

/// <summary>
/// Controller responsible for managing return items.
/// </summary>
public class ReturnController : Controller {
    private readonly InventoryManagementContext _context;

    /// <summary>
    /// Constructor for ReturnController.
    /// </summary>
    /// <param name="context">Database context for return item management.</param>
    public ReturnController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Return
    /// <summary>
    /// Displays the list of return items.
    /// </summary>
    /// <returns>The view containing the list of return items.</returns>
    public IActionResult Index() {
        var returns = _context.Return.ToList();
        return View(returns);
    }

    // GET: Return/Details/5
    /// <summary>
    /// Displays details of a specific return item.
    /// </summary>
    /// <param name="orderId">ID of the order associated with the return item.</param>
    /// <returns>The view containing details of the return item.</returns>
    public IActionResult Details(int? orderId) {
        if (orderId == null) return NotFound();
        var returnItem = _context.Return.FirstOrDefault(r => r.OrderID == orderId);
        if (returnItem == null) return NotFound();
        return View(returnItem);
    }

    // GET: Return/Create
    /// <summary>
    /// Displays the form to create a new return item.
    /// </summary>
    /// <returns>The view containing the form to create a new return item.</returns>
    public IActionResult Create() {
        return View();
    }

    // POST: Return/Create
    /// <summary>
    /// Adds a new return item to the database.
    /// </summary>
    /// <param name="returnItem">The Return object to be added.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("OrderID, ReturnDate, ReturnReason")] Return returnItem) {
        _context.Add(returnItem);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Return/Edit/5
    /// <summary>
    /// Displays the form to edit an existing return item.
    /// </summary>
    /// <param name="orderId">ID of the order associated with the return item.</param>
    /// <returns>The view containing the form to edit an existing return item.</returns>
    public IActionResult Edit(int? orderId) {
        if (orderId == null) return NotFound();
        var returnItem = _context.Return.Find(orderId);
        if (returnItem == null) return NotFound();
        return View(returnItem);
    }

    // POST: Return/Edit/5
    /// <summary>
    /// Updates an existing return item in the database.
    /// </summary>
    /// <param name="orderId">ID of the order associated with the return item.</param>
    /// <param name="returnItem">The Return object with updated information.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int orderId, [Bind("OrderID, ReturnDate, ReturnReason")] Return returnItem){
        if (orderId != returnItem.OrderID) return NotFound();
        try {
            _context.Update(returnItem);
            _context.SaveChanges();
        } catch (DbUpdateConcurrencyException) {
            if (!ReturnExists(returnItem.OrderID)) return NotFound();
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Return/Delete/5
    /// <summary>
    /// Displays the confirmation page to delete a return item.
    /// </summary>
    /// <param name="orderId">ID of the order associated with the return item.</param>
    /// <returns>The view containing the confirmation page.</returns>
    public IActionResult Delete(int? orderId) {
        if (orderId == null) return NotFound();
        var returnItem = _context.Return.FirstOrDefault(r => r.OrderID == orderId);
        if (returnItem == null) return NotFound();
        return View(returnItem);
    }

    // POST: Return/Delete/5
    /// <summary>
    /// Deletes a specified return item from the database.
    /// </summary>
    /// <param name="orderId">ID of the order associated with the return item.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int orderId) {
        var returnItem = _context.Return.Find(orderId);
        _context.Return.Remove(returnItem);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Checks if a specific return item exists.
    /// </summary>
    /// <param name="orderId">ID of the order associated with the return item.</param>
    /// <returns>True if the return item exists, otherwise false.</returns>
    private bool ReturnExists(int orderId) {
        return _context.Return.Any(r => r.OrderID == orderId);
    }
}
