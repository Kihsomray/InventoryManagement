using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

public class MembershipController : Controller {
    private readonly InventoryManagementContext _context;

    public MembershipController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Membership
    public IActionResult Index() {
        var memberships = _context.Membership.Include(m => m.Customer).ToList();
        return View(memberships);
    }

    // GET: Membership/Details/5
    public IActionResult Details(int? id) {
        if (id == null) return NotFound();
        var membership = _context.Membership.Include(m => m.Customer).FirstOrDefault(m => m.CustomerID == id);
        if (membership == null) return NotFound();
        return View(membership);
    }

    // GET: Membership/Create
    public IActionResult Create() {
        ViewBag.Customers = _context.Customer.ToList();
        return View();
    }

    // POST: Membership/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("CustomerID, MembershipType, StartDate, EndDate")] Membership membership) {
        _context.Add(membership);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Membership/Edit/5
    public IActionResult Edit(int? id) {
        if (id == null) return NotFound();
        var membership = _context.Membership.Find(id);
        if (membership == null) return NotFound();
        ViewBag.Customers = _context.Customer.ToList();
        return View(membership);
    }

    // POST: Membership/Edit/5
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
    public IActionResult Delete(int? id) {
        if (id == null) return NotFound();
        var membership = _context.Membership.Include(m => m.Customer).FirstOrDefault(m => m.CustomerID == id);
        if (membership == null) return NotFound();
        return View(membership);
    }

    // POST: Membership/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id) {
        var membership = _context.Membership.Find(id);
        _context.Membership.Remove(membership);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    private bool MembershipExists(int id) {
        return _context.Membership.Any(e => e.CustomerID == id);
    }
}
