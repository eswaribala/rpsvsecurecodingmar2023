using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RateLimitingBankAPI.Models
{
    public enum CompanyType { PUBLIC,PRIVATE,NGO, GOVERNMENT}
    [Table("Corporate")]
    public class Corporate:Customer
    {
        [Required]
        [Column("CompanyType")]
        public CompanyType CompanyType { get; set; }

        public Corporate(CompanyType companyType)
        {
            CompanyType = companyType;
        }
        [JsonConstructor]
        public Corporate(long customerId, string? firstName, string? lastName, long contactNo, string? email, string? password) : base(customerId, firstName, lastName, contactNo, email, password)
        {
        }

        public Corporate()
        {
        }
    }
}
