using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

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

        private string? landMark;
        [StringLength(160)]
        [DataType(DataType.MultilineText)]
        [Column("LandMark")]
        public string? LandMark
        {
            get => landMark;
            set => landMark = Regex.Replace(value, @"[\!\@\$\%\^\&\<\>\?\|\;\[\]\{\~]+", string.Empty);
           // set => landMark = new HtmlSanitizer().Sanitize(value);
        }
    }
}
