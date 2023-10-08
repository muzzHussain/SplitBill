using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class UserExpense
    {

        [Key]
        public Guid Id { get; set; }

        public Guid ExpenseId { get; set; }

        [ForeignKey("ExpenseId")]
        public GroupExpense GroupExpense { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public Guid GroupId { get; set; }

        [ForeignKey("GroupId")]
        public Group Group { get; set; }

        [Required]
        public double Spend_Amount { get; set; }

        [Required]
        public double Per_Person { get; set; }

      
       
    }
}
