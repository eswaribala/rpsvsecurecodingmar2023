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
    public class IndexModel : PageModel
    {
        private readonly BankingApp.Contexts.CustomerContext _context;
        private readonly IDataProtector _dataProtector;
        public IndexModel(BankingApp.Contexts.CustomerContext context, IDataProtector dataProtector)
        {
            _context = context;
            _dataProtector = dataProtector;
        }

        public IList<Customer> Customer { get;set; } = default!;

        public async Task OnGetAsync()
        {

            foreach (var cust in _context.Customers)
            {
                cust.EncCustomerID = _dataProtector.Protect(cust.CustomerId.ToString());
            }

            Customer = await _context.Customers.ToListAsync();

            //if (_context.Customers != null)
            //{
            //    Customer = await _context.Customers.ToListAsync();
            //}
        }
    }
}
