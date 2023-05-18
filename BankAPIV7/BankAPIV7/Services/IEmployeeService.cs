using BankAPIV7.Models;

namespace BankAPIV7.Services
{
    public interface IEmployeeService
    {
        List<Employee> Search(string input);
    }
}
