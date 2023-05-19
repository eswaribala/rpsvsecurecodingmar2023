using BankAPIV7.Models;
using BankAPIV7.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankAPIV7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeService employeeService;
        public IList<Employee> EmployeeList { get; set; }

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
            this.EmployeeList = new List<Employee>();
        }


        [HttpGet("search")]
         public IActionResult Search(string input) { 
        
           this.EmployeeList=this.employeeService.Search(input);
            return new ObjectResult(this.EmployeeList);
        }
    }
}
