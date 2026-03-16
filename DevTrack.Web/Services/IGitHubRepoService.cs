namespace DevTrack.Web.Services;

// gets basic github repo info for project sync
public interface IGitHubRepoService
{
    Task<GitHubRepoInfo?> GetRepoInfoAsync(string owner, string repo, CancellationToken cancellationToken = default);
}