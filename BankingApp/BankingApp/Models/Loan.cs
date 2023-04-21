using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingApp.Models
{
    [Serializable]
    public class Loan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ID { get; set; }



        [ForeignKey("CustomerId")]
        [Column("CustomerId_FK")]
        public long CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        [Display(Name = "Term in Months")]
        public int PeriodInMonths { get; set; }

        [Display(Name = "Transaction Date")]
        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; }

        public LoanStatus Status { get; set; }

        [Display(Name = "Notes")]
        public string? Note { get; set; }
    }

    public enum LoanStatus
    {
        Approved,
        Denied,
        Pending
    }
}
