using DevTrack.Web.Data;
using DevTrack.Web.Models;
using DevTrack.Web.Services;
using DevTrack.Web.ViewModels.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DevTrack.Web.Controllers;

// main crud controller for student projects
[Authorize]
public class ProjectsController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly IGitHubRepoService _gitHubRepoService;

    public ProjectsController(ApplicationDbContext db, IGitHubRepoService gitHubRepoService)
    {
        _db = db;
        _gitHubRepoService = gitHubRepoService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        var isAdmin = User.IsInRole("Admin");

        var projectQuery = _db.Projects
            .AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.ProjectSkills)
                .ThenInclude(ps => ps.Skill)
            .AsQueryable();

        if (!isAdmin && !string.IsNullOrWhiteSpace(userId))
        {
            projectQuery = projectQuery.Where(p => p.OwnerId == userId);
        }

        var vm = new ProjectsIndexViewModel
        {
            Projects = await projectQuery
                .OrderByDescending(p => p.UpdatedAtUtc ?? p.CreatedAtUtc)
                .ToListAsync(),
            Skills = await _db.Skills.AsNoTracking().Include(s => s.Category).OrderBy(s => s.Name).ToListAsync()
        };

        return View(vm);
    }

    public async Task<IActionResult> Create()
    {
        var vm = new ProjectFormViewModel();
        await LoadFormListsAsync(vm);
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProjectFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await LoadFormListsAsync(model);
            return View(model);
        }

        var userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Challenge();
        }

        var project = new Project
        {
            Title = model.Title,
            Description = model.Description,
            Status = model.Status,
            GitHubRepoUrl = model.GitHubRepoUrl,
            CategoryId = model.CategoryId,
            OwnerId = userId,
            CreatedAtUtc = DateTime.UtcNow,
            UpdatedAtUtc = DateTime.UtcNow
        };

        // try to pull github metadata if url looks right
        await TryApplyGitHubMetadataAsync(project);

        _db.Projects.Add(project);
        await _db.SaveChangesAsync();

        await ReplaceProjectSkillsAsync(project.Id, model.SelectedSkillIds);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var project = await _db.Projects
            .Include(p => p.ProjectSkills)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (project is null)
        {
            return NotFound();
        }

        if (!CanAccessProject(project))
        {
            return Forbid();
        }

        var vm = new ProjectFormViewModel
        {
            Id = project.Id,
            Title = project.Title,
            Description = project.Description,
            Status = project.Status,
            GitHubRepoUrl = project.GitHubRepoUrl,
            CategoryId = project.CategoryId,
            SelectedSkillIds = project.ProjectSkills.Select(ps => ps.SkillId).ToList()
        };

        await LoadFormListsAsync(vm);
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProjectFormViewModel model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        var project = await _db.Projects.FirstOrDefaultAsync(p => p.Id == id);
        if (project is null)
        {
            return NotFound();
        }

        if (!CanAccessProject(project))
        {
            return Forbid();
        }

        if (!ModelState.IsValid)
        {
            await LoadFormListsAsync(model);
            return View(model);
        }

        project.Title = model.Title;
        project.Description = model.Description;
        project.Status = model.Status;
        project.GitHubRepoUrl = model.GitHubRepoUrl;
        project.CategoryId = model.CategoryId;
        project.UpdatedAtUtc = DateTime.UtcNow;

        await TryApplyGitHubMetadataAsync(project);
        await ReplaceProjectSkillsAsync(project.Id, model.SelectedSkillIds);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var project = await _db.Projects.FirstOrDefaultAsync(p => p.Id == id);
        if (project is null)
        {
            return NotFound();
        }

        if (!CanAccessProject(project))
        {
            return Forbid();
        }

        _db.Projects.Remove(project);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddSkill(string name, int? categoryId)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return RedirectToAction(nameof(Index));
        }

        var exists = await _db.Skills.AnyAsync(s => s.Name.ToLower() == name.Trim().ToLower());
        if (!exists)
        {
            _db.Skills.Add(new Skill
            {
                Name = name.Trim(),
                CategoryId = categoryId
            });
            await _db.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool CanAccessProject(Project project)
    {
        if (User.IsInRole("Admin"))
        {
            return true;
        }

        var userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        return !string.IsNullOrWhiteSpace(userId) && project.OwnerId == userId;
    }

    private async Task LoadFormListsAsync(ProjectFormViewModel vm)
    {
        vm.Categories = await _db.Categories
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .Select(c => new SelectListItem(c.Name, c.Id.ToString()))
            .ToListAsync();

        vm.Skills = await _db.Skills
            .AsNoTracking()
            .OrderBy(s => s.Name)
            .Select(s => new SelectListItem(s.Name, s.Id.ToString()))
            .ToListAsync();
    }

    private async Task ReplaceProjectSkillsAsync(int projectId, IEnumerable<int> selectedSkillIds)
    {
        var existing = await _db.ProjectSkills.Where(ps => ps.ProjectId == projectId).ToListAsync();
        _db.ProjectSkills.RemoveRange(existing);

        var newRows = selectedSkillIds
            .Distinct()
            .Select(skillId => new ProjectSkill { ProjectId = projectId, SkillId = skillId });

        await _db.ProjectSkills.AddRangeAsync(newRows);
    }

    private async Task TryApplyGitHubMetadataAsync(Project project)
    {
        var parsed = TryParseGitHubRepo(project.GitHubRepoUrl);
        if (parsed is null)
            return;

        try
        {
            var info = await _gitHubRepoService.GetRepoInfoAsync(parsed.Value.owner, parsed.Value.repo);
            if (info is null)
                return;

            project.GitHubRepoName = info.Name;
            project.GitHubLastSyncedUtc = DateTime.UtcNow;

            if (string.IsNullOrWhiteSpace(project.Description) && !string.IsNullOrWhiteSpace(info.Description))
                project.Description = info.Description;
        }
        catch (HttpRequestException)
        {
            // GitHub API unavailable or rate-limited — save the project without metadata
        }
    }

    private static (string owner, string repo)? TryParseGitHubRepo(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            return null;
        }

        if (!Uri.TryCreate(url.Trim(), UriKind.Absolute, out var uri))
        {
            return null;
        }

        if (!uri.Host.Contains("github.com", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        var parts = uri.AbsolutePath.Trim('/').Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 2)
        {
            return null;
        }

        var owner = parts[0];
        var repo = parts[1].Replace(".git", string.Empty, StringComparison.OrdinalIgnoreCase);
        return (owner, repo);
    }
}