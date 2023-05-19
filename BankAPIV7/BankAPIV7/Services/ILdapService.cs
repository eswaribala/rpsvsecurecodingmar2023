using BankAPIV7.Models;

namespace BankAPIV7.Services
{
    public interface ILdapService
    {
        User Search(string username);
    }
}
