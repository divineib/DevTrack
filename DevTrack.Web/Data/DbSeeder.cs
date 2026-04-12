using DevTrack.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DevTrack.Web.Data;

public static class DbSeeder
{
    public static async Task SeedCoreDataAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

        await db.Database.MigrateAsync();

        await MigrateLegacyCasingAsync(db);

        var roles = new[] { "Student", "Admin" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        if (!await db.Categories.AnyAsync())
        {
            db.Categories.AddRange(
                new Category { Name = "Web development", Description = "MVC, UI, and full-stack app work." },
                new Category { Name = "Backend", Description = "APIs, data, and services." },
                new Category { Name = "Cloud", Description = "Deployment and cloud tools." }
            );
            await db.SaveChangesAsync();
        }

        if (!await db.Skills.AnyAsync())
        {
            var webCat = await db.Categories.FirstAsync(c => c.Name == "Web development");
            var backendCat = await db.Categories.FirstAsync(c => c.Name == "Backend");
            var cloudCat = await db.Categories.FirstAsync(c => c.Name == "Cloud");

            db.Skills.AddRange(
                new Skill { Name = "ASP.NET Core MVC", Description = "Server-side MVC app building.", CategoryId = webCat.Id },
                new Skill { Name = "Entity Framework Core", Description = "Data access and migrations.", CategoryId = backendCat.Id },
                new Skill { Name = "SQLite", Description = "Lightweight relational database.", CategoryId = backendCat.Id },
                new Skill { Name = "GitHub API", Description = "Public repository metadata integration.", CategoryId = webCat.Id },
                new Skill { Name = "Razor views", Description = "HTML + C# templating.", CategoryId = webCat.Id },
                new Skill { Name = "CSS / responsive design", Description = "Custom layout with media queries.", CategoryId = webCat.Id },
                new Skill { Name = "Docker", Description = "Containerized deployment.", CategoryId = cloudCat.Id },
                new Skill { Name = "JavaScript", Description = "Client-side interactivity.", CategoryId = webCat.Id }
            );
            await db.SaveChangesAsync();
        }

        await SeedUsersAndProjectsAsync(db, userManager);
    }

    /// <summary>
    /// One-time casing fixes for databases seeded before display names were title-cased.
    /// </summary>
    private static async Task MigrateLegacyCasingAsync(ApplicationDbContext db)
    {
        var categoryRenames = new Dictionary<string, string>(StringComparer.Ordinal)
        {
            ["web development"] = "Web development",
            ["backend"] = "Backend",
            ["cloud"] = "Cloud",
        };

        foreach (var cat in await db.Categories.ToListAsync())
        {
            if (categoryRenames.TryGetValue(cat.Name, out var newName))
                cat.Name = newName;
        }

        var skillRenames = new Dictionary<string, string>(StringComparer.Ordinal)
        {
            ["asp.net core mvc"] = "ASP.NET Core MVC",
            ["entity framework core"] = "Entity Framework Core",
            ["sqlite"] = "SQLite",
            ["github api"] = "GitHub API",
            ["razor views"] = "Razor views",
            ["css / responsive design"] = "CSS / responsive design",
            ["docker"] = "Docker",
            ["javascript"] = "JavaScript",
        };

        foreach (var skill in await db.Skills.ToListAsync())
        {
            if (skillRenames.TryGetValue(skill.Name, out var newName))
                skill.Name = newName;
        }

        if (db.ChangeTracker.HasChanges())
            await db.SaveChangesAsync();
    }

    private static async Task SeedUsersAndProjectsAsync(ApplicationDbContext db, UserManager<AppUser> userManager)
    {
        const string adminEmail = "admin@devtrack.local";
        const string studentEmail = "student@devtrack.local";
        const string seedPassword = "DevTrack1";

        if (await userManager.FindByEmailAsync(adminEmail) is null)
        {
            var admin = new AppUser { UserName = adminEmail, Email = adminEmail, DisplayName = "Admin User" };
            var result = await userManager.CreateAsync(admin, seedPassword);
            if (result.Succeeded)
                await userManager.AddToRoleAsync(admin, "Admin");
        }

        var studentUser = await userManager.FindByEmailAsync(studentEmail);
        if (studentUser is null)
        {
            studentUser = new AppUser { UserName = studentEmail, Email = studentEmail, DisplayName = "Demo Student" };
            var result = await userManager.CreateAsync(studentUser, seedPassword);
            if (result.Succeeded)
                await userManager.AddToRoleAsync(studentUser, "Student");
        }

        if (studentUser is not null && !await db.Projects.AnyAsync())
        {
            var webCat = await db.Categories.FirstAsync(c => c.Name == "Web development");
            var backendCat = await db.Categories.FirstAsync(c => c.Name == "Backend");

            var skills = await db.Skills.ToListAsync();
            var mvcSkill = skills.First(s => s.Name == "ASP.NET Core MVC");
            var efSkill = skills.First(s => s.Name == "Entity Framework Core");
            var sqliteSkill = skills.First(s => s.Name == "SQLite");
            var githubSkill = skills.First(s => s.Name == "GitHub API");
            var razorSkill = skills.First(s => s.Name == "Razor views");
            var cssSkill = skills.First(s => s.Name == "CSS / responsive design");
            var jsSkill = skills.First(s => s.Name == "JavaScript");

            var projects = new List<Project>
            {
                new()
                {
                    Title = "DevTrack Portfolio System",
                    Description = "Full-stack MVC application for tracking student developer projects, skills, and portfolio progress.",
                    Status = ProjectStatus.InProgress,
                    OwnerId = studentUser.Id,
                    CategoryId = webCat.Id,
                    GitHubRepoUrl = "https://github.com/octocat/Hello-World",
                    GitHubRepoName = "Hello-World",
                    GitHubLastSyncedUtc = DateTime.UtcNow,
                    CreatedAtUtc = DateTime.UtcNow.AddDays(-30),
                    UpdatedAtUtc = DateTime.UtcNow
                },
                new()
                {
                    Title = "Campus Event API",
                    Description = "RESTful Web API for campus event scheduling with JWT authentication and filtering.",
                    Status = ProjectStatus.Planned,
                    OwnerId = studentUser.Id,
                    CategoryId = backendCat.Id,
                    CreatedAtUtc = DateTime.UtcNow.AddDays(-14),
                    UpdatedAtUtc = DateTime.UtcNow.AddDays(-7)
                },
                new()
                {
                    Title = "Algorithm Practice Tracker",
                    Description = "Dashboard for logging daily algorithm practice sessions with progress analytics.",
                    Status = ProjectStatus.Completed,
                    OwnerId = studentUser.Id,
                    CategoryId = backendCat.Id,
                    CreatedAtUtc = DateTime.UtcNow.AddDays(-60),
                    UpdatedAtUtc = DateTime.UtcNow.AddDays(-20)
                }
            };

            db.Projects.AddRange(projects);
            await db.SaveChangesAsync();

            db.ProjectSkills.AddRange(
                new ProjectSkill { ProjectId = projects[0].Id, SkillId = mvcSkill.Id },
                new ProjectSkill { ProjectId = projects[0].Id, SkillId = efSkill.Id },
                new ProjectSkill { ProjectId = projects[0].Id, SkillId = sqliteSkill.Id },
                new ProjectSkill { ProjectId = projects[0].Id, SkillId = githubSkill.Id },
                new ProjectSkill { ProjectId = projects[0].Id, SkillId = razorSkill.Id },
                new ProjectSkill { ProjectId = projects[0].Id, SkillId = cssSkill.Id },
                new ProjectSkill { ProjectId = projects[0].Id, SkillId = jsSkill.Id },
                new ProjectSkill { ProjectId = projects[1].Id, SkillId = mvcSkill.Id },
                new ProjectSkill { ProjectId = projects[1].Id, SkillId = efSkill.Id },
                new ProjectSkill { ProjectId = projects[2].Id, SkillId = sqliteSkill.Id },
                new ProjectSkill { ProjectId = projects[2].Id, SkillId = efSkill.Id }
            );

            var adminUser = await db.Users.FirstAsync(u => u.Email == adminEmail);
            db.Reviews.AddRange(
                new Review
                {
                    ProjectId = projects[0].Id,
                    ReviewerId = adminUser.Id,
                    Decision = "approved",
                    Notes = "Strong MVC structure with clean separation of concerns.",
                    ReviewedAtUtc = DateTime.UtcNow.AddDays(-2)
                },
                new Review
                {
                    ProjectId = projects[2].Id,
                    ReviewerId = adminUser.Id,
                    Decision = "approved",
                    Notes = "Good use of EF Core queries and data visualization.",
                    ReviewedAtUtc = DateTime.UtcNow.AddDays(-5)
                },
                new Review
                {
                    ProjectId = projects[1].Id,
                    ReviewerId = adminUser.Id,
                    Decision = "pending",
                    Notes = "Waiting for initial commit and project setup.",
                    ReviewedAtUtc = DateTime.UtcNow.AddDays(-1)
                }
            );

            await db.SaveChangesAsync();
        }
    }
}