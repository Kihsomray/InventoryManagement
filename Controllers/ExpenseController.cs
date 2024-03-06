using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

public class ExpenseController : Controller {
    private readonly InventoryManagementContext _context;

    public ExpenseController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Expense
    public IActionResult Index() {
        var expenses = _context.Expense.ToList();
        return View(expenses);
    }

    // GET: Expense/Details/5
    public IActionResult Details(int? id) {
        if (id == null) return NotFound();
        var expense = _context.Expense.FirstOrDefault(m => m.ExpenseID == id);
        if (expense == null) return NotFound();
        return View(expense);
    }

    // GET: Expense/Create
    public IActionResult Create() {
        ViewBag.Locations = _context.Location.ToList();
        ViewBag.Items = _context.Item.ToList();
        return View();
    }

    // POST: Expense/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("ExpenseID, LocationID, ItemID, Date, Quantity, Method, Completed")] Expense expense) {
        _context.Add(expense);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Expense/Edit/5
    public IActionResult Edit(int? id) {
        if (id == null) return NotFound();
        var expense = _context.Expense.Find(id);
        if (expense == null) return NotFound();
        ViewBag.Locations = _context.Location.ToList();
        ViewBag.Items = _context.Item.ToList();
        return View(expense);
    }

    // POST: Expense/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("ExpenseID, LocationID, ItemID, Date, Quantity, Method, Completed")] Expense expense) {
        if (id != expense.ExpenseID) return NotFound();
        try {
            _context.Update(expense);
            _context.SaveChanges();
        } catch (DbUpdateConcurrencyException) {
            if (!ExpenseExists(expense.ExpenseID)) return NotFound();
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Expense/Delete/5
    public IActionResult Delete(int? id) {
        if (id == null) return NotFound();
        var expense = _context.Expense.FirstOrDefault(m => m.ExpenseID == id);
        if (expense == null) return NotFound();
        return View(expense);
    }

    // POST: Expense/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id) {
        var expense = _context.Expense.Find(id);
        _context.Expense.Remove(expense);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    private bool ExpenseExists(int id) {
        return _context.Expense.Any(e => e.ExpenseID == id);
    }
}
