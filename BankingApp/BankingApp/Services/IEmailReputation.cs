namespace BankingApp.Services
{
    public interface IEmailReputation
    {
        bool IsRisky(string Email);
    }
}
