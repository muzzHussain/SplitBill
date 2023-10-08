using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.DTOs
{
    public class GroupExpenseDTO
    {
        public Guid ExpenseId { get; set; }

        public string ExpenseTitle { get; set; }

        public double TotalExpenseAmount { get; set; }
        
    }
}
