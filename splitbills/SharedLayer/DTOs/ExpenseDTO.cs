using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharedLayer.DTOs
{
    public class ExpenseDTO
    {
      
        public Guid GroupId { get; set; }

        public string Title { get; set; }

        public IDictionary<string, double> UsersList { get; set; }
    }
}
