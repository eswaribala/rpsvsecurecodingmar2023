using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BankingApp.Contexts;
using BankingApp.Models;
using reCAPTCHA.AspNetCore;
using Microsoft.AspNetCore.Authorization;

namespace BankingApp.Pages_Customers
{
    [Authorize(Roles = "Customer")]
    [Authorize(Roles = "PendingCustomer")]
    public class CreateModel : PageModel
    {
        private readonly BankingApp.Contexts.CustomerContext _context;
        private readonly IRecaptchaService _recaptcha;
        public string ReturnUrl { get; set; }
        public CreateModel(BankingApp.Contexts.CustomerContext context, IRecaptchaService recaptcha)
        {
            _context = context;
            _recaptcha = recaptcha;
        }

        public IActionResult OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var recaptcha = await _recaptcha.Validate(this.HttpContext.Request);
            if (!recaptcha.success)

                ModelState.AddModelError("Recaptcha", "Error Validating Captcha");
            //dummy value
            Customer.EncCustomerID = "2365";
           
            if ( _context.Customers == null || Customer == null)
            {
                return Page();
            }

            _context.Customers.Add(Customer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
