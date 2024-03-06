using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

public class ReturnController : Controller {
    private readonly InventoryManagementContext _context;

    public ReturnController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Return
    public IActionResult Index() {
        var returns = _context.Return.ToList();
        return View(returns);
    }

    // GET: Return/Details/5
    public IActionResult Details(int? orderId) {
        if (orderId == null) return NotFound();
        var returnItem = _context.Return.FirstOrDefault(r => r.OrderID == orderId);
        if (returnItem == null) return NotFound();
        return View(returnItem);
    }

    // GET: Return/Create
    public IActionResult Create() {
        return View();
    }

    // POST: Return/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("OrderID, ReturnDate, ReturnReason")] Return returnItem) {
        _context.Add(returnItem);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Return/Edit/5
    public IActionResult Edit(int? orderId) {
        if (orderId == null) return NotFound();
        var returnItem = _context.Return.Find(orderId);
        if (returnItem == null) return NotFound();
        return View(returnItem);
    }

    // POST: Return/Edit/5
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
    public IActionResult Delete(int? orderId) {
        if (orderId == null) return NotFound();
        var returnItem = _context.Return.FirstOrDefault(r => r.OrderID == orderId);
        if (returnItem == null) return NotFound();
        return View(returnItem);
    }

    // POST: Return/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int orderId) {
        var returnItem = _context.Return.Find(orderId);
        _context.Return.Remove(returnItem);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    private bool ReturnExists(int orderId) {
        return _context.Return.Any(r => r.OrderID == orderId);
    }
}
