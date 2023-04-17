using Microsoft.AspNetCore.Identity;
using BC = BCrypt.Net.BCrypt;
namespace BankingApp.Areas.Identity
{
    public class BCryptPasswordHasher<TUser> : IPasswordHasher<TUser> where TUser : class
    {
        int salt = 12;
        public string HashPassword(TUser user, string password)
        {
            return BC.HashPassword(password, salt);
        }
        public PasswordVerificationResult VerifyHashedPassword(TUser user,
          string hashedPassword, string password)
        {

            if (BC.Verify(password, hashedPassword))
                return PasswordVerificationResult.Success;
            else
                return PasswordVerificationResult.Failed;
        }
        
    }
}
