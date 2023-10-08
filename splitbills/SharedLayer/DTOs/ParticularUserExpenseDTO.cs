using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.DTOs
{
    public class ParticularUserExpenseDTO
    {
        public Guid ExpenseId { get; set; }

        public string UserId { get; set; }

        public double spendAmount { get; set; }

        public double perPerson { get; set; }

        public Guid GroupId { get; set; }

        public string userName { get; set; }

        public string expenseTitle { get; set; }

        public string EmailId { get; set; }
    }
}
