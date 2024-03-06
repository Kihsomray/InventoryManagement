using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

public class OrderItemController : Controller {
    private readonly InventoryManagementContext _context;

    public OrderItemController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: OrderItem
    public IActionResult Index() {
        var orderItems = _context.OrderItem.Include(oi => oi.Order).Include(oi => oi.Item).ToList();
        return View(orderItems);
    }

    // GET: OrderItem/Details/5
    public IActionResult Details(int? orderId, int? itemId) {
        if (orderId == null || itemId == null) return NotFound();
        var orderItem = _context.OrderItem
            .Include(oi => oi.Order)
            .Include(oi => oi.Item)
            .FirstOrDefault(oi => oi.OrderID == orderId && oi.ItemID == itemId);
        if (orderItem == null) return NotFound();
        return View(orderItem);
    }

    // GET: OrderItem/Create
    public IActionResult Create() {
        ViewBag.Orders = _context.Order.ToList();
        ViewBag.Items = _context.Item.ToList();
        return View();
    }

    // POST: OrderItem/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("OrderID, ItemID, Quantity")] OrderItem orderItem) {
        _context.Add(orderItem);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: OrderItem/Edit/5/1
    public IActionResult Edit(int? orderId, int? itemId) {
        if (orderId == null || itemId == null) return NotFound();
        var orderItem = _context.OrderItem
            .Include(oi => oi.Order)
            .Include(oi => oi.Item)
            .FirstOrDefault(oi => oi.OrderID == orderId && oi.ItemID == itemId);
        if (orderItem == null) return NotFound();
        ViewBag.Orders = _context.Order.ToList();
        ViewBag.Items = _context.Item.ToList();
        return View(orderItem);
    }

    // POST: OrderItem/Edit/5/1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int orderId, int itemId, [Bind("OrderID, ItemID, Quantity")] OrderItem orderItem) {
        if (orderId != orderItem.OrderID || itemId != orderItem.ItemID) return NotFound();
        try {
            _context.Update(orderItem);
            _context.SaveChanges();
        } catch (DbUpdateConcurrencyException) {
            if (!OrderItemExists(orderItem.OrderID, orderItem.ItemID)) return NotFound();
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: OrderItem/Delete/5/1
    public IActionResult Delete(int? orderId, int? itemId) {
        if (orderId == null || itemId == null) return NotFound();
        var orderItem = _context.OrderItem
            .Include(oi => oi.Order)
            .Include(oi => oi.Item)
            .FirstOrDefault(oi => oi.OrderID == orderId && oi.ItemID == itemId);
        if (orderItem == null) return NotFound();
        return View(orderItem);
    }

    // POST: OrderItem/Delete/5/1
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int orderId, int itemId) {
        var orderItem = _context.OrderItem.Find(orderId, itemId);
        _context.OrderItem.Remove(orderItem);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    private bool OrderItemExists(int orderId, int itemId) {
        return _context.OrderItem.Any(oi => oi.OrderID == orderId && oi.ItemID == itemId);
    }
}
