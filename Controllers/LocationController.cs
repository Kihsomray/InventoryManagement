using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;

public class LocationController : Controller
{
    private readonly InventoryManagementContext _context;

    public LocationController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Location
    public IActionResult Index() {
        var locations = _context.Location.ToList();
        return View(locations);
    }

    // GET: Location/Details/5
    public IActionResult Details(int? id) {
        if (id == null) return NotFound();
        var location = _context.Location.FirstOrDefault(m => m.LocationID == id);
        if (location == null) return NotFound();
        var employees = _context.Employee.Where(e => e.LocationID == id).ToList();
        var inventory = _context.Inventory.Where(i => i.LocationID == id).ToList();
        var locationViewModel = new LocationViewModel {
            Location = location,
            Employees = employees,
            Inventory = inventory
        };
        return View(locationViewModel);
    }

    // GET: Location/Create
    public IActionResult Create() {
        return View();
    }

    // POST: Location/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("LocationID, Name, Address")] Location location) {
        _context.Add(location);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Location/Edit/5
    public IActionResult Edit(int? id) {
        if (id == null) return NotFound();
        var location = _context.Location.Find(id);
        if (location == null) return NotFound();
        return View(location);
    }

    // POST: Location/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("LocationID,Name,Address")] Location location) {
        if (id != location.LocationID) return NotFound();
        try {
            _context.Update(location);
            _context.SaveChanges();
        } catch (DbUpdateConcurrencyException) {
            if (!LocationExists(location.LocationID)) return NotFound();
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Location/Delete/5
    public IActionResult Delete(int? id) {
        if (id == null) return NotFound();
        var location = _context.Location.FirstOrDefault(m => m.LocationID == id);
        if (location == null) return NotFound();
        return View(location);
    }

    // POST: Location/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id) {
        var location = _context.Location.Find(id);
        _context.Location.Remove(location);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    private bool LocationExists(int id) {
        return _context.Location.Any(e => e.LocationID == id);
    }
    
}
