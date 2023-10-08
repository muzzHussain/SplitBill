using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.DTOs
{
    public class CreateGroupDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<Users> UsersList { get; set; }
    }

}
