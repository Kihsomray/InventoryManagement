using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

public class CustomerController : Controller {
    private readonly InventoryManagementContext _context;

    public CustomerController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Customer
    public IActionResult Index() {
        var customers = _context.Customer.ToList();
        return View(customers);
    }

    // GET: Customer/Details/5
    public IActionResult Details(int? id) {
        if (id == null) return NotFound();
        var customer = _context.Customer.FirstOrDefault(m => m.CustomerID == id);
        if (customer == null) return NotFound();
        return View(customer);
    }

    // GET: Customer/Create
    public IActionResult Create() {
        return View();
    }

    // POST: Customer/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("CustomerID, FullName, Email, PhoneNumber, Address, DateOfCreation")] Customer customer) {
        _context.Add(customer);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Customer/Edit/5
    public IActionResult Edit(int? id) {
        if (id == null) return NotFound();
        var customer = _context.Customer.Find(id);
        if (customer == null) return NotFound();
        return View(customer);
    }

    // POST: Customer/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("CustomerID, FullName, Email, PhoneNumber, Address, DateOfCreation")] Customer customer) {
        if (id != customer.CustomerID) return NotFound();
        try {
            _context.Update(customer);
            _context.SaveChanges();
        } catch (DbUpdateConcurrencyException) {
            if (!CustomerExists(customer.CustomerID)) return NotFound();
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Customer/Delete/5
    public IActionResult Delete(int? id) {
        if (id == null) return NotFound();
        var customer = _context.Customer.FirstOrDefault(m => m.CustomerID == id);
        if (customer == null) return NotFound();
        return View(customer);
    }

    // POST: Customer/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id) {
        var customer = _context.Customer.Find(id);
        _context.Customer.Remove(customer);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    private bool CustomerExists(int id) {
        return _context.Customer.Any(e => e.CustomerID == id);
    }
}
