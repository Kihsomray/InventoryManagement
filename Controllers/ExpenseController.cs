using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

/// <summary>
/// Controller responsible for managing expenses.
/// </summary>
public class ExpenseController : Controller {
    private readonly InventoryManagementContext _context;

    /// <summary>
    /// Constructor for ExpenseController.
    /// </summary>
    /// <param name="context">Database context for inventory management.</param>
    public ExpenseController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Expense
    /// <summary>
    /// Displays the list of expenses.
    /// </summary>
    /// <returns>The view containing the list of expenses.</returns>
    public IActionResult Index() {
        var expenses = _context.Expense.ToList();
        return View(expenses);
    }

    // GET: Expense/Details/5
    /// <summary>
    /// Displays details of a specific expense.
    /// </summary>
    /// <param name="id">ID of the expense.</param>
    /// <returns>The view containing details of the expense.</returns>
    public IActionResult Details(int? id) {
        if (id == null) return NotFound();
        var expense = _context.Expense.FirstOrDefault(m => m.ExpenseID == id);
        if (expense == null) return NotFound();
        return View(expense);
    }

    // GET: Expense/Create
    /// <summary>
    /// Displays the form to create a new expense.
    /// </summary>
    /// <returns>The view containing the form to create a new expense.</returns>
    public IActionResult Create() {
        ViewBag.Locations = _context.Location.ToList();
        ViewBag.Items = _context.Item.ToList();
        return View();
    }

    // POST: Expense/Create
    /// <summary>
    /// Adds a new expense to the database.
    /// </summary>
    /// <param name="expense">The Expense object to be added.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("ExpenseID, LocationID, ItemID, Date, Quantity, Method, Completed")] Expense expense) {
        _context.Add(expense);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Expense/Edit/5
    /// <summary>
    /// Displays the form to edit an existing expense.
    /// </summary>
    /// <param name="id">ID of the expense.</param>
    /// <returns>The view containing the form to edit an existing expense.</returns>
    public IActionResult Edit(int? id) {
        if (id == null) return NotFound();
        var expense = _context.Expense.Find(id);
        if (expense == null) return NotFound();
        ViewBag.Locations = _context.Location.ToList();
        ViewBag.Items = _context.Item.ToList();
        return View(expense);
    }

    // POST: Expense/Edit/5
    /// <summary>
    /// Updates an existing expense in the database.
    /// </summary>
    /// <param name="id">ID of the expense.</param>
    /// <param name="expense">The Expense object with updated information.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
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
    /// <summary>
    /// Displays the confirmation page to delete an expense.
    /// </summary>
    /// <param name="id">ID of the expense.</param>
    /// <returns>The view containing the confirmation page.</returns>
    public IActionResult Delete(int? id) {
        if (id == null) return NotFound();
        var expense = _context.Expense.FirstOrDefault(m => m.ExpenseID == id);
        if (expense == null) return NotFound();
        return View(expense);
    }

    // POST: Expense/Delete/5
    /// <summary>
    /// Deletes a specific expense from the database.
    /// </summary>
    /// <param name="id">ID of the expense.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id) {
        var expense = _context.Expense.Find(id);
        _context.Expense.Remove(expense);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Checks if a specific expense exists in the database.
    /// </summary>
    /// <param name="id">ID of the expense.</param>
    /// <returns>True if the expense exists; otherwise, false.</returns>
    private bool ExpenseExists(int id) {
        return _context.Expense.Any(e => e.ExpenseID == id);
    }
}
