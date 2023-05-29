using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankingAppv7.Pages
{
    public class IndexModel : PageModel
    {
        public const string SessionKeyName = "_Name";
        public const string SessionKeyStatus = "_Status";
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyName)))
            {
                HttpContext.Session.SetString(SessionKeyName, "User");
                HttpContext.Session.SetString(SessionKeyStatus, "Active");
            }
            var name = HttpContext.Session.GetString(SessionKeyName);
            var status = HttpContext.Session.GetString(SessionKeyStatus);

            _logger.LogInformation("Session Name: {Name}", name);
            _logger.LogInformation("Session Status: {Status}", status);
        }
    }
}