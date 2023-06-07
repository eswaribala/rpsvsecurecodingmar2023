using Microsoft.AspNetCore.Mvc;

namespace BankingAntiForgeryTokenApp.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Iframe"] = @"<iframe name='hdfcbank' id='hdfcbank' 
width='200' height='200' src='https://www.youtube.com/' ></iframe>";
            return RedirectToPage("/Home/Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string userName, string password)
        {
            ViewBag.Name = string.Format("Name: {0} {1}", userName, password);
            return RedirectToPage("/Home/Index");
        }
    }
}
