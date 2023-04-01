using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CustomerApp.Contexts;
using CustomerApp.Models;

namespace CustomerApp.Pages.Addresses
{
    public class IndexModel : PageModel
    {
        private readonly CustomerApp.Contexts.BankingContext _context;

        public IndexModel(CustomerApp.Contexts.BankingContext context)
        {
            _context = context;
        }

        public IList<Address> Address { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Addresses != null)
            {
                Address = await _context.Addresses
                .Include(a => a.Customer).ToListAsync();
            }
        }
    }
}
