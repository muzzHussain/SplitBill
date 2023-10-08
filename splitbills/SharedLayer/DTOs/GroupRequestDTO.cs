using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.DTOs
{
    public class GroupRequestDTO
    {
        public string Title { get; set; }
        public List<Users> UsersList { get; set; }
    }

    public class Users
    {
        public string Name { get; set; }
        public string EmailId { get; set; }
    }
   
}
