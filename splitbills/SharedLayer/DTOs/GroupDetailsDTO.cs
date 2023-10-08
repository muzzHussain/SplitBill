using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.DTOs
{
    public class GroupDetailsDTO
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<Users> GroupMembers { get; set; }
    }
}
