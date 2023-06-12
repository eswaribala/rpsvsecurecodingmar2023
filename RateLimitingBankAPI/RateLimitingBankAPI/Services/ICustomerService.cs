using RateLimitingBankAPI.Models;

namespace RateLimitingBankAPI.Services
{
    public interface ICustomerService
    {

        Task<Customer> AddCustomer(Customer Customer);
        Task<Customer> UpdateCustomer(Customer customer);
        Task<bool> DeleteCustomer(long CustomerId);
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerById(long CustomerId);

    }
}
