using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CustomerApp.Contexts;
using CustomerApp.Models;

namespace CustomerApp.Pages.Corporates
{
    public class CreateModel : PageModel
    {
        private readonly CustomerApp.Contexts.BankingContext _context;
        [BindProperty]
        public List<string> CompanyTypeArr { get; set; }
        public List<SelectListItem> CompanyList { get; set; }   
        public CreateModel(CustomerApp.Contexts.BankingContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            List<SelectListItem> Companies = new List<SelectListItem>();
            Companies.Add(new SelectListItem { Text = "Public", Value = "Public" });
            Companies.Add(new SelectListItem { Text = "NGO", Value = "NGO" });
            Companies.Add(new SelectListItem { Text = "Private", Value = "Private" });
            Companies.Add(new SelectListItem { Text = "MNC", Value = "MNC" });
            Companies.Add(new SelectListItem { Text = "Government", Value = "Government" });
            this.CompanyList = Companies;
            return Page();
        }

        [BindProperty]
        public Corporate Corporate { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }
            CompanyType ConvType;
            var CompanyTypeData = string.Join(",", CompanyTypeArr.ToArray());
            if(Enum.TryParse<CompanyType>(CompanyTypeArr[0], out ConvType))
            {
                Corporate.CompanyType = ConvType;
            }

            //Corporate.CompanyType = message;
            _context.Corporates.Add(Corporate);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
