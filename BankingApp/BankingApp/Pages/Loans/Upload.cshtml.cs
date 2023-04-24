using BankingApp.Contexts;
using BankingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Security.Claims;

namespace BankingApp.Pages.Loans
{
    public class UploadModel : PageModel
    {
        private IWebHostEnvironment _environment;
        private readonly CustomerContext _context;

        public UploadModel(IWebHostEnvironment environment, CustomerContext context)
        {
            _environment = environment;
            _context = context;
        }

        [BindProperty]
        public IFormFile Upload { get; set; }
        public async Task OnPostAsync()
        {
            Loan emptyLoan = null;
            var file = Path.GetFileName(Upload.FileName);

            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
                using (var reader = new StreamReader(Upload.OpenReadStream()))
                {
                    string fileContent = reader.ReadToEnd();
                    try
                    {
                        emptyLoan = (Loan)Newtonsoft.Json.JsonConvert.DeserializeObject<Loan>(fileContent,
                            new JsonSerializerSettings
                            {
                                TypeNameHandling = TypeNameHandling.None
                            });
                    }
                    catch (JsonException je)
                    {
                        //_logger.LogError($"Unexpected error deserializing data '{je.Message}'.");
                        throw new JsonException(je.Message);
                    }
                }
            }

            //var loggedInUser = HttpContext.User;
            //var customerId = loggedInUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            //emptyLoan.CustomerId= Convert.ToInt64(customerId);
            //emptyLoan.TransactionDate = DateTime.Now;

            //if (await TryUpdateModelAsync<Loan>(
            //    emptyLoan,
            //    "loan",
            //    l => l.CustomerId,  l => l.Amount, l => l.PeriodInMonths, l => l.TransactionDate, l => l.Note))
            //{
            //    _context.Loans.Add(emptyLoan);
            //    await _context.SaveChangesAsync();
            //}

        }
    }
}
