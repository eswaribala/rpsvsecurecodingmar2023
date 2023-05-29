using BankAPIV7.Models;

namespace BankAPIV7.Services
{
    public interface IAuthorService
    {
        List<Author> Search(string input);
    }
}
