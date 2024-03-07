using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Data;
using InventoryManagement.Models;

/// <summary>
/// Controller responsible for managing order items.
/// </summary>
public class OrderItemController : Controller {
    private readonly InventoryManagementContext _context;

    /// <summary>
    /// Constructor for OrderItemController.
    /// </summary>
    /// <param name="context">Database context for order item management.</param>
    public OrderItemController(InventoryManagementContext context) {
        _context = context;
    }

    // GET: OrderItem
    /// <summary>
    /// Displays the list of order items, including associated order and item details.
    /// </summary>
    /// <returns>The view containing the list of order items.</returns>
    public IActionResult Index() {
        var orderItems = _context.OrderItem.Include(oi => oi.Order).Include(oi => oi.Item).ToList();
        return View(orderItems);
    }

    // GET: OrderItem/Details/5
    /// <summary>
    /// Displays details of a specific order item, including associated order and item details.
    /// </summary>
    /// <param name="orderId">ID of the order.</param>
    /// <param name="itemId">ID of the item.</param>
    /// <returns>The view containing details of the order item.</returns>
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
    /// <summary>
    /// Displays the form to create a new order item.
    /// </summary>
    /// <returns>The view containing the form to create a new order item.</returns>
    public IActionResult Create() {
        ViewBag.Orders = _context.Order.ToList();
        ViewBag.Items = _context.Item.ToList();
        return View();
    }

    // POST: OrderItem/Create
    /// <summary>
    /// Adds a new order item to the database.
    /// </summary>
    /// <param name="orderItem">The OrderItem object to be added.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("OrderID, ItemID, Quantity")] OrderItem orderItem) {
        _context.Add(orderItem);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: OrderItem/Edit/5/1
    /// <summary>
    /// Displays the form to edit an existing order item.
    /// </summary>
    /// <param name="orderId">ID of the order.</param>
    /// <param name="itemId">ID of the item.</param>
    /// <returns>The view containing the form to edit an existing order item.</returns>
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
    /// <summary>
    /// Updates an existing order item in the database.
    /// </summary>
    /// <param name="orderId">ID of the order.</param>
    /// <param name="itemId">ID of the item.</param>
    /// <param name="orderItem">The OrderItem object with updated information.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
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
    /// <summary>
    /// Displays the confirmation page to delete an order item.
    /// </summary>
    /// <param name="orderId">ID of the order.</param>
    /// <param name="itemId">ID of the item.</param>
    /// <returns>The view containing the confirmation page.</returns>
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
    /// <summary>
    /// Deletes a specified order item from the database.
    /// </summary>
    /// <param name="orderId">ID of the order.</param>
    /// <param name="itemId">ID of the item.</param>
    /// <returns>Redirects to the Index action if successful.</returns>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int orderId, int itemId) {
        var orderItem = _context.OrderItem.Find(orderId, itemId);
        _context.OrderItem.Remove(orderItem);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Checks if a specific order item exists.
    /// </summary>
    /// <param name="orderId">ID of the order.</param>
    /// <param name="itemId">ID of the item.</param>
    /// <returns>True if the order item exists, otherwise false.</returns>
    private bool OrderItemExists(int orderId, int itemId) {
        return _context.OrderItem.Any(oi => oi.OrderID == orderId && oi.ItemID == itemId);
    }
}
