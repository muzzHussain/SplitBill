using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class GroupExpense
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public Guid GroupId { get; set; }

        [ForeignKey("GroupId")]
        public Group Group { get; set; }

        [Required]
        public double ExpenseAmount { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Timestamp { get; set; }


        public ICollection<UserExpense> UserExpenses { get; set; }

      

    }
}
