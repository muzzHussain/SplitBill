using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.DTOs
{
    public class DisplayDTO
    {
        public List<Expense> UsersList { get; set; }
    }
    public class Expense
    {
        public Guid GroupId { get; set; }
        public decimal Ammount { get; set; }
        public Guid ExpenseId { get; set; }
    }

}
