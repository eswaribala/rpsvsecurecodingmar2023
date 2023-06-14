
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using RateLimitingBankAPI.Models;
using RateLimitingBankAPI.Services;

namespace BankingAPI.Controllers
{
    
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {

        private ICustomerService customerService;
        private IConfiguration configuration;
        
        public CustomersController(ICustomerService _customerService,IConfiguration _configuration)
        {
            this.customerService = _customerService;
            this.configuration = _configuration;
          
        }

        // GET: api/<CustomersController>

        [HttpGet]
        [MapToApiVersion("2.0")]
        [EnableRateLimiting("fixed")]
        public Task<IEnumerable<Customer>> Get()
        {
           
            return this.customerService.GetAllCustomers();
        }

        // GET api/<CustomersController>/5
        [HttpGet("{CustomerId}")]
        public Task<Customer> Get(long CustomerId)
        {
            return this.customerService.GetCustomerById(CustomerId);
        }

        // POST api/<CustomersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            await this.customerService.AddCustomer(customer);
            return CreatedAtAction(nameof(Get),
                           new { id = customer.CustomerId }, customer);
        }





        // PUT api/<CustomersController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Customer customer)
        {
            await this.customerService.UpdateCustomer(customer);
            return CreatedAtAction(nameof(Get),
                           new { id = customer.CustomerId }, customer);
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{CustomerId}")]
        public async Task<IActionResult> Delete(long CustomerId)
        {

            if (await this.customerService.DeleteCustomer(CustomerId))
                return new OkResult();
            else
                return new BadRequestResult();

        }
    }
    }
