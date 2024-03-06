using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

public class EmployeeController : Controller {
    private readonly InventoryManagementContext _context;

    public EmployeeController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Employee
    public IActionResult Index() {
        var employees = _context.Employee.ToList();
        return View(employees);
    }

    // GET: Employee/Details/5
    public IActionResult Details(int? id) {
        if (id == null)return NotFound();
        var employee = _context.Employee.FirstOrDefault(m => m.EmployeeID == id);
        if (employee == null) return NotFound();
        return View(employee);
    }

    // GET: Employee/Create
    public IActionResult Create() {
        ViewBag.Locations = _context.Location.ToList();
        return View();
    }

    // POST: Employee/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("EmployeeID, LocationID, FullName, Position, Email, PhoneNumber")] Employee employee) {
        _context.Add(employee);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Employee/Edit/5
    public IActionResult Edit(int? id) {
        if (id == null) return NotFound();
        var employee = _context.Employee.Find(id);
        if (employee == null) return NotFound();
        ViewBag.Locations = _context.Location.ToList();
        return View(employee);
    }

    // POST: Employee/Edit/5
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
    public IActionResult Delete(int? id) {
        if (id == null) return NotFound();
        var employee = _context.Employee.FirstOrDefault(m => m.EmployeeID == id);
        if (employee == null) return NotFound();
        return View(employee);
    }

    // POST: Employee/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id) {
        var employee = _context.Employee.Find(id);
        _context.Employee.Remove(employee);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    private bool EmployeeExists(int id) {
        return _context.Employee.Any(e => e.EmployeeID == id);
    }
}
