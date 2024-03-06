using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using InventoryManagement.Data;
using InventoryManagement.Models;

public class EmployeeController : Controller
{
    private readonly InventoryManagementContext _context;

    public EmployeeController(InventoryManagementContext context)
    {
        _context = context;
    }

    // GET: Employee
    public IActionResult Index()
    {
        var employees = _context.Employee
                                .Include(e => e.Location)
                                .ToList();

        return View(employees);
    }

    // GET: Employee/Details/5
    public IActionResult Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var employee = _context.Employee
                                .Include(e => e.Location)
                                .FirstOrDefault(e => e.EmployeeID == id);

        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }

    // GET: Employee/Create
    public IActionResult Create()
    {
        ViewBag.Locations = _context.Location.ToList();
        return View();
    }

    // POST: Employee/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Employee employee)
    {
        if (ModelState.IsValid)
        {
            _context.Employee.Add(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Locations = _context.Location.ToList();
        return View(employee);
    }

    // GET: Employee/Edit/5
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var employee = _context.Employee.Find(id);

        if (employee == null)
        {
            return NotFound();
        }

        ViewBag.Locations = _context.Location.ToList();
        return View(employee);
    }

    // POST: Employee/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Employee employee)
    {
        if (id != employee.EmployeeID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.Update(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Locations = _context.Location.ToList();
        return View(employee);
    }

    // GET: Employee/Delete/5
    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var employee = _context.Employee
                                .Include(e => e.Location)
                                .FirstOrDefault(e => e.EmployeeID == id);

        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }

    // POST: Employee/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var employee = _context.Employee.Find(id);

        if (employee == null)
        {
            return NotFound();
        }

        _context.Employee.Remove(employee);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
