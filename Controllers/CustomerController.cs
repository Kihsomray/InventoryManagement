using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

/// <summary>
/// Controller responsible for managing customers.
/// </summary>
public class CustomerController : Controller {
    private readonly InventoryManagementContext _context;

    /// <summary>
    /// Constructor for CustomerController.
    /// </summary>
    /// <param name="context">Database context for inventory management.</param>
    public CustomerController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Customer
    /// <summary>
    /// Displays the list of customers.
    /// </summary>
    /// <returns>The view containing the list of customers.</returns>
    public IActionResult Index() {
        var customers = _context.Customer.ToList();
        return View(customers);
    }

    // GET: Customer/Details/5
    /// <summary>
    /// Displays details of a specific customer.
    /// </summary>
    /// <param name="id">ID of the customer.</param>
    /// <returns>The view containing details of the customer.</returns>
    public IActionResult Details(int? id) {
        if (id == null) return NotFound();
        var customer = _context.Customer.FirstOrDefault(m => m.CustomerID == id);
        if (customer == null) return NotFound();
        return View(customer);
    }

    // GET: Customer/Create
    /// <summary>
    /// Displays the form to create a new customer.
    /// </summary>
    /// <returns>The view containing the form to create a new customer.</returns>
    public IActionResult Create() {
        return View();
    }

    // POST: Customer/Create
    /// <summary>
    /// Adds a new customer to the database.
    /// </summary>
    /// <param name="customer">The Customer object to be added.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("CustomerID, FullName, Email, PhoneNumber, Address, DateOfCreation")] Customer customer) {
        _context.Add(customer);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Customer/Edit/5
    /// <summary>
    /// Displays the form to edit an existing customer.
    /// </summary>
    /// <param name="id">ID of the customer.</param>
    /// <returns>The view containing the form to edit an existing customer.</returns>
    public IActionResult Edit(int? id) {
        if (id == null) return NotFound();
        var customer = _context.Customer.Find(id);
        if (customer == null) return NotFound();
        return View(customer);
    }

    // POST: Customer/Edit/5
    /// <summary>
    /// Updates an existing customer in the database.
    /// </summary>
    /// <param name="id">ID of the customer.</param>
    /// <param name="customer">The Customer object with updated information.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
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
    /// <summary>
    /// Displays the confirmation page to delete a customer.
    /// </summary>
    /// <param name="id">ID of the customer.</param>
    /// <returns>The view containing the confirmation page.</returns>
    public IActionResult Delete(int? id) {
        if (id == null) return NotFound();
        var customer = _context.Customer.FirstOrDefault(m => m.CustomerID == id);
        if (customer == null) return NotFound();
        return View(customer);
    }

    // POST: Customer/Delete/5
    /// <summary>
    /// Deletes a specific customer from the database.
    /// </summary>
    /// <param name="id">ID of the customer.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id) {
        var customer = _context.Customer.Find(id);
        _context.Customer.Remove(customer);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Checks if a specific customer exists in the database.
    /// </summary>
    /// <param name="id">ID of the customer.</param>
    /// <returns>True if the customer exists; otherwise, false.</returns>
    private bool CustomerExists(int id) {
        return _context.Customer.Any(e => e.CustomerID == id);
    }
}
