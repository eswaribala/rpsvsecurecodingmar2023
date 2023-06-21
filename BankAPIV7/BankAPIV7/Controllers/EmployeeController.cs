using BankAPIV7.Models;
using BankAPIV7.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankAPIV7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IEmployeeService employeeService;
        public IList<Employee> EmployeeList { get; set; }

        public EmployeeController(IEmployeeService employeeService, IHttpClientFactory httpClientFactory)
        {
            this.employeeService = employeeService;
            this.EmployeeList = new List<Employee>();
            _httpClientFactory = httpClientFactory;
        }


        [HttpGet("search")]
         public IActionResult Search(string input) {

            //inter service communication
            var httpClient = _httpClientFactory.CreateClient("WeatherClient");

            var response = httpClient.GetAsync("WeatherForecast").Result;
            var Data = response.Content.ReadAsStringAsync().Result.ToString();
            Console.WriteLine(Data);

            this.EmployeeList=this.employeeService.Search(input);
            return new ObjectResult(this.EmployeeList);
        }
    }
}
