using SharedLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Business.BusinessInterface
{

    public interface IGroupBusinessLogic<A, B,C> 

    {
        public Task<Guid> CreateGroup(string emailId, A entity);

        public Task<IEnumerable<B>> FetchAllGroupsById(string emailId);
        public Task<bool> DeletegroupById(Guid groupId);


        public Task<IEnumerable<C>> GetDetailsGroupById(Guid id);


        public Task<bool> LeaveGroup(Guid groupId, string emailId);
        

        public Task<bool> AddUserIntoExistingGroup(Guid groupId, string userEmailId);

        
        public Task<bool> RemoveUserFromGroup(Guid groupId, string userEmailId);



    }
}
