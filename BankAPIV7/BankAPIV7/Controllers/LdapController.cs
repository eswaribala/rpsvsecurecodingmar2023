using BankAPIV7.Models;
using BankAPIV7.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAPIV7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LdapController : ControllerBase
    {
        private readonly ILdapService ldapService;
       

        public LdapController(ILdapService ldapService)
        {
            this.ldapService = ldapService;
           
        }


        [HttpGet("search")]
        public IActionResult Search(string input)
        {

            var user = this.ldapService.Search(input);
            return new ObjectResult(user);
        }
    }
}
