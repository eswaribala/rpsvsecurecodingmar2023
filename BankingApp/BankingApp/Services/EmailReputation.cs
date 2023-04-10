using BankingApp.Models;

using System.Net;
using System.Text.Json;

namespace BankingApp.Services
{
    public class EmailReputation : IEmailReputation
    {
      
        private readonly IConfiguration Configuration;
        public EmailReputation(IConfiguration config)
        {
            Configuration = config;
        }

        public bool IsRisky(string email)
        {
            var emailRepApiKey = Configuration["EmailRepApiKey"];
            HttpWebRequest repEmailRequest = (HttpWebRequest)WebRequest.Create($"https://api.apilayer.com/email_verification/" + email);
            repEmailRequest.Headers.Add("apikey", $"{emailRepApiKey}");
            // repEmailRequest.Headers.Add("User-Agent", "MyAppName");
            HttpWebResponse repEmailResponse = (HttpWebResponse)repEmailRequest.GetResponse();

            Stream newStream = repEmailResponse.GetResponseStream();
            var repEmail = new StreamReader(newStream).ReadToEnd();
            var reputation = JsonSerializer.Deserialize<Reputation>(repEmail);



#pragma warning disable CS8602 // Dereference of a possibly null reference.
            if (reputation.is_deliverable)
                return false;
            else
                return true;
#pragma warning restore CS8602 // Dereference of a possibly null reference.


        }
    
    }
}
