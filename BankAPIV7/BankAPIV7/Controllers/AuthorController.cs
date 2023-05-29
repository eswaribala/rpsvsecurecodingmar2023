using BankAPIV7.Models;
using BankAPIV7.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAPIV7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService authorService;
        public IList<Author> AuthorList { get; set; }

        public AuthorController(IAuthorService authorService)
        {
            this.authorService = authorService;
            this.AuthorList = new List<Author>();
        }


        [HttpGet("search")]
        public IActionResult Search(string input)
        {

            this.AuthorList = this.authorService.Search(input);
            return new ObjectResult(this.AuthorList);
        }
    }
}
