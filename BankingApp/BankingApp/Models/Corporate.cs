using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingApp.Models
{
    public enum CompanyType { Public,Private,NGO,MNC, Government}
    [Table("Corporate")]
    public class Corporate:Customer
    {
        [Required]
        public CompanyType CompanyType { get; set; }

        public Corporate(CompanyType companyType)
        {
            CompanyType = companyType;
        }

        public Corporate(long customerId, string? firstName, string? lastName, long contactNo, string? email, string? password) : base(customerId, firstName, lastName, contactNo, email, password)
        {
        }

        public Corporate()
        {
        }
    }
}
