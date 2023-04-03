using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BankingApp.Contexts;
using BankingApp.Models;

namespace BankingApp.Pages_Individuals
{
    public class IndexModel : PageModel
    {
        private readonly BankingApp.Contexts.CustomerContext _context;

        public IndexModel(BankingApp.Contexts.CustomerContext context)
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
