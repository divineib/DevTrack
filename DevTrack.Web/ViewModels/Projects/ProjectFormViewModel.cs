using DevTrack.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevTrack.Web.ViewModels.Projects;

// form model used by create and edit project screens
public class ProjectFormViewModel
{
    public int? Id { get; set; }

    [Required]
    [StringLength(120)]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    [Required]
    public ProjectStatus Status { get; set; } = ProjectStatus.Planned;

    [Url]
    [Display(Name = "github repo url")]
    public string? GitHubRepoUrl { get; set; }

    [Display(Name = "category")]
    public int? CategoryId { get; set; }

    [Display(Name = "skills")]
    public List<int> SelectedSkillIds { get; set; } = new();

    // dropdown + checklist data
    public List<SelectListItem> Categories { get; set; } = new();
    public List<SelectListItem> Skills { get; set; } = new();
}