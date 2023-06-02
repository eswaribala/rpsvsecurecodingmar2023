using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Octokit;
using Octokit.Internal;

namespace GithubOauth2App.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public IReadOnlyList<Repository> Repositories { get; set; }

        public IReadOnlyList<Repository> StarredRepos { get; set; }

        public IReadOnlyList<User> Followers { get; set; }

        public IReadOnlyList<User> Following { get; set; }
        public string? AccessToken { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                AccessToken = await HttpContext.GetTokenAsync("access_token");
                var github = new GitHubClient(new ProductHeaderValue("AspNetCoreGitHubAuth"), new InMemoryCredentialStore(new Credentials(AccessToken)));
                Repositories = await github.Repository.GetAllForCurrent();

                StarredRepos = await github.Activity.Starring.GetAllForCurrent();

                Followers = await github.User.Followers.GetAllForCurrent();
                Following = await github.User.Followers.GetAllFollowingForCurrent();
            }
        }
    }
}