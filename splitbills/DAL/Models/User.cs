using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DAL.Models
{
    public class User: IdentityUser
    {
       
        public ICollection<UsersGroup> UsersGroups { get; set; }
        public ICollection<UserExpense> UserExpenses { get; set; }
    }
}
