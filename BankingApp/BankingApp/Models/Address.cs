using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingApp.Models
{
    [Table("Address")]
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Address_Id")]
        public long AddressId { get; set; }
        [Required]
        [Column("Door_No")]
        public string? DoorNo { get; set; }
        [Required]
        [Column("Street_Name")]
        public string? StreetName { get; set; }
        [Required]
        [Column("City")]
        public string? City { get; set; }
        [Required]
        [Column("Country")]
        public string? Country { get; set; }
        [Required]
        [Column("Pin_Code")]
        public long? PinCode { get; set; }

        [ForeignKey("CustomerId")]
        [Column("CustomerId_FK")]
        public long CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
