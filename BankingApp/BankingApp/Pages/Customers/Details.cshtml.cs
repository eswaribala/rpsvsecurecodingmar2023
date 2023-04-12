using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BankingApp.Contexts;
using BankingApp.Models;
using Microsoft.AspNetCore.DataProtection;

namespace BankingApp.Pages_Customers
{
    public class DetailsModel : PageModel
    {
        private readonly BankingApp.Contexts.CustomerContext _context;
        private readonly IDataProtector _dataProtector;
        public DetailsModel(BankingApp.Contexts.CustomerContext context,IDataProtectionProvider dataProtectionProvider)
        {
            _context = context;
           _dataProtector = dataProtectionProvider.CreateProtector("");
        }

      public Customer Customer { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }



#pragma warning disable CS8604 // Possible null reference argument.
            var decID = Convert.ToInt64(_dataProtector.Unprotect(id));
#pragma warning restore CS8604 // Possible null reference argument.
            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == decID);
            if (customer == null)
            {
                return NotFound();
            }
            else 
            {
                Customer = customer;
            }
            return Page();
        }
    }
}
