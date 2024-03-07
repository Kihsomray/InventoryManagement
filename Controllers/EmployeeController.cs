using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

/// <summary>
/// Controller responsible for managing employees.
/// </summary>
public class EmployeeController : Controller {
    private readonly InventoryManagementContext _context;

    /// <summary>
    /// Constructor for EmployeeController.
    /// </summary>
    /// <param name="context">Database context for inventory management.</param>
    public EmployeeController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Employee
    /// <summary>
    /// Displays the list of employees.
    /// </summary>
    /// <returns>The view containing the list of employees.</returns>
    public IActionResult Index() {
        var employees = _context.Employee.ToList();
        return View(employees);
    }

    // GET: Employee/Details/5
    /// <summary>
    /// Displays details of a specific employee.
    /// </summary>
    /// <param name="id">ID of the employee.</param>
    /// <returns>The view containing details of the employee.</returns>
    public IActionResult Details(int? id) {
        if (id == null)return NotFound();
        var employee = _context.Employee.FirstOrDefault(m => m.EmployeeID == id);
        if (employee == null) return NotFound();
        return View(employee);
    }

    // GET: Employee/Create
    /// <summary>
    /// Displays the form to create a new employee.
    /// </summary>
    /// <returns>The view containing the form to create a new employee.</returns>
    public IActionResult Create() {
        ViewBag.Locations = _context.Location.ToList();
        return View();
    }

    // POST: Employee/Create
    /// <summary>
    /// Adds a new employee to the database.
    /// </summary>
    /// <param name="employee">The Employee object to be added.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("EmployeeID, LocationID, FullName, Position, Email, PhoneNumber")] Employee employee) {
        _context.Add(employee);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Employee/Edit/5
    /// <summary>
    /// Displays the form to edit an existing employee.
    /// </summary>
    /// <param name="id">ID of the employee.</param>
    /// <returns>The view containing the form to edit an existing employee.</returns>
    public IActionResult Edit(int? id) {
        if (id == null) return NotFound();
        var employee = _context.Employee.Find(id);
        if (employee == null) return NotFound();
        ViewBag.Locations = _context.Location.ToList();
        return View(employee);
    }

    // POST: Employee/Edit/5
    /// <summary>
    /// Updates an existing employee in the database.
    /// </summary>
    /// <param name="id">ID of the employee.</param>
    /// <param name="employee">The Employee object with updated information.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("EmployeeID, LocationID, FullName, Position, Email, PhoneNumber")] Employee employee){
        if (id != employee.EmployeeID) return NotFound();
        try {
            _context.Update(employee);
            _context.SaveChanges();
        } catch (DbUpdateConcurrencyException) {
            if (!EmployeeExists(employee.EmployeeID)) return NotFound();
        }
        return RedirectToAction(nameof(Index));

    }

    // GET: Employee/Delete/5
    /// <summary>
    /// Displays the confirmation page to delete an employee.
    /// </summary>
    /// <param name="id">ID of the employee.</param>
    /// <returns>The view containing the confirmation page.</returns>
    public IActionResult Delete(int? id) {
        if (id == null) return NotFound();
        var employee = _context.Employee.FirstOrDefault(m => m.EmployeeID == id);
        if (employee == null) return NotFound();
        return View(employee);
    }

    // POST: Employee/Delete/5
    /// <summary>
    /// Deletes a specific employee from the database.
    /// </summary>
    /// <param name="id">ID of the employee.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id) {
        var employee = _context.Employee.Find(id);
        _context.Employee.Remove(employee);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Checks if a specific employee exists in the database.
    /// </summary>
    /// <param name="id">ID of the employee.</param>
    /// <returns>True if the employee exists; otherwise, false.</returns>
    private bool EmployeeExists(int id) {
        return _context.Employee.Any(e => e.EmployeeID == id);
    }
}
