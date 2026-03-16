using System.ComponentModel.DataAnnotations;

namespace DevTrack.Web.Models;

// admin/instructor review log for project moderation
public class Review
{
    public int Id { get; set; }

    [Required]
    public int ProjectId { get; set; }
    public Project? Project { get; set; }

    [Required]
    public string ReviewerId { get; set; } = string.Empty;
    public AppUser? Reviewer { get; set; }

    [Required]
    [StringLength(120)]
    public string Decision { get; set; } = "pending";

    [StringLength(1000)]
    public string? Notes { get; set; }

    public DateTime ReviewedAtUtc { get; set; } = DateTime.UtcNow;
}