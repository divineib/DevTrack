using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DevTrack.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace DevTrack.Web.Controllers;

public class HomeController : Controller
{
    // Public landing/overview page.
    public IActionResult Index()
    {
        return View();
    }

    // Student-focused dashboard shell.
    // student area needs login
    [Authorize]
    public IActionResult Dashboard()
    {
        return View();
    }

    // Project portfolio listing page.
    // project page needs login
    [Authorize]
    public IActionResult Projects()
    {
        return View();
    }

    // Admin/reviewer page for role-specific workflows.
    // admin page is role protected
    [Authorize(Roles = "Admin")]
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
