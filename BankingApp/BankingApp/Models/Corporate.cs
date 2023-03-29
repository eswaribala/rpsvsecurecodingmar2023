using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingApp.Models
{
    public enum CompanType { PUBLIC,PRIVATE,NGO, GOVERNMENT}
    [Table("Corporate")]
    public class Corporate:Customer
    {
        [Required]
        public CompanType CompanType { get; set; }

        public Corporate(CompanType companType)
        {
            CompanType = companType;
        }

        public Corporate(long customerId, string? firstName, string? lastName, long contactNo, string? email, string? password) : base(customerId, firstName, lastName, contactNo, email, password)
        {
        }

        public Corporate()
        {
        }
    }
}
