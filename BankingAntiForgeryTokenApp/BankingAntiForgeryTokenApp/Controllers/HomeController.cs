using Microsoft.AspNetCore.Mvc;

namespace BankingAntiForgeryTokenApp.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string userName, string password)
        {
            ViewBag.Name = string.Format("Name: {0} {1}", userName, password);
            return RedirectToPage("/Index");
        }
    }
}
