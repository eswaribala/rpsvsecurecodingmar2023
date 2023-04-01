using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CustomerApp.Contexts;
using CustomerApp.Models;

namespace CustomerApp.Pages.Individuals
{
    public class IndexModel : PageModel
    {
        private readonly CustomerApp.Contexts.BankingContext _context;

        public IndexModel(CustomerApp.Contexts.BankingContext context)
        {
            _context = context;
        }

        public IList<Individual> Individual { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Individuals != null)
            {
                Individual = await _context.Individuals.ToListAsync();
            }
        }
    }
}
