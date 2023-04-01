using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingAPI.Models
{
    public enum Gender { MALE,FEMALE,TRANSGENDER}
    [Table("Individual")]
    public class Individual:Customer
    {
        [Required]
        [Column("Gender")]
        public Gender Gender { get; set; }
        [Required]
        [Column("DOB")]
        public DateTime DOB { get; set; }

        public Individual(Gender gender, DateTime dOB)
        {
            Gender = gender;
            DOB = dOB;
        }

        public Individual()
        {
        }

        public Individual(long customerId, string? firstName, string? lastName, long contactNo, string? email, string? password) : base(customerId, firstName, lastName, contactNo, email, password)
        {
           
        }
    }
}
