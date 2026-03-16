using DevTrack.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DevTrack.Web.Data;

// main ef core db context for app + identity
public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<ProjectSkill> ProjectSkills => Set<ProjectSkill>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<StudentProfile> StudentProfiles => Set<StudentProfile>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // project-skill many-to-many via join table
        builder.Entity<ProjectSkill>()
            .HasKey(ps => new { ps.ProjectId, ps.SkillId });

        builder.Entity<ProjectSkill>()
            .HasOne(ps => ps.Project)
            .WithMany(p => p.ProjectSkills)
            .HasForeignKey(ps => ps.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<ProjectSkill>()
            .HasOne(ps => ps.Skill)
            .WithMany(s => s.ProjectSkills)
            .HasForeignKey(ps => ps.SkillId)
            .OnDelete(DeleteBehavior.Cascade);

        // one profile per user
        builder.Entity<StudentProfile>()
            .HasIndex(sp => sp.UserId)
            .IsUnique();
    }
}