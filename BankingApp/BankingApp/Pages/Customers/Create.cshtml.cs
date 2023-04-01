using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CustomerApp.Contexts;
using CustomerApp.Models;
using reCAPTCHA.AspNetCore;
using reCAPTCHA.AspNetCore.Attributes;

namespace CustomerApp.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly CustomerApp.Contexts.BankingContext _context;
        private readonly IRecaptchaService _recaptcha;
        public string ReturnUrl { get; set; }
        public CreateModel(CustomerApp.Contexts.BankingContext context,IRecaptchaService recaptcha)
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
        public Customer Customer { get; set; }
        

    
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            var recaptcha = await _recaptcha.Validate(this.HttpContext.Request);
            if (!recaptcha.success)
           
                ModelState.AddModelError("Recaptcha", "Error Validating Captcha");
              
            //returnUrl ??= Url.Content("~/");


            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Customers.Add(Customer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
