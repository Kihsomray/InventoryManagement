using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;

/// <summary>
/// Controller responsible for managing locations.
/// </summary>
public class LocationController : Controller
{
    private readonly InventoryManagementContext _context;

    /// <summary>
    /// Constructor for LocationController.
    /// </summary>
    /// <param name="context">Database context for location management.</param>
    public LocationController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Location
    /// <summary>
    /// Displays the list of locations.
    /// </summary>
    /// <returns>The view containing the list of locations.</returns>
    public IActionResult Index() {
        var locations = _context.Location.ToList();
        return View(locations);
    }

    // GET: Location/Details/5
    /// <summary>
    /// Displays details of a specific location, including associated employees and inventory items.
    /// </summary>
    /// <param name="id">ID of the location.</param>
    /// <returns>The view containing details of the location.</returns>
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
    /// <summary>
    /// Displays the form to create a new location.
    /// </summary>
    /// <returns>The view containing the form to create a new location.</returns>
    public IActionResult Create() {
        return View();
    }

    // POST: Location/Create
    /// <summary>
    /// Adds a new location to the database.
    /// </summary>
    /// <param name="location">The Location object to be added.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("LocationID, Name, Address")] Location location) {
        _context.Add(location);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Location/Edit/5
    /// <summary>
    /// Displays the form to edit an existing location.
    /// </summary>
    /// <param name="id">ID of the location.</param>
    /// <returns>The view containing the form to edit an existing location.</returns>
    public IActionResult Edit(int? id) {
        if (id == null) return NotFound();
        var location = _context.Location.Find(id);
        if (location == null) return NotFound();
        return View(location);
    }

    // POST: Location/Edit/5
    /// <summary>
    /// Updates an existing location in the database.
    /// </summary>
    /// <param name="id">ID of the location.</param>
    /// <param name="location">The Location object with updated information.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
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
    /// <summary>
    /// Displays the confirmation page to delete a location.
    /// </summary>
    /// <param name="id">ID of the location.</param>
    /// <returns>The view containing the confirmation page.</returns>
    public IActionResult Delete(int? id) {
        if (id == null) return NotFound();
        var location = _context.Location.FirstOrDefault(m => m.LocationID == id);
        if (location == null) return NotFound();
        return View(location);
    }

    // POST: Location/Delete/5
    /// <summary>
    /// Deletes a specified location from the database.
    /// </summary>
    /// <param name="id">ID of the location.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id) {
        var location = _context.Location.Find(id);
        _context.Location.Remove(location);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Checks if a specific location exists.
    /// </summary>
    /// <param name="id">ID of the location.</param>
    /// <returns>True if the location exists, otherwise false.</returns>
    private bool LocationExists(int id) {
        return _context.Location.Any(e => e.LocationID == id);
    }
    
}
