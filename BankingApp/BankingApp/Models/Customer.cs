﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingApp.Models
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Column("Customer_Id")]
        public long CustomerId { get; set; }

        [NotMapped]
        
        public string EncCustomerID { get; set; }

        [Column("First_Name")]
        [Required]
        [RegularExpression("^[a-zA-Z]{5,25}$",ErrorMessage ="First Name Should be in alphabets within the range of 5,25")]
        public string? FirstName { get; set; }
        [Column("Last_Name")]
        [Required]
        [RegularExpression("^[a-zA-Z]{5,25}$", ErrorMessage = "Last Name Should be in alphabets within the range of 5,25")]
        public string? LastName { get; set; }
        [Required]
        public long? ContactNo { get; set; }
        [Column("Email")]
        [Required]
        [EmailAddress]
        [ReputableEmail]
        public string? Email { get; set; }
        [Column("Password")]
        [Required]
        public string? Password { get; set; }


        public Customer(long customerId, string? firstName, string? lastName, long contactNo, string? email, string? password)
        {
            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastName;
            ContactNo = contactNo;
            Email = email;
            Password = password;
        }

        public Customer()
        {
        }
    }
}
