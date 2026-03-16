using DevTrack.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DevTrack.Web.Data;

// seeds starter rows so the app is not empty in dev
public static class DbSeeder
{
    public static async Task SeedCoreDataAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // make sure schema is there before we seed
        await db.Database.MigrateAsync();

        // seed the two required roles for this class project
        var roles = new[] { "Student", "Admin" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        if (!await db.Categories.AnyAsync())
        {
            var categories = new List<Category>
            {
                new() { Name = "web development", Description = "mvc, ui, and full-stack app work" },
                new() { Name = "backend", Description = "apis, data, and services" },
                new() { Name = "cloud", Description = "deployment and cloud tools" }
            };

            db.Categories.AddRange(categories);
            await db.SaveChangesAsync();
        }

        if (!await db.Skills.AnyAsync())
        {
            var webCategory = await db.Categories.FirstAsync(c => c.Name == "web development");
            var backendCategory = await db.Categories.FirstAsync(c => c.Name == "backend");

            var skills = new List<Skill>
            {
                new() { Name = "asp.net core mvc", Description = "server-side mvc app building", CategoryId = webCategory.Id },
                new() { Name = "entity framework core", Description = "data access and migrations", CategoryId = backendCategory.Id },
                new() { Name = "sqlite", Description = "lightweight relational database", CategoryId = backendCategory.Id },
                new() { Name = "github api", Description = "public repository metadata integration", CategoryId = webCategory.Id }
            };

            db.Skills.AddRange(skills);
            await db.SaveChangesAsync();
        }
    }
}