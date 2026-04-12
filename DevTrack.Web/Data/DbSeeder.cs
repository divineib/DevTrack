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

        var roles = new[] { "Student", "Admin" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        if (!await db.Categories.AnyAsync())
        {
            db.Categories.AddRange(
                new Category { Name = "web development", Description = "mvc, ui, and full-stack app work" },
                new Category { Name = "backend", Description = "apis, data, and services" },
                new Category { Name = "cloud", Description = "deployment and cloud tools" }
            );
            await db.SaveChangesAsync();
        }

        if (!await db.Skills.AnyAsync())
        {
            var webCat = await db.Categories.FirstAsync(c => c.Name == "web development");
            var backendCat = await db.Categories.FirstAsync(c => c.Name == "backend");
            var cloudCat = await db.Categories.FirstAsync(c => c.Name == "cloud");

            db.Skills.AddRange(
                new Skill { Name = "asp.net core mvc", Description = "server-side mvc app building", CategoryId = webCat.Id },
                new Skill { Name = "entity framework core", Description = "data access and migrations", CategoryId = backendCat.Id },
                new Skill { Name = "sqlite", Description = "lightweight relational database", CategoryId = backendCat.Id },
                new Skill { Name = "github api", Description = "public repository metadata integration", CategoryId = webCat.Id },
                new Skill { Name = "razor views", Description = "html + c# templating engine", CategoryId = webCat.Id },
                new Skill { Name = "css / responsive design", Description = "custom layout with media queries", CategoryId = webCat.Id },
                new Skill { Name = "docker", Description = "containerized deployment", CategoryId = cloudCat.Id },
                new Skill { Name = "javascript", Description = "client-side interactivity", CategoryId = webCat.Id }
            );
            await db.SaveChangesAsync();
        }

        await SeedUsersAndProjectsAsync(db, userManager);
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
            var webCat = await db.Categories.FirstAsync(c => c.Name == "web development");
            var backendCat = await db.Categories.FirstAsync(c => c.Name == "backend");

            var skills = await db.Skills.ToListAsync();
            var mvcSkill = skills.First(s => s.Name == "asp.net core mvc");
            var efSkill = skills.First(s => s.Name == "entity framework core");
            var sqliteSkill = skills.First(s => s.Name == "sqlite");
            var githubSkill = skills.First(s => s.Name == "github api");
            var razorSkill = skills.First(s => s.Name == "razor views");
            var cssSkill = skills.First(s => s.Name == "css / responsive design");
            var jsSkill = skills.First(s => s.Name == "javascript");

            var projects = new List<Project>
            {
                new()
                {
                    Title = "DevTrack Portfolio System",
                    Description = "Full-stack MVC application for tracking student developer projects, skills, and portfolio progress.",
                    Status = ProjectStatus.InProgress,
                    OwnerId = studentUser.Id,
                    CategoryId = webCat.Id,
                    GitHubRepoUrl = "https://github.com/divineib/DevTrack",
                    GitHubRepoName = "DevTrack",
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