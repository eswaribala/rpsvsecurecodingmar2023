using System.ComponentModel.DataAnnotations;

namespace BankingApp.Models
{
    public class ReputableEmailAttribute:ValidationAttribute
    {
        public string GetErrorMessage() => "Email address is rejected because of its reputation";
        public ReputableEmailAttribute() : base()
        {
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string? email = value.ToString();
            var service = (IEmailReputation)validationContext.GetService(typeof(IEmailReputation));
            if (service.IsRisky(email))
                return new ValidationResult(GetErrorMessage());

            return ValidationResult.Success;
        }
    }
}
