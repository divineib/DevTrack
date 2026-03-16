using System.ComponentModel.DataAnnotations;

namespace DevTrack.Web.Models;

// main portfolio project record for each student
public class Project
{
    public int Id { get; set; }

    [Required]
    [StringLength(120)]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    [Required]
    public ProjectStatus Status { get; set; } = ProjectStatus.Planned;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAtUtc { get; set; }

    [StringLength(200)]
    public string? GitHubRepoUrl { get; set; }

    [StringLength(120)]
    public string? GitHubRepoName { get; set; }

    public DateTime? GitHubLastSyncedUtc { get; set; }

    public string OwnerId { get; set; } = string.Empty;
    public AppUser? Owner { get; set; }

    public int? CategoryId { get; set; }
    public Category? Category { get; set; }

    public ICollection<ProjectSkill> ProjectSkills { get; set; } = new List<ProjectSkill>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}