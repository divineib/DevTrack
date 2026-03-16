using DevTrack.Web.Models;

namespace DevTrack.Web.ViewModels.Projects;

// list page model for projects screen
public class ProjectsIndexViewModel
{
    public List<Project> Projects { get; set; } = new();
    public List<Skill> Skills { get; set; } = new();
}