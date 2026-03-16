using System.ComponentModel.DataAnnotations;

namespace DevTrack.Web.Models;

// simple category bucket for projects + skills
public class Category
{
    public int Id { get; set; }

    [Required]
    [StringLength(80)]
    public string Name { get; set; } = string.Empty;

    [StringLength(250)]
    public string? Description { get; set; }

    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<Skill> Skills { get; set; } = new List<Skill>();
}