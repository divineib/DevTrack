namespace DevTrack.Web.Models;

/// <summary>
/// Human-readable labels for <see cref="ProjectStatus"/> in the UI.
/// </summary>
public static class ProjectStatusFormatting
{
    public static string ToDisplayLabel(this ProjectStatus status) => status switch
    {
        ProjectStatus.Planned => "Planned",
        ProjectStatus.InProgress => "In progress",
        ProjectStatus.Completed => "Completed",
        ProjectStatus.OnHold => "On hold",
        _ => status.ToString()
    };
}
