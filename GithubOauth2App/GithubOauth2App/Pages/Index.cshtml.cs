using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Octokit;

namespace GithubOauth2App.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

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
            }
        }
    }
}