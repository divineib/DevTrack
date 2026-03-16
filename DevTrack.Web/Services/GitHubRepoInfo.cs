namespace DevTrack.Web.Services;

// tiny dto for the github fields we care about
public class GitHubRepoInfo
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string HtmlUrl { get; set; } = string.Empty;
    public DateTimeOffset? UpdatedAt { get; set; }
    public int StargazersCount { get; set; }
}