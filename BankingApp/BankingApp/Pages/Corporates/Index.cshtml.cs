using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CustomerApp.Contexts;
using CustomerApp.Models;

namespace CustomerApp.Pages.Corporates
{
    public class IndexModel : PageModel
    {
        private readonly CustomerApp.Contexts.BankingContext _context;

        public IndexModel(CustomerApp.Contexts.BankingContext context)
        {
            _context = context;
        }

        public IList<Corporate> Corporate { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Corporates != null)
            {
                Corporate = await _context.Corporates.ToListAsync();
            }
        }
    }
}
