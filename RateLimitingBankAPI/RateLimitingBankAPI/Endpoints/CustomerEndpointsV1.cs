using RateLimitingBankAPI.Models;
using RateLimitingBankAPI.Services;

namespace RateLimitingBankAPI.Endpoints
{
    public static class CustomerEndpointsV1
    {
        public static RouteGroupBuilder MapCustomersApiV1(this RouteGroupBuilder group)
        {
            // Map to NBomber GlobalScenario
            group.MapGet("/", GetAllCustomers).RequireRateLimiting(Policy.FixedWindowPolicy.ToString());

            // Map to NBomber ConcurrencyScenario
            group.MapGet("/{id}", GetCustomer);
            group.MapPost("/", CreateCustomer);
               

            group.MapPut("/{id}", UpdateCustomer);
            group.MapDelete("/{id}", DeleteCustomer);

            return group;
        }

        // get all
        public static async Task<IResult> GetAllCustomers(ICustomerService CustomerService)
        {
            var customers = await CustomerService.GetAllCustomers();
            return TypedResults.Ok(customers);

        }

        // get by id
        public static async Task<IResult> GetCustomer(int id, ICustomerService CustomerService)
        {
            var customer = await CustomerService.GetCustomerById(id);

            if (customer != null)
            {
                return TypedResults.Ok(customer);
            }

            return TypedResults.NotFound();
        }

        // create
        public static async Task<IResult> CreateCustomer(Customer customer, ICustomerService CustomerService )
        {
            var newCustomer = new Customer
            {
                CustomerId=customer.CustomerId,
                ContactNo=customer.ContactNo,
                Email=customer.Email,
                FirstName= customer.FirstName,
                LastName= customer.LastName,
                Password = customer.Password

            };

            await CustomerService.AddCustomer(newCustomer);

            return TypedResults.Created($"/customers/v1/{newCustomer.CustomerId}", newCustomer);
        }

        // update
        public static async Task<IResult> UpdateCustomer(Customer customer, ICustomerService CustomerService)
        {
            var existingCustomer = await CustomerService.GetCustomerById(customer.CustomerId);

            if (existingCustomer != null)
            {
                existingCustomer.FirstName=customer.FirstName;
                existingCustomer.LastName=customer.LastName;    
                existingCustomer.Password=customer.Password;
                existingCustomer.Email=customer.Email;
                await CustomerService.UpdateCustomer(existingCustomer);

                return TypedResults.Created($"/customers/v1/{existingCustomer.CustomerId}", existingCustomer);
            }

            return TypedResults.NotFound();
        }

        // delete
        public static async Task<IResult> DeleteCustomer(int CustomerId, ICustomerService CustomerService)
        {
            var customer = await CustomerService.GetCustomerById(CustomerId);

            if (customer != null)
            {
                await CustomerService.DeleteCustomer(CustomerId);
                return TypedResults.NoContent();
            }

            return TypedResults.NotFound();
        }
    }
}
