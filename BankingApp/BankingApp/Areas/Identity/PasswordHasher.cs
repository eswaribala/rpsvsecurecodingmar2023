using BC = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Identity;
using BankingApp.Models;

namespace BankingApp.Areas.Identity
{
    public class PasswordHasher : IPasswordHasher<Customer>
    {
        public string HashPassword(Customer customer, string password)
        {
            return BC.HashPassword(password);
        }

        public PasswordVerificationResult VerifyHashedPassword(Customer customer,
            string hashedPassword, string password)
        {
            if (BC.Verify(password, hashedPassword))
                return PasswordVerificationResult.Success;
            else
                return PasswordVerificationResult.Failed;
        }
    }
   
}
