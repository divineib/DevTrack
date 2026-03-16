using System.Diagnostics;
using DevTrack.Web.Data;
using Microsoft.AspNetCore.Mvc;
using DevTrack.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace DevTrack.Web.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _db;

    public HomeController(ApplicationDbContext db)
    {
        _db = db;
    }

    // Public landing/overview page.
    public async Task<IActionResult> Index()
    {
        // live counts for the landing cards
        ViewBag.StudentCount = await _db.Users.CountAsync();
        ViewBag.ProjectCount = await _db.Projects.CountAsync();
        ViewBag.SkillCount = await _db.Skills.CountAsync();

        return View();
    }

    // Student-focused dashboard shell.
    // student area needs login
    [Authorize]
    public async Task<IActionResult> Dashboard()
    {
        // only show own projects unless admin is checking around
        var userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        var isAdmin = User.IsInRole("Admin");

        var query = _db.Projects.AsNoTracking();
        if (!isAdmin && !string.IsNullOrWhiteSpace(userId))
        {
            query = query.Where(p => p.OwnerId == userId);
        }

        var projects = await query
            .OrderByDescending(p => p.UpdatedAtUtc ?? p.CreatedAtUtc)
            .Take(5)
            .ToListAsync();

        ViewBag.InProgressCount = await query.CountAsync(p => p.Status == ProjectStatus.InProgress);
        ViewBag.CompletedCount = await query.CountAsync(p => p.Status == ProjectStatus.Completed);
        ViewBag.SkillCount = await _db.Skills.CountAsync();
        ViewBag.ProfileScore = projects.Count == 0 ? 0 : Math.Min(100, 55 + (projects.Count * 9));

        return View(projects);
    }

    // Project portfolio listing page.
    // project page needs login
    [Authorize]
    public IActionResult Projects()
    {
        // route old nav/action to the real crud page
        return RedirectToAction("Index", "Projects");
    }

    // Admin/reviewer page for role-specific workflows.
    // admin page is role protected
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Admin()
    {
        // simple moderation snapshots from db
        var pendingReviews = await _db.Reviews.CountAsync(r => r.Decision == "pending");
        var flaggedReviews = await _db.Reviews.CountAsync(r => r.Decision == "flagged");
        var approvedReviews = await _db.Reviews.CountAsync(r => r.Decision == "approved");

        ViewBag.PendingReviews = pendingReviews;
        ViewBag.FlaggedReviews = flaggedReviews;
        ViewBag.ApprovedReviews = approvedReviews;
        ViewBag.RoleHealth = "stable";

        var feed = await _db.Reviews
            .AsNoTracking()
            .OrderByDescending(r => r.ReviewedAtUtc)
            .Take(6)
            .Select(r => new { r.Decision, r.Notes, r.ReviewedAtUtc })
            .ToListAsync();

        return View(feed);
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
