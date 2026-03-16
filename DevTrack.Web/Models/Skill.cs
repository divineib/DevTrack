using System.ComponentModel.DataAnnotations;

namespace DevTrack.Web.Models;

// this is a skill item students can tag on projects
public class Skill
{
    public int Id { get; set; }

    [Required]
    [StringLength(80)]
    public string Name { get; set; } = string.Empty;

    [StringLength(250)]
    public string? Description { get; set; }

    public int? CategoryId { get; set; }
    public Category? Category { get; set; }

    public ICollection<ProjectSkill> ProjectSkills { get; set; } = new List<ProjectSkill>();
}