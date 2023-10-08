using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.DTOs
{
    public class UpdateExpenseDTO
    {
        public Guid ExpenseId { get; set; }

        public string Title { get; set; }

        public IDictionary<string, double> UsersList { get; set; }
    }
}
