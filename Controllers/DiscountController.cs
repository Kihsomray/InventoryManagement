using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

public class DiscountController : Controller {
    private readonly InventoryManagementContext _context;

    public DiscountController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: Discount
    public IActionResult Index() {
        var discounts = _context.Discount.Include(d => d.Item).ToList();
        return View(discounts);
    }

    // GET: Discount/Details/5
    public IActionResult Details(int? id) {
        if (id == null) return NotFound();
        var discount = _context.Discount.Include(d => d.Item).FirstOrDefault(m => m.ItemID == id);
        if (discount == null) return NotFound();
        return View(discount);
    }

    // GET: Discount/Create
    public IActionResult Create() {
        ViewBag.Items = _context.Item.ToList();
        return View();
    }

    // POST: Discount/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("ItemID, Percentage, StartDate, EndDate, QuantityUsed, UsageLimit")] Discount discount) {
        _context.Add(discount);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: Discount/Edit/5
    public IActionResult Edit(int? id) {
        if (id == null) return NotFound();
        var discount = _context.Discount.Find(id);
        if (discount == null) return NotFound();
        ViewBag.Items = _context.Item.ToList();
        return View(discount);
    }

    // POST: Discount/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("ItemID, Percentage, StartDate, EndDate, QuantityUsed, UsageLimit")] Discount discount) {
        if (id != discount.ItemID) return NotFound();
        try {
            _context.Update(discount);
            _context.SaveChanges();
        } catch (DbUpdateConcurrencyException) {
            if (!DiscountExists(discount.ItemID)) return NotFound();
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Discount/Delete/5
    public IActionResult Delete(int? id) {
        if (id == null) return NotFound();
        var discount = _context.Discount.Include(d => d.Item).FirstOrDefault(m => m.ItemID == id);
        if (discount == null) return NotFound();
        return View(discount);
    }

    // POST: Discount/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id) {
        var discount = _context.Discount.Find(id);
        _context.Discount.Remove(discount);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    private bool DiscountExists(int id) {
        return _context.Discount.Any(e => e.ItemID == id);
    }
    
}
