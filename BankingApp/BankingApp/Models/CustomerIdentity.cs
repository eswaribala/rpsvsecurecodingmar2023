using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BankingApp.Models
{
    public class CustomerIdentity:IdentityUser
    {

        //public int ID { get; set; }

        [PersonalData]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        [Display(Name = "First Name")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? FirstName { get; set; }

        [PersonalData]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        [Display(Name = "Middle Name")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? MiddleName { get; set; }

        [PersonalData]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        [Display(Name = "Last Name")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? LastName { get; set; }

        [PersonalData]
        [DisplayFormat(DataFormatString = "{mm/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
