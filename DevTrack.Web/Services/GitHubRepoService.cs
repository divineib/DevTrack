using System.Net.Http.Json;

namespace DevTrack.Web.Services;

// lightweight github client for repo metadata lookup
public class GitHubRepoService : IGitHubRepoService
{
    private readonly HttpClient _httpClient;

    public GitHubRepoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GitHubRepoInfo?> GetRepoInfoAsync(string owner, string repo, CancellationToken cancellationToken = default)
    {
        var result = await _httpClient.GetFromJsonAsync<GitHubApiResponse>($"repos/{owner}/{repo}", cancellationToken);
        if (result is null)
        {
            return null;
        }

        return new GitHubRepoInfo
        {
            Name = result.name ?? string.Empty,
            Description = result.description,
            HtmlUrl = result.html_url ?? string.Empty,
            UpdatedAt = result.updated_at,
            StargazersCount = result.stargazers_count
        };
    }

    // just map what we need from github's big response
    private sealed class GitHubApiResponse
    {
        public string? name { get; set; }
        public string? description { get; set; }
        public string? html_url { get; set; }
        public DateTimeOffset? updated_at { get; set; }
        public int stargazers_count { get; set; }
    }
}