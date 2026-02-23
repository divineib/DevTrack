using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DevTrack.Web.Models;

namespace DevTrack.Web.Controllers;

public class HomeController : Controller
{
    // Public landing/overview page.
    public IActionResult Index()
    {
        return View();
    }

    // Student-focused dashboard shell.
    // Placeholder for Part III:
    // populate this view from database-backed models.
    public IActionResult Dashboard()
    {
        return View();
    }

    // Project portfolio listing page.
    // Placeholder for Part III:
    // switch from sample list to CRUD-driven project records.
    public IActionResult Projects()
    {
        return View();
    }

    // Admin/reviewer page for role-specific workflows.
    // Placeholder for Part III:
    // protect this route with role-based authorization.
    public IActionResult Admin()
    {
        return View();
    }

    // Project privacy notice page.
    public IActionResult Privacy()
    {
        return View();
    }

    // Standard error handler for unhandled exceptions.
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
