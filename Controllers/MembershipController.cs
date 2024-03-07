using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

/// <summary>
/// Controller responsible for managing memberships.
/// </summary>
public class MembershipController : Controller {
    private readonly InventoryManagementContext _context;

    /// <summary>
    /// Constructor for MembershipController.
    /// </summary>
    /// <param name="context">Database context for membership management.</param>
    public MembershipController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Membership
    /// <summary>
    /// Displays the list of memberships, including associated customer details.
    /// </summary>
    /// <returns>The view containing the list of memberships.</returns>
    public IActionResult Index() {
        var memberships = _context.Membership.Include(m => m.Customer).ToList();
        return View(memberships);
    }

    // GET: Membership/Details/5
    /// <summary>
    /// Displays details of a specific membership, including associated customer details.
    /// </summary>
    /// <param name="id">ID of the customer.</param>
    /// <returns>The view containing details of the membership.</returns>
    public IActionResult Details(int? id) {
        if (id == null) return NotFound();
        var membership = _context.Membership.Include(m => m.Customer).FirstOrDefault(m => m.CustomerID == id);
        if (membership == null) return NotFound();
        return View(membership);
    }

    // GET: Membership/Create
    /// <summary>
    /// Displays the form to create a new membership.
    /// </summary>
    /// <returns>The view containing the form to create a new membership.</returns>
    public IActionResult Create() {
        ViewBag.Customers = _context.Customer.ToList();
        return View();
    }

    // POST: Membership/Create
    /// <summary>
    /// Adds a new membership to the database.
    /// </summary>
    /// <param name="membership">The Membership object to be added.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("CustomerID, MembershipType, StartDate, EndDate")] Membership membership) {
        _context.Add(membership);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Membership/Edit/5
    /// <summary>
    /// Displays the form to edit an existing membership.
    /// </summary>
    /// <param name="id">ID of the customer.</param>
    /// <returns>The view containing the form to edit an existing membership.</returns>
    public IActionResult Edit(int? id) {
        if (id == null) return NotFound();
        var membership = _context.Membership.Find(id);
        if (membership == null) return NotFound();
        ViewBag.Customers = _context.Customer.ToList();
        return View(membership);
    }

    // POST: Membership/Edit/5
    /// <summary>
    /// Updates an existing membership in the database.
    /// </summary>
    /// <param name="id">ID of the customer.</param>
    /// <param name="membership">The Membership object with updated information.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("CustomerID, MembershipType, StartDate, EndDate")] Membership membership) {
        if (id != membership.CustomerID) return NotFound();
        try {
            _context.Update(membership);
            _context.SaveChanges();
        } catch (DbUpdateConcurrencyException) {
            if (!MembershipExists(membership.CustomerID)) return NotFound();
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Membership/Delete/5
    /// <summary>
    /// Displays the confirmation page to delete a membership.
    /// </summary>
    /// <param name="id">ID of the customer.</param>
    /// <returns>The view containing the confirmation page.</returns>
    public IActionResult Delete(int? id) {
        if (id == null) return NotFound();
        var membership = _context.Membership.Include(m => m.Customer).FirstOrDefault(m => m.CustomerID == id);
        if (membership == null) return NotFound();
        return View(membership);
    }

    // POST: Membership/Delete/5
    /// <summary>
    /// Deletes a specified membership from the database.
    /// </summary>
    /// <param name="id">ID of the customer.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id) {
        var membership = _context.Membership.Find(id);
        _context.Membership.Remove(membership);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Checks if a specific membership exists.
    /// </summary>
    /// <param name="id">ID of the customer.</param>
    /// <returns>True if the membership exists, otherwise false.</returns>
    private bool MembershipExists(int id) {
        return _context.Membership.Any(e => e.CustomerID == id);
    }
}
