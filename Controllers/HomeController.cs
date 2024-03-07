using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Models;

namespace InventoryManagement.Controllers;

/// <summary>
/// Controller responsible for managing home-related views and actions.
/// </summary>
public class HomeController : Controller {
    private readonly ILogger<HomeController> _logger;

    /// <summary>
    /// Constructor for HomeController.
    /// </summary>
    /// <param name="logger">Logger instance for logging purposes.</param>
    public HomeController(ILogger<HomeController> logger) {
        _logger = logger;
    }

    /// <summary>
    /// Action method for the home page.
    /// </summary>
    /// <returns>The view for the home page.</returns>
    public IActionResult Index() {
        return View();
    }

    /// <summary>
    /// Action method for the privacy page.
    /// </summary>
    /// <returns>The view for the privacy page.</returns>
    public IActionResult Privacy() {
        return View();
    }

    /// <summary>
    /// Action method for handling errors.
    /// </summary>
    /// <returns>The view for displaying errors.</returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
