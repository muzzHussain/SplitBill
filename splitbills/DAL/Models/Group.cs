using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Group
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime CreationDate { get; set; }
    
       public ICollection<UsersGroup> Groups { get; set; }
       public ICollection<GroupExpense> GroupExpenses { get; set; }
       public ICollection<UserExpense> UserExpenses { get; set; }
    }
   
}
