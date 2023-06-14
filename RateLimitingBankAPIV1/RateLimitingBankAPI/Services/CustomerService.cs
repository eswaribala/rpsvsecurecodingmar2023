using RateLimitingBankAPI.Contexts;
using RateLimitingBankAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace RateLimitingBankAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerContext _dbContext;

        //dependecy injection

        public CustomerService(CustomerContext customerContext)
        {
            _dbContext = customerContext;
        }

        public async Task<Customer> AddCustomer(Customer Customer)
        {

            var result = await _dbContext.Customers.AddAsync(Customer);
            await _dbContext.SaveChangesAsync();
            return result.Entity;

        }

        public async Task<bool> DeleteCustomer(long CustomerId)
        {
            var result = await this._dbContext.Customers.FirstOrDefaultAsync(c =>
             c.CustomerId == CustomerId);
            if (result != null)
            {
                this._dbContext.Customers.Remove(result);
                await this._dbContext.SaveChangesAsync();
            }

            result = await GetCustomerById(CustomerId);
            if (result == null)
                return true;
            else
                return false;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await this._dbContext.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomerById(long CustomerId)
        {
            var result = await this._dbContext.Customers.FirstOrDefaultAsync(c =>
            c.CustomerId == CustomerId);
#pragma warning disable CS8603 // Possible null reference return.
            return result;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            var result = await _dbContext.Customers
                 .FirstOrDefaultAsync(c => c.CustomerId == customer.CustomerId);

            if (result != null)
            {
                result.ContactNo = customer.ContactNo;
                result.Email = customer.Email;



                await _dbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
