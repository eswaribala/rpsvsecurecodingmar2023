using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BankingApp.Contexts;
using BankingApp.Models;

namespace BankingApp.Pages_Corporates
{
    public class IndexModel : PageModel
    {
        private readonly BankingApp.Contexts.CustomerContext _context;

        public IndexModel(BankingApp.Contexts.CustomerContext context)
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
